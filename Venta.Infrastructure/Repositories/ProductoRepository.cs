using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Repositories;
using Venta.Domain.Models;
using Venta.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;



namespace Venta.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly VentaDbContext _context;
        public ProductoRepository(VentaDbContext context)
        {
            _context = context;
        }
        public  async Task<bool> Adicionar(Producto entity)
        {
            try
            {
                entity.PrecioUnitario = Convert.ToDecimal(entity.PrecioUnitario);
              _context.Productos.Add(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new NotImplementedException();

            }

        }

        public async Task<IEnumerable<Producto>> Consultar(string nombre)
        {
            try {
                return await _context.Productos.Include(p => p.Categoria).ToListAsync();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
            
            //return await _context.Productos.ToListAsync();
            
        }

        public async Task<Producto> ConsultarById(int id)
        {
            
            try
            {
                return await _context.Productos.FindAsync(id);
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public Task<bool> Eliminar(Producto entity)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Modificar(Producto entity)
        {
            try
            {
                var productoencontrado = await _context.Productos.FindAsync(entity.IdProducto);
                productoencontrado.Stock = entity.Stock;
                await _context.SaveChangesAsync();
                return true;
                //_context.Entry..Attach(entity);
                //await _context.Attach(entity);


                //context.Entry.Attach(entidad);
                //context.Entry(entidad).Property(x => x.campo1).IsModified = true;
                //db.SaveChanges();
                //db.Entry(user).Property(x => x.Password).IsModified = true;
                //context.Entry.Attach(entidad);
                //context.Entry(entidad).Property(x => x.campo1).IsModified = true;
                //db.SaveChanges();

                //db.Entry(user).Property(x => x.Password).IsModified = true;
                return true;

                //context.Entry.Attach(entidad);
                //context.Entry(entidad).Property(x => x.campo1).IsModified = true;
                //db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();

            }

        }
    }
}

