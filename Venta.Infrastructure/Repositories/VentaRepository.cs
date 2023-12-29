using Venta.Domain.Repositories;

namespace Venta.Infrastructure.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        public Task<bool> Registrar(Domain.Models.Venta venta)
        {
            throw new NotImplementedException();
        }
    }
}
