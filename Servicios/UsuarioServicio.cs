using Entidades; // Utilizo la capa Entidades para acceder al conector de la BD
using DAO; // Utilizo la capa DAO para manipular los datos de la BD 
using System.Web;

namespace Servicios
{
    public class UsuarioServicio
    {
        UsuarioDAO usuarioDAO;

        public UsuarioServicio(Hospital_Italiano_dbEntities context)
        {
            usuarioDAO = new UsuarioDAO(context);
        }

        public bool GuardarUsuario(Usuario usuario)
        {
            bool estado = usuarioDAO.GuardarUsuario(usuario);
            return estado;
        }

        public void CerrarSession()
        {
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();
            HttpContext.Current.Session.RemoveAll();
        }

        public Usuario obtenerUsuarioPorNombreUsuario(string nombreUsuario)
        {
            return usuarioDAO.obtenerPorNombreDeUsuario(nombreUsuario);
        }
    }
}