﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Venta.Application.CasosUso.Venta;
using Venta.Domain.Repositories;

namespace Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosHandler
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public ConsultarProductosHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }

        public ConsultarProductosResponse Handle(ConsultarProductosRequest request)
        {
            var response = new ConsultarProductosResponse();

            var datos = _productoRepository.Consultar(request.FiltroPorNombre);

            response.Resultado = _mapper.Map<IEnumerable<ConsultaProducto>>(datos);


            return response;
        }
    }
}
