namespace ProyectoFinalWebAPP.Model
{
    public class ProductoVendido : IId
    {
        private long _id;
        private long _idProducto;
        private int _stock;
        private long _idVenta;

        public long Id { get { return _id; } set { _id = value; } }
        public long IdProducto { get { return _idProducto; } set { _idProducto = value; } }
        public int Stock { get { return _stock; } set { _stock = value; } }
        public long IdVenta { get { return _idVenta; } set { _idVenta = value; } }

        public ProductoVendido()
        {
            _id = 0;
            _idProducto = 0;
            _stock = 0;
            _idVenta = 0;
        }

        public ProductoVendido(long id, long idProducto, int stock, long idVenta)
        {
            _id = id;
            _idProducto = idProducto;
            _stock = stock;
            _idVenta = idVenta;
        }
    }
}
