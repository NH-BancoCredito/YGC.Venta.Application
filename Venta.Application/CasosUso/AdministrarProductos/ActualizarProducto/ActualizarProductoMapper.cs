using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Venta.Domain.Models;

namespace Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto
{
    public class ActualizarProductoMapper : Profile
    {
        public ActualizarProductoMapper()
        {
            CreateMap<ActualizarProductoRequest, Producto>();
            //    .ForMember();

            //CreateMap<RegistrarVentaRequest, Models.Venta>()
            //    .ForMember(dest => dest.Detalle, map => map.MapFrom(src => src.Productos));
        }
    }
}
