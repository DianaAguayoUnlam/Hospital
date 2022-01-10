using System.ComponentModel.DataAnnotations;

namespace Entidades.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre de usuario es demasiado largo")]
        public string Nombre_usuario { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        //Puede comenzar con A-Z, o a-z, o numeros de 0 a 9    |  finaliza con a-z - A-Z -ó- 0-9   {desde, hasta}
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])[a-zA-Z0-9]{1,}$", ErrorMessage = "La contraseña debe contener al menos una letra mayúscula, una letra minúscula y un número")]
        [MinLength(6, ErrorMessage = "Su contraseña debe tener 6 digitos como minimo")]
        public string Password { get; set; }
    }
}
