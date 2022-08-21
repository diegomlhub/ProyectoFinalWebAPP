namespace ProyectoFinalWebAPI.Controllers.DTOS
{
    public class PostProductoVendido
    {        
        public int Stock { get; set; }
        public long IdProducto { get; set; }
        public long IdVenta { get; set; }
    }
}
