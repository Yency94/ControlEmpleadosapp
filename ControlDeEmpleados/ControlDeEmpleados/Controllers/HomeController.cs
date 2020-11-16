using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlDeEmpleados.Controllers
{
    
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult About()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}