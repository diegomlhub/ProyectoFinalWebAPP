﻿using Microsoft.AspNetCore.Mvc;
using ProyectoFinalWebAPP.Controllers.DTOS;
using ProyectoFinalWebAPP.Model;
using ProyectoFinalWebAPP.Repository;

namespace ProyectoFinalWebAPP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductoController : ControllerBase
    {
        [HttpGet(Name = "GetProductosByIdUsuario")]
        public List<Producto> Get(long idUsuario)
        {
            return ProductoHandler.Get(idUsuario);
        }

        [HttpDelete]
        public bool Delete([FromBody] int id)
        {
            try
            {
                return ProductoHandler.Delete(id);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPut]
        public bool Update([FromBody] PutProducto producto)
        {
            try
            {
                return ProductoHandler.Update(new Producto
                {
                    Id = producto.Id,
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [HttpPost]
        public bool Add([FromBody] PostProducto producto)
        {
            try
            {
                return ProductoHandler.Add(new Producto
                {                    
                    Descripciones = producto.Descripciones,
                    Costo = producto.Costo,
                    PrecioVenta = producto.PrecioVenta,
                    Stock = producto.Stock,
                    IdUsuario = producto.IdUsuario
                });
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
