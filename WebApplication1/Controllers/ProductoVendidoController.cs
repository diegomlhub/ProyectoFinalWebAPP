using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPI.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductosVendidosByIdUsuario")]
        public List<ProductoVendido> Get(long idUsuario)
        {
            return ProductoVendidoHandler.Get(idUsuario);
        }

        [HttpDelete]
        public bool Delete([FromBody] int id)
        {
            try
            {
                return ProductoVendidoHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Update([FromBody] PutProductoVendido productoVendido)
        {
            try
            {
                return ProductoVendidoHandler.Update(new ProductoVendido
                {
                    Id = productoVendido.Id,
                    IdProducto = productoVendido.IdProducto,
                    Stock = productoVendido.Stock,
                    IdVenta = productoVendido.IdVenta
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Add([FromBody] PostProductoVendido productoVendido)
        {
            try
            {
                return ProductoVendidoHandler.Add(new ProductoVendido
                {                    
                    IdProducto = productoVendido.IdProducto,
                    Stock = productoVendido.Stock,
                    IdVenta = productoVendido.IdVenta
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
