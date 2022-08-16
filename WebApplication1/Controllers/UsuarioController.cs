using Microsoft.AspNetCore.Mvc;
using WebApplication1.Model;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuarios")]
        public List<Usuario> GetUsuarios()
        {
            return new List<Usuario>() { new Usuario() { Apellido = "Lofiego", Contraseña = "123", Nombre = "Diego" } };
        }
        [HttpDelete]
        public void EliminarUsuario([FromBody] int id)
        {
            
        }

        [HttpPut]
        public void ModificarUsuario([Frombody] PutUsuario usuario)
        {

        }

        [HttpPost]
        public void ActualizarUsuario([Frombody] PostUsuario usuario)
        {

        }

    }

}
