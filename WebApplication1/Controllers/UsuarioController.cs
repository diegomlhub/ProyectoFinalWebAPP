﻿using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;

namespace ProyectoFinalWebAPP.Controllers
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
        public void ModificarUsuario([FromBody] PutUsuario usuario)
        {

        }

        [HttpPost]
        public void ActualizarUsuario([FromBody] PostUsuario usuario)
        {

        }

    }

}
