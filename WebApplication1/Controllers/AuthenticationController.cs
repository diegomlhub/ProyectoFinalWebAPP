using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpGet(Name = "Autenticacion")]
        public Usuario Auth(string nombreUsuario, string contraseña)
        {
            return UsuarioHandler.Auth(nombreUsuario, contraseña);
        }
    }
}
