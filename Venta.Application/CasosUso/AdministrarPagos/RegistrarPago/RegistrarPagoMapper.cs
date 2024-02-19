using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto;
using Venta.Domain.Models;

namespace Venta.Application.CasosUso.AdministrarPagos.RegistroPago
{
    public class RegistrarPagoMapper : Profile
    {
        public RegistrarPagoMapper()
        {
            CreateMap<RegistrarPagoRequest, Pago>();
            //    .ForMember();

            //CreateMap<RegistrarVentaRequest, Models.Venta>()
            //    .ForMember(dest => dest.Detalle, map => map.MapFrom(src => src.Productos));
        }
    }
}
