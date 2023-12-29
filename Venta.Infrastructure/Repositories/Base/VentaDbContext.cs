﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Domain.Models;

namespace Venta.Infrastructure.Repositories.Base
{
    public class VentaDbContext : DbContext
    {
        /// <summary>
        /// Configurando el aceso a la base de datos
        /// </summary>
        /// <param name="options"></param>
        public VentaDbContext(DbContextOptions<VentaDbContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Producto> Productos { get; set; }


        public virtual DbSet<Categoria> Categorias { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }


        public virtual DbSet<Domain.Models.Venta> Ventas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(
                p =>
                {
                    p.ToTable("Categoria");
                    p.HasKey(c => c.IdCategoria);
                }
                );

                modelBuilder.Entity<Cliente>(
                    p =>
                    {
                        p.ToTable("Cliente");
                        p.HasKey(c => c.IdCliente);
                    }
                    );

            modelBuilder.Entity<Producto>(
                p =>
                {
                    p.ToTable("Producto");
                    p.HasKey(c => c.IdProducto);
                    p.Property(c => c.PrecioUnitario).HasPrecision(2);

                    p.HasOne(p => p.Categoria).WithMany(p => p.Productos)
                        .HasForeignKey(p => p.IdCategoria);
                }
                );


            modelBuilder.Entity<Domain.Models.Venta>(
               p =>
               {
                   p.ToTable("Venta");
                   p.HasKey(c => c.IdVenta);

                   p.HasOne(p => p.Cliente).WithMany(p => p.Ventas)
                        .HasForeignKey(p => p.IdCliente);
               }
               );


            modelBuilder.Entity<Domain.Models.VentaDetalle>(
               p =>
               {
                   p.ToTable("VentaDetalle");
                   p.HasKey(c => c.IdVentaDetalle);

                   p.HasOne(p => p.Venta).WithMany(p => p.Detalle)
                        .HasForeignKey(p => p.IdVenta);

                   p.HasOne(p => p.Producto).WithMany(p => p.VentaDetalles)
                       .HasForeignKey(p => p.IdProducto);
               }
               );
        }

    }
}
