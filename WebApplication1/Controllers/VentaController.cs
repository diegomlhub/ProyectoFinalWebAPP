using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "GetVentas")]
        public List<Venta> Get(long idUsuario)
        {
            return VentaHandler.Get(idUsuario);
        }
    }
}
