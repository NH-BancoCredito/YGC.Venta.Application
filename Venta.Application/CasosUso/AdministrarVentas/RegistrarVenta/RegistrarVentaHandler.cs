using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Common;
using Venta.Domain.Repositories;
using Venta.Domain.Services.WebServices;
using Models = Venta.Domain.Models;


namespace Venta.Application.CasosUso.AdministrarVentas.RegistrarVenta
{
    public  class RegistrarVentaHandler :
        IRequestHandler<RegistrarVentaRequest, IResult>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly IStocksService _stocksService;
        private readonly ILogger _logger;

        public RegistrarVentaHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper,
            IStocksService stocksService, ILogger<RegistrarVentaHandler> logger)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
            _stocksService = stocksService;
        }

        public async Task<IResult> Handle(RegistrarVentaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IResult response = null;

                //Aplicando el automapper para convertir el objeto Request a venta dominio

                var venta = _mapper.Map<Models.Venta>(request);

                //var ventaFinal = new Models.Venta();
                //var VentaDetalleFinal = new List<VentaDetalle>();
                ///============Condiciones de validaciones
                _logger.LogInformation($"Cantidad de productos {venta.Detalle.Count()}");


                foreach (var detalle in venta.Detalle)
                {
                    //1 - Validar si el productos existe
                    var productoEncontrado = await _productoRepository.ConsultarById(detalle.IdProducto);
                    if (productoEncontrado?.IdProducto <= 0)
                    {
                        throw new Exception($"Producto no encontrado, código {detalle.IdProducto}");
                    }

                    //Actualizar el detalle del pedido con el precio del producto
                    detalle.Precio = productoEncontrado.PrecioUnitario;


                    ////2 - Validar si existe stock suficiente - TODO
                    //if(detalle.Cantidad> productoEncontrado.Stock)
                    //{
                    //    throw new Exception($"El producto {detalle.IdProducto} no tiene stock, solo hay en almacen {detalle.Cantidad}");

                    //}

                    ////3 - Reservar el stock del producto - TODO
                    //if (detalle.Cantidad < productoEncontrado.StockMinimo)
                    //{
                    //    throw new Exception($"El producto {detalle.IdProducto} no cumple con el stock mínimo de {productoEncontrado.StockMinimo}");

                    //}
                    //detalle.Producto = productoEncontrado;

                    ////VentaDetalleFinal.Add(detalle);

                    ////3.1 --Si sucedio algun erro al reservar el producto , retornar una exepcion
                    await _stocksService.ActualizarStock(detalle.IdProducto, detalle.Cantidad);
                    //try
                    //{

                    //}catch(Exception ex)
                    //{
                    //    throw new Exception($"Error al reservar");
                    //}

                }
                //venta.Detalle = VentaDetalleFinal;
                //if(!(await _ventaRepository.Registrar(venta)))
                //{
                //    throw new Exception($"Error al registrar venta, código {venta.IdVenta}");
                //}
                //response.VentaRegistrada = true;

                await _ventaRepository.Registrar(venta);

                response = new SuccessResult<int>(venta.IdVenta);

                //Registrar
                /// SI todo esta OK
                /// Registrar la venta - TODO

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            
        }

    }
}
