using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPI.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;
using System.Linq.Expressions;

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

        [HttpPost(Name = "CargarVenta")]
        public bool AddVenta([FromBody] List <PostVenta> listaProductos, string comentarios)
        {
            try
            {
                if (listaProductos.Count > 0)
                {
                    VentaHandler.Add(new Venta { Comentarios = comentarios }); //Se carga venta "Coemntarios"

                    ProductoVendido productoVendido = new ProductoVendido();

                    foreach (PostVenta postProducto in listaProductos) //Se cargan los Procutos Vendidos
                    {
                        productoVendido.Stock = postProducto.Stock;
                        productoVendido.IdProducto = postProducto.Id;
                        productoVendido.IdVenta = VentaHandler.IdUltimaVenta(); //Id de venta cargada
                        ProductoVendidoHandler.Add(productoVendido);
                        ProductoHandler.UpdateStock(postProducto.Id, postProducto.Stock); //Actualizo stock del producto
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

