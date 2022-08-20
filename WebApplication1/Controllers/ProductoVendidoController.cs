using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoVendidoController : ControllerBase
    {
        [HttpGet(Name = "GetProductosVendidosByIdUsuario")]
        public List<ProductoVendido> Get(long idUsuario)
        {
            return ProductoVendidoHandler.Get(idUsuario);
        }

        [HttpDelete]
        public bool Eliminar([FromBody] int id)
        {
            try
            {
                return ProductoVendidoHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
      
    }
}
