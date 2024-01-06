﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Venta.Application.CasosUso.Venta;
using Venta.Domain.Models;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos
{
    public class ConsultarProductosMapper : Profile
    {
        public ConsultarProductosMapper()
        {
            CreateMap<Producto, ConsultaProducto>()
                .ForMember(dest => dest.CodigoProducto, opt => opt.MapFrom(src => src.IdProducto));
        }
    }
}
