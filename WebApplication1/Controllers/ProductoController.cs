using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "TraerProductos")]
        public List<Producto> Get()
        {
            return ProductoHandler.Get();
        }

        [HttpDelete(Name = "EliminarProducto")]
        public bool Delete([FromBody] int id)
        {
            try
            {
                return ProductoHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut(Name = "ModificarProducto")]
        public bool Update([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.Update(new Producto
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost(Name = "CrearProducto")]
        public bool Add([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.Add(new Producto
                {                    
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
