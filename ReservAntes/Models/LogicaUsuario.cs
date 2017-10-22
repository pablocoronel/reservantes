using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;

namespace ReservAntes.Models
{
    public class LogicaUsuario
    {

        public List<TipoUsuario> GetTiposDeUs()
        {
            List<TipoUsuario> ListUsuario = new List<TipoUsuario>();
            ListUsuario = ctx.TipoUsuario.ToList();

            return ListUsuario;
        }


        dbReservAntesEntities ctx = new dbReservAntesEntities();

        public void CrearUsuario (Usuario us)
        {
            Usuario NewUsuario = new Usuario();

            NewUsuario.Username = us.Username;
            NewUsuario.Password = us.Password;
            NewUsuario.Email = us.Email;
            NewUsuario.TipoUsuarioId = us.TipoUsuarioId;


            ctx.Usuario.Add(NewUsuario);
            ctx.SaveChanges();

        }

        //Login de usuarios
        public Usuario UsuarioIngresar(Usuario usuario)
        {
            return ctx.Usuario.FirstOrDefault(usu => usu.Username == usuario.Username && usu.Password == usuario.Password);
        }
    }
}