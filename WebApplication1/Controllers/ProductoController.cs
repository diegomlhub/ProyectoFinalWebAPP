using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductos")]
        public List<Producto> GetProductos()
        {
            return new List<Producto>() { new Producto() { Apellido = "Lofiego", Contraseña = "123", Nombre = "Diego" } };
        }

        [HttpDelete]
        public void EliminarUsuario([FromBody] int id)
        {

        }

        [HttpPut]
        public void ModificarUsuario([FromBody] PutProducto producto)
        {

        }

        [HttpPost]
        public void ActualizarUsuario([FromBody] PostProducto producto)
        {

        }
    }
}
