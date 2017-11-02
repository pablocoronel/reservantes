﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if(Session["usuarioTipo"] != null)
{
                switch (Session["usuarioTipo"])
                {
                    case 1:
                        Response.Redirect("/Admin/Index/");
                        break;

                    case 2:
                        Response.Redirect("/Cliente/Index/");
                        break;

                    case 3:
                        Response.Redirect("/Restaurante/Index/");
                        break;

                    default:
                        Response.Redirect("/Home/Index/");
                        break;
                }

            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}