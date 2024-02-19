using AutoMapper;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.CasosUso.AdministrarProductos.ConsultarProductos;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Venta.Test.Aplication.Testes
{


    public class AdministrarProductosTests
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        private readonly ConsultarProductosHandler _consultarProductosHandler;
        
        public AdministrarProductosTests()
        {
            _mapper = new MapperConfiguration(c=>c.AddProfile<ConsultarProductosMapper>()).CreateMapper();
            //_mapper = Substitute.For<IMapper>();
            _productoRepository = Substitute.For<IProductoRepository>();
            _consultarProductosHandler = Substitute.For<ConsultarProductosHandler>(_productoRepository, _mapper);
        }


        [Fact]
        public async Task ConsultarProductos()
        {
            //var request = new ConsultarProductosRequest() { FiltroPorNombre = "123" };
            //var response = new ConsultarProductosResponse();
            //response.Resultado = AddListaResponse();
            //IEnumerable<Producto> m_oEnum = AddLista();
            //_productoRepository.Consultar(default).ReturnsForAnyArgs(m_oEnum);
            //_mapper.Map<IEnumerable<ConsultaProducto>>(default).ReturnsForAnyArgs(response.Resultado);

            //response = await _consultarProductosHandler.Handle(request);

            //Assert.True(response.Resultado.ToList().Count > 0);

        }

        [Fact]
        public async Task ConsultarProductosV2()
        {
            //var request = new ConsultarProductosRequest() { FiltroPorNombre = "123" };
            
            //IEnumerable<Producto> m_oEnum = AddLista();
            //_productoRepository.Consultar(default).ReturnsForAnyArgs(m_oEnum);

            //var response = await _consultarProductosHandler.Handle(request);

            //Assert.True(response.Resultado.ToList().Count > 0);

        }

        private List<Producto> AddLista()
        {
            var lstProducto = new List<Producto>();
            var producto1 = new Producto() { IdProducto=1,Nombre="Naranjas"};
            var producto2 = new Producto();
            var producto3 = new Producto();
            lstProducto.Add(producto1);
            lstProducto.Add(producto2);
            lstProducto.Add(producto3);
            return lstProducto;
        }
        private List<ConsultaProducto> AddListaResponse()
        {
            var lstProducto = new List<ConsultaProducto>();
            var producto1 = new ConsultaProducto();
            lstProducto.Add(producto1);
            return lstProducto;
        }
    }
}
