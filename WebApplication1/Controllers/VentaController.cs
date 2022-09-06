using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPI.Controllers.DTOS;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;
using System.Linq.Expressions;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VentaController : ControllerBase
    {
        [HttpGet(Name = "TraerVentas")]
        public List<Venta> Get(long idUsuario)
        {
            return VentaHandler.Get(idUsuario);
        }

        [HttpDelete(Name = "EliminarVenta")]
        public bool Delete([FromBody] int idVenta)
        {
            try
            {
                if (idVenta <= VentaHandler.IdUltimaVenta() & idVenta > 0) //Realizo tarea si idVenta ingresado es mayor a cero y menor o igual al ultimo dVenta
                {
                    List<ProductoVendido> listaProductosVendidos = ProductoVendidoHandler.GetByIdVenta(idVenta); //Traigo todos los ProductosVendidos con idVenta Ingresado.

                    foreach (ProductoVendido productoVendido in listaProductosVendidos) //Se cargan los Procutos Vendidos en la lista
                    {
                        ProductoHandler.UpdateStock(productoVendido.IdProducto, -productoVendido.Stock); //Actualizo stock del producto (Reutilizo actualizar stock con un menos al stock para que se agregue positivamente)
                        ProductoVendidoHandler.Delete(productoVendido.Id); //Elimino el registo del produco vendido                        
                    }
                    VentaHandler.Delete(idVenta); //Elimino la venta.
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
                    VentaHandler.Add(new Venta { Comentarios = comentarios }); //Se carga venta "Comentarios"

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

