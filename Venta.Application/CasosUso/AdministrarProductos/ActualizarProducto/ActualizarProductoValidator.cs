using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto
{
    public class ActualizarProductoValidator : AbstractValidator<ActualizarProductoRequest>
    {
        public ActualizarProductoValidator()
        {
            RuleFor(item => item.Nombre).NotEmpty().WithMessage("Debe especificar un nombre");
            RuleFor(item => item.Stock).NotEmpty().WithMessage("Debe especificar un Stock");
            RuleFor(item => item.StockMinimo).NotEmpty().WithMessage("Debe especificar un Stock mínimo");
            RuleFor(item => item.PrecioUnitario).NotEmpty().WithMessage("Debe especificar un precio unitario");
        }
    }
}
