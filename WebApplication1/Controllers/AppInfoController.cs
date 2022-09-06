using Microsoft.AspNetCore.Mvc;

namespace ProyectoFinalWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppInfoController : ControllerBase
    {
        [HttpGet(Name = "TraerNombreDelLocal")]
        public string AppInfo()
        {
            return "Proyecto Final Web Api 1.0";
        }
    }
}
