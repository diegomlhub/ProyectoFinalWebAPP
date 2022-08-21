namespace ProyectoFinalWebAPI.Controllers.DTOS
{
    public class PostProductoVendido
    {
        public long IdProducto { get; set; }
        public int Stock { get; set; }
        public long IdVenta { get; set; }
    }
}
