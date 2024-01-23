using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos;

namespace Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto
{
    public class ActualizarProductoResponse
    {
        public IEnumerable<ConsultaProducto> Resultado { get; set; }

    }
}
