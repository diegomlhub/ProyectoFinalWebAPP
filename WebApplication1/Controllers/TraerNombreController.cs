using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalWebAPI.Controllers
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
