using ReservAntes.Models;
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
        LogicaUsuario LogUs = new LogicaUsuario();

        [HttpGet]
        public ActionResult Login()
        {
            if (Session["usuarioId"] != null)
            {
                Response.Redirect("../Home/Index");
            }
            return View();
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
                switch (Convert.ToInt32(Session["usuarioTipo"]))
           
                //switch (Session["usuarioTipo"]) da error
                {
                    case 1:
                        Response.Redirect("../Admin/Index");
                        break;
                    case 2:
                        Response.Redirect("../Cliente/Index");
                        break;
                    case 3:
                        Response.Redirect("../Restaurante/Index");
                        break;

                    default:
                        Response.Redirect("../Home/Index");
                        break;
                }

                
            }

            //si no hay session
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
