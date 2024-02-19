using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos;
using Venta.Application.CasosUso.AdministrarVentas.RegistrarVenta;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Venta.Tests.RegistroVentas.Testes
{
    public class AdministrarVentasTests
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly RegistrarVentaHandler _registrarVentaHandler;

        public AdministrarVentasTests()
        {
            _mapper = new MapperConfiguration(c => c.AddProfile<RegistrarVentaMapper>()).CreateMapper();
            _ventaRepository = Substitute.For<IVentaRepository>();
            _productoRepository = Substitute.For<IProductoRepository>();
            _registrarVentaHandler = Substitute.For<RegistrarVentaHandler>(_ventaRepository, _productoRepository, _mapper);
        }


        [Fact]
        public async Task RegistraVentas()
        {
            //var request = setVentaRequest();

            //var objProducto = new Producto() { IdProducto = 2, Stock=30,StockMinimo=1};
            ////var objVenta = new Domain.Models.Venta();

            //_productoRepository.ConsultarById(default(int)).ReturnsForAnyArgs(objProducto);
            //_ventaRepository.Registrar(default).ReturnsForAnyArgs(true);

            //var resultado = await _registrarVentaHandler.Registrar(request);

            //Assert.True(resultado.VentaRegistrada);


        }
        //private RegistrarVentaRequest setVentaRequest()
        //{
        //    var registrarVentaDetalleRequest = new List<RegistrarVentaDetalleRequest>();
        //    registrarVentaDetalleRequest.Add(new RegistrarVentaDetalleRequest() { Cantidad = 2, IdProducto = 2, Precio = 10 });
        //    registrarVentaDetalleRequest.Add(new RegistrarVentaDetalleRequest() { Cantidad = 4, IdProducto = 10, Precio = 30 });
        //    registrarVentaDetalleRequest.Add(new RegistrarVentaDetalleRequest() { Cantidad = 5, IdProducto = 7, Precio = 30 });
        //    var registrarVentaRequest = new RegistrarVentaRequest() { IdCliente = 2, Productos = registrarVentaDetalleRequest };
        //    return registrarVentaRequest;
        //}
    }
}
