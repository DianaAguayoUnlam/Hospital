using Entidades;
using Entidades.Enum;
using Entidades.ViewModel;
using Servicios;
using System;
using System.Web.Mvc;

namespace Hospital_Italiano.Controllers
{
    public class HomeController : Controller
    {
        UsuarioServicio usuarioServicio;

        public HomeController()
        {
            Hospital_Italiano_dbEntities context = new Hospital_Italiano_dbEntities();
            usuarioServicio = new UsuarioServicio(context);
        }
        public ActionResult Index()
        {
            if (string.IsNullOrEmpty((string)Session["nombre_usuario"]))
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid)
            {
                TempData["mensaje_error"] = "Por favor, ingrese sus datos correctamente.";
            }
            else
            {
                //Obtengo el usuario con todos sus datos para validar si los datos ingresados son correctos
                Usuario usuarioEncontrado = usuarioServicio.obtenerUsuarioPorNombreUsuario(loginVM.Nombre_usuario);

                //Valido si es el nombre del usuario o si es la contraseña la que esta incorrecta
                if (usuarioEncontrado == null)
                {
                    TempData["mensaje_error"] = "El usuario no existe";
                    return RedirectToAction("Login");
                }
                else if (usuarioEncontrado.Id_Estado == (int)EstadoUsuario.Habilitado)
                {
                    if (usuarioEncontrado.Password != loginVM.Password)
                    {
                        TempData["mensaje_error"] = "La contraseña es incorrecta";
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        //Los datos ingresados son correctos, se permite el ingreso a la web.   
                        Session["nombre_usuario"] = loginVM.Nombre_usuario;
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    //No está habilitado el usuario
                    TempData["mensaje_error"] = "No estás habilitado para ingresar. Por favor, contactanos para evaluar tu situación";
                    return RedirectToAction("Login");
                }
            }
            return RedirectToAction("Login");
        }


        [HttpGet]
        public ActionResult AltaUsuario()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult AltaUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return View("Login");
            }
            else
            {
                //Obtengo el usuario con todos sus datos para validar si los datos ingresados son correctos
                Usuario usuarioEncontrado = usuarioServicio.obtenerUsuarioPorNombreUsuario(usuario.Nombre_usuario);

                if (usuarioEncontrado == null)
                {
                    //Procedo a guardar el usuario en la bd
                    bool estadoRegistro = usuarioServicio.GuardarUsuario(usuario);

                    if (estadoRegistro)
                    {
                        //Logueo 
                        Session["nombre_usuario"] = usuario.Nombre_usuario;
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        //Mostrar mensaje de que ocurrio un error al guardar
                        TempData["mensaje_error"] = "Ocurrio un error al registrarse. Por favor, intentelo nuevamente.";
                        return RedirectToAction("Login");
                    }
                }
                else
                {
                    //Ya existe un usuario con esos datos
                    TempData["mensaje_error"] = "Ya existe ese nombre de usuario";
                    return RedirectToAction("Login");
                }
            }
        }

        public ActionResult CerrarSession()
        {
            usuarioServicio.CerrarSession();
            return RedirectToAction("Login");
        }
    }
}