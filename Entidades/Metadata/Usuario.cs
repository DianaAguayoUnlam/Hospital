using System.ComponentModel.DataAnnotations;

namespace Entidades
{
    // Definimos una clase parcial que hace referencia a la entidad "Usuario" para poder agregarle validaciones en el backend
    [MetadataType(typeof(UsuarioMetadata))]

    public partial class Usuario
    {
    }

    public class UsuarioMetadata
    {
        [Required(ErrorMessage = "Por favor, ingrese un nombre")]
        [StringLength(100, ErrorMessage = "El nombre no puede tener más de 100 letras")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese un usuario")]
        public string Nombre_usuario { get; set; }

        [Required(ErrorMessage = "Por favor, ingrese una contraseña")]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{1,}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        public string Password { get; set; }
    }
}
