using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPI.Controllers.DTOS;
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

        [HttpDelete]
        public bool Delete([FromBody] int id)
        {
            try
            {
                return VentaHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Update([FromBody] PutVenta venta)
        {
            try
            {
                return VentaHandler.Update(new Venta
                {
                    Id = venta.Id,
                    Comentarios = venta.Comentarios                    
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Add([FromBody] PostVenta venta)
        {
            try
            {
                return VentaHandler.Add(new Venta
                {
                    Comentarios = venta.Comentarios
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

