namespace ProyectoFinalWebAPP.Model
{
    public class Venta : IId
    {
        private long _id;
        private string _comentarios;

        public long Id { get { return _id; } set { _id = value; } }
        public string Comentarios { get { return _comentarios; } set { _comentarios = value; } }

        public Venta()
        {
            _id = 0;
            _comentarios = String.Empty;
        }

        public Venta(long id, string comentarios)
        {
            _id = id;
            _comentarios = comentarios;
        }
    }
}
