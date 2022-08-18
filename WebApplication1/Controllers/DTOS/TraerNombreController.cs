using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalWebAPP.Controllers.DTOS
{
    [ApiController]
    [Route("[controller]")]
    public class TraerNombreController : ControllerBase
    {
        [HttpGet]
        public string TraerNombre()
        {
            return "Proyecto Final Web Api";
        }
    }
}
