using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Common;

namespace Venta.Application.CasosUso.AdministrarPagos.RegistroPago
{
    public class RegistrarPagoRequest : IRequest<IResult>
    {
        public int IdVenta { get; set; }

        public int Monto { get; set; }

        public int FormaPago { get; set; }
        public string? NumeroTarjeta { get; set; }
        //public DateTime FechaVencimiento { get; set; }
        public string? CVV { get; set; }
        public string? NombreTitular { get; set; }
        public int? NumeroCuotas { get; set; }

    }

}
