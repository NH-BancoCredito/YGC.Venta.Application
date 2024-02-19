using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Venta.Application.Common;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using Venta.Domain.Service.Events;
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
        private readonly IEventSender _eventSender;
        private readonly IPagosService _pagoService;
        private readonly IClienteRepository _clienteRepository;


        public RegistrarVentaHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper,
            IStocksService stocksService, ILogger<RegistrarVentaHandler> logger, IPagosService pagoService, IClienteRepository clienteRepository, IEventSender eventSender)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;
            _stocksService = stocksService;
            _pagoService = pagoService;
            _clienteRepository = clienteRepository;
            _eventSender = eventSender;
        }

        public async Task<IResult> Handle(RegistrarVentaRequest request, CancellationToken cancellationToken)
        {
            try
            {
                IResult response = null;
                bool responsePago = false;
                IResult responseEntrega = null;
                //Aplicando el automapper para convertir el objeto Request a venta dominio
                var venta = _mapper.Map<Models.Venta>(request);
                var pago = _mapper.Map<Pago>(request.Pago);
                var entrega = new Entrega();
                var lstEntregaDetalle = new List<EntregaDetalle>();
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
                    var entregaDetalle = new EntregaDetalle();
                    entregaDetalle.Producto = productoEncontrado.Nombre;
                    entregaDetalle.Cantidad=detalle.Cantidad;
                    lstEntregaDetalle.Add(entregaDetalle);
                    //descomentar
                    await _stocksService.ActualizarStock(detalle.IdProducto, detalle.Cantidad);            

                }
                await _ventaRepository.Registrar(venta);
                response = new SuccessResult<int>(venta.IdVenta);
                if (response.HasSucceeded)
                {
                    pago.Monto = venta.Monto;
                    pago.IdVenta = venta.IdVenta;
                    responsePago = await _pagoService.Pagar(pago);//descomentar
                    if (responsePago)
                    {
                        var clienteEncontrado = await _clienteRepository.ConsultarById(request.IdCliente);
                        entrega=setEntrega(venta.IdVenta, clienteEncontrado.Nombre + " " + clienteEncontrado.Apellidos, request.Entrega.DireccionEntrega, request.Entrega.Ciudad, lstEntregaDetalle);
                        await _eventSender.PublishAsync("entregas", JsonSerializer.Serialize(entrega), cancellationToken);
                    //    return new SuccessResult();
                }

            }
                //if(response.HasSucceeded)
                //{
                //    await _eventSender.PublishAsync("entregas", JsonSerializer.Serialize(venta), cancellationToken);
                //    return new SuccessResult();
                //}
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

        private Entrega setEntrega(int idVenta, string nombreCliente,string direccionEntrega, string ciudad,List<EntregaDetalle> lstEntregaDetalle)
        {
            var entrega = new Entrega();
            entrega.IdVenta = idVenta;
            entrega.NombreCliente = nombreCliente;
            entrega.DireccionEntrega = direccionEntrega;
            entrega.Ciudad = ciudad;
            entrega.Detalle = lstEntregaDetalle;

            return entrega;
        }
    }
}
