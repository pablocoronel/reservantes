using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class LoginController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();
        Models.LogicaUsuario LogUs = new Models.LogicaUsuario();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["usuarioId"].ToString() != String.Empty)
            {
                Response.Redirect("../Home/Index");
            }
            return View(    );
        }

        [HttpPost]
        public ActionResult Login(Usuario usuario)
        {
            Usuario existeUsuario = LogUs.UsuarioIngresar(usuario);

            if (existeUsuario != null)
            {
                Session["usuarioId"] = existeUsuario.Id;
                Session["usuarioTipo"] = existeUsuario.TipoUsuarioId;
                Session["usuarioNombre"] = existeUsuario.Username;
                Response.Redirect("../Home/Index");
            }
            return View("../Home/Index");
        }

        //Cerrar sesion
        [HttpGet]
        public void Logout()
        {
            Session.Abandon();
            Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            Response.Redirect("../Home/Index");
        }

    }
}
