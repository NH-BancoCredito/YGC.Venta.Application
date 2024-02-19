using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto;
using Venta.Application.Common;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using Venta.Domain.Services.WebServices;
namespace Venta.Application.CasosUso.AdministrarPagos.RegistroPago
{
    public class RegistrarPagoHandler : IRequestHandler<RegistrarPagoRequest, IResult>
    {
        private readonly IPagosService _pagoService;
        private readonly IMapper _mapper; 

        public RegistrarPagoHandler(IPagosService pagosService, IMapper mapper)
        {
            _pagoService = pagosService;
            _mapper = mapper;
        }


        public async Task<IResult> Handle(RegistrarPagoRequest request, CancellationToken cancellationToken)
        {

            IResult response = null;
            bool result = false;

            try
            {
                var pago = _mapper.Map<Pago>(request);
                result = await _pagoService.Pagar(pago);

                if (result)
                {
                    return new SuccessResult();
                }
                else
                    return new FailureResult();

            }
            catch (Exception ex)
            {
                response = new FailureResult();
                return response;
            }
        }
    }
}
