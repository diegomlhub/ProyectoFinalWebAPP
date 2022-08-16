namespace ProyectoFinalWebAPP.Model
{
    public class Usuario : IId
    {
        private long _id;
        private string _nombre;
        private string _apellido;
        private string _nombreUsuario;
        private string _contraseña;
        private string _mail;

        public long Id { get { return _id; } set { _id = value; } }
        public string Nombre { get { return _nombre; } set { _nombre = value; } }
        public string Apellido { get { return _apellido; } set { _apellido = value; } }
        public string NombreUsuario { get { return _nombreUsuario; } set { _nombreUsuario = value; } }
        public string Contraseña { get { return _contraseña; } set { _contraseña = value; } }
        public string Mail { get { return _mail; } set { _mail = value; } }

        public Usuario()
        {
            _id = 0;
            _nombre = String.Empty;
            _apellido = String.Empty;
            _nombreUsuario = String.Empty;
            _contraseña = String.Empty;
            _mail = String.Empty;
        }

        public Usuario(long id, string nombre, string apellido, string nombreUsuario, string contraseña, string mail)
        {
            _id = id;
            _nombre = nombre;
            _apellido = apellido;
            _nombreUsuario = nombreUsuario;
            _contraseña = contraseña;
            _mail = mail;
        }
    }
}
