using ReservAntes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservAntes.Controllers
{
    public class UsuarioController : Controller
    {
        dbReservantesEntities ctx = new dbReservantesEntities();

        LogicaUsuario LogUs = new LogicaUsuario();



        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Usuario/Create
        public ActionResult CreateUser()
        {
            if (Session["usuarioTipo"] != null)
            {
                if (Session["usuarioTipo"].ToString() != "1")
                {
                    Response.Redirect("../home/Index");
                }
                
            }

            ViewBag.ListUsuario = LogUs.GetTiposDeUs();
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult CreateUser(Usuario user)
        {

            ViewBag.ListUsuario = LogUs.GetTiposDeUs();

            {
                if (ModelState.IsValid)
                    
                    this.LogUs.CrearUsuario(user);


                return View("../Home/Index");
            }

        }
         
        

        // GET: Usuario/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id)
        {
            this.LogUs.EliminarUs(id);

            return RedirectToAction("../Admin/VerUsuarios");
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       }
}


