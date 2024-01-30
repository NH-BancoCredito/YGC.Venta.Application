﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Repositories;
using Venta.Infrastructure.Repositories;
using Venta.Infrastructure.Repositories.Base;
using System.Reflection;
using System.Runtime.CompilerServices;
using Venta.Domain.Services.WebServices;
using Venta.Infrastructure.Services.WebServices;
using Microsoft.Extensions.Configuration;
using Venta.CrossCutting.Configs;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Net;
using Polly.Extensions.Http;
using Polly;
using Polly.CircuitBreaker;
namespace Venta.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfraestructure(
    this IServiceCollection services, IConfiguration configInfo
    )
        {
            var appConfiguration = new AppConfiguration(configInfo);

            var httpClientBuilder = services.AddHttpClient<IStocksService, StockService>(
                options =>
                {
                    options.BaseAddress = new Uri(appConfiguration.UrlBaseServicioStock);
                   // options.Timeout = TimeSpan.FromMilliseconds(5000);
                }
                ).SetHandlerLifetime(TimeSpan.FromMinutes(5))
                //.AddPolicyHandler(GetRetryPolicy());
                .AddPolicyHandler(GetCircuitBreakerPolicy());


            services.AddDbContext<VentaDbContext>(
                options => options.UseSqlServer(appConfiguration.ConexionDBVentas)
                );

            services.AddRepositories(Assembly.GetExecutingAssembly());
        }
        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(2,
                            retryAttempts => TimeSpan.FromSeconds(Math.Pow(2, retryAttempts)));
        }

        private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            Action<DelegateResult<HttpResponseMessage>, TimeSpan> onBreak = (result, timeSpan) =>
            {
                Console.WriteLine(result);
            }
            ;
            Action onReset = null;
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30),
                onBreak, onReset
                );


        }


        public static void
                AddRepositories(this IServiceCollection services, Assembly assembly)

        {


            var respositoryTypes = assembly
                .GetExportedTypes().Where(item => item.GetInterface(nameof(IRepository)) != null).ToList();


            foreach (var repositoryType in respositoryTypes)
            {
                var repositoryInterfaceType = repositoryType.GetInterfaces()
                    .Where(item => item.GetInterface(nameof(IRepository)) != null)
                    .First();

                services.AddScoped(repositoryInterfaceType, repositoryType);
            }
        }

        /*
        public static void AddInfraestructure(
            this IServiceCollection services , string connectionString
            )
        {
            var httpClientBuilder = services.AddHttpClient<IStocksService, StockService>(
                options =>
                {
                    options.BaseAddress = new Uri("https://localhost:7065/");
                    options.Timeout = TimeSpan.FromMilliseconds(30000);
                }
                );

            services.AddDbContext<VentaDbContext>(
                options => options.UseSqlServer(connectionString)
                );

            services.AddRepositories();
        }
        
        private static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProductoRepository, ProductoRepository>();

            services.AddScoped<IVentaRepository, VentaRepository>();
        }*/
        private static void SetHttpClient<TClient, TImplementation>(this IServiceCollection services, string constante) where TClient : class where TImplementation : class, TClient
        {

            services.AddHttpClient<TClient, TImplementation>(options =>
            {
                options.Timeout = TimeSpan.FromMilliseconds(2000);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(30))
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
              

                return handler;
            });
        }
    }
}
