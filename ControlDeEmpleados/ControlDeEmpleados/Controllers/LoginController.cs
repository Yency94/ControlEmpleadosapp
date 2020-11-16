using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlDeEmpleados.Models;


namespace ControlDeEmpleados.Controllers
{
    public class LoginController : Controller
    {
        QuickfatControlEmplEntities1 db = new QuickfatControlEmplEntities1();
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Index(Users user)
        {
            Encryption enc = new Encryption();
            string passworEncrypt = enc.EncryptKey(user.SecurityStamp);
            var usuarioDb = db.Users.Where(x => x.Email == user.Email).FirstOrDefault();
            if (usuarioDb != null )
            {
                if (passworEncrypt == usuarioDb.PasswordHash)
                {
                    Session["Usuario"] = usuarioDb.NombreCompleto;
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }

        
        public ActionResult CerrarSesion()
        {

            try
            {
                Session["Usuario"] = null;
                return RedirectToAction("Index", "Login");
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}