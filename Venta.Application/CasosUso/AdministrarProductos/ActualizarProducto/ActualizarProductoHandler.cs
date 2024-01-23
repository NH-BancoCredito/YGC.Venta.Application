using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Common;
using Venta.Domain.Models;
using Venta.Domain.Repositories;

namespace Venta.Application.CasosUso.AdministrarProductos.ActualizarProducto
{
        /* 
         1 - Deberia verificar si el productos
         Si existe , entoces actualizar en la table de producto
         Si no existe, crear un nuevo registro

         */    
    public class ActualizarProductoHandler :
       IRequestHandler<ActualizarProductoRequest, IResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public ActualizarProductoHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }


        public async Task<IResult> Handle(ActualizarProductoRequest request, CancellationToken cancellationToken)
        {

            IResult response = null;
            bool result = false;

            try
            {
                var producto = _mapper.Map<Producto>(request);
                var productoEncontrado = await _productoRepository.ConsultarById(request.IdProducto);

                if (productoEncontrado==null)
                {
                    result = await _productoRepository.Adicionar(producto);

                }else
                {
                    result = await _productoRepository.Modificar(producto);

                }
                
                if (result)
                {
                    return new SuccessResult();
                }
                else
                    return new FailureResult();

            }
            catch (Exception ex)
            {
                response = new FailureResult();
                return response;
            }
        }
    }
}
