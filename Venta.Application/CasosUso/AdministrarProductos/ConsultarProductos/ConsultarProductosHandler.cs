﻿using Microsoft.Extensions.Logging;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Common;

//using Venta.Application.CasosUso.Venta;
using Venta.Domain.Repositories;

namespace Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosHandler :
        IRequestHandler<ConsultarProductosRequest, IResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ConsultarProductosHandler(IProductoRepository productoRepository, IMapper mapper
            , ILogger<ConsultarProductosHandler> logger)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
            _logger = logger;

        }

        /* public ConsultarProductosResponse Handle(ConsultarProductosRequest request)
         {
             var response = new ConsultarProductosResponse();

             var datos = _productoRepository.Consultar(request.FiltroPorNombre);

             response.Resultado = _mapper.Map<IEnumerable<ConsultaProducto>>(datos);


             return response;
         }*/
        public async Task<IResult> Handle(ConsultarProductosRequest request, CancellationToken cancellationToken)
        {
            IResult response = null;

            try
            {

                var datos = await _productoRepository.Consultar(request.FiltroPorNombre);
                response = new SuccessResult<IEnumerable<ConsultaProducto>>(
                        _mapper.Map<IEnumerable<ConsultaProducto>>(datos)
                        );

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new FailureResult();
                return response;
            }
        }
    }
}
