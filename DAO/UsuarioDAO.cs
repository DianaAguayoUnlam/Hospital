using Entidades;
using Entidades.Enum;
using System.Linq;

namespace DAO
{
    public class UsuarioDAO
    {
        Hospital_Italiano_dbEntities contexto;

        public UsuarioDAO(Hospital_Italiano_dbEntities context)
        {
            contexto = context;
        }

        public bool GuardarUsuario(Usuario user)
        {
            user.Id_Rol = (int) RolUsuario.Alumno;
            user.Id_Estado = (int) EstadoUsuario.Habilitado;
            
            contexto.Usuario.Add(user);
            int numContext = contexto.SaveChanges();

            return numContext > 0;
        }

        public Usuario obtenerPorNombreDeUsuario(string nombreUsuario)
        {
            return contexto.Usuario.Where(o => o.Nombre_usuario == nombreUsuario).FirstOrDefault();
        }
    }
}