using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        [HttpGet(Name = "GetUsuario")]
        public Usuario Get(string nombreUsuario)
        {
            return UsuarioHandler.Get(nombreUsuario);            
        }

        [HttpDelete]
        public bool Delete([FromBody] int id)
        {
            try
            {
                return UsuarioHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Update([FromBody] PutUsuario usuario)
        {
            try
            {
                return UsuarioHandler.Update(new Usuario
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Add([FromBody] PostUsuario usuario)
        {
            try
            {
                return UsuarioHandler.Add(new Usuario
                {                   
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    NombreUsuario = usuario.NombreUsuario,
                    Contraseña = usuario.Contraseña,
                    Mail = usuario.Mail
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }

}
