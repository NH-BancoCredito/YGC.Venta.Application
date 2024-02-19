using MongoDB.Bson.IO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Venta.Domain.Models;
using Venta.Domain.Services.WebServices;

namespace Venta.Infrastructure.Services.WebServices
{
    public class PagosService : IPagosService
    {
        private readonly HttpClient _httpClientPagos;
        public PagosService(HttpClient httpClientPagos)
        {
            _httpClientPagos = httpClientPagos;
            _httpClientPagos.DefaultRequestHeaders.ProxyAuthorization = null;

        }

        public async Task<bool> Pagar(Pago pago)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Post, "api/pagos/pagar");

                var entidadSerializada = JsonSerializer.Serialize(new { IdVenta = pago.IdVenta, Monto = pago.Monto, FormaPago = pago.FormaPago, NumeroTarjeta = pago.NumeroTarjeta, CVV = pago.CVV, NombreTitular = pago.NombreTitular, NumeroCuotas = pago.NumeroCuotas });
                request.Content = new StringContent(entidadSerializada, Encoding.UTF8, MediaTypeNames.Application.Json);

                var response = await _httpClientPagos.SendAsync(request);
                Respuesta rpta = JsonSerializer.Deserialize<Respuesta>(response.Content.ReadAsStringAsync().Result);
                return rpta.hasSucceeded;
            }
            catch (Exception ex)
            {
                return false;   
            }
            

        }

       
      
    }
}
