using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ControlDeEmpleados.Models;
using Syscome.ControlEmpleados.Entidades;
using Syscome.ControlEmpleados.BL;

namespace ControlDeEmpleados.Controllers
{
    public class Frm_HorariosController : Controller
    {
        String _conn = System.Configuration.ConfigurationManager.ConnectionStrings["QuickfatControlEmplEntities"].ConnectionString;
        blHorarios _db = new blHorarios();
        // GET: Frm_Horarios
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult lista()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<Horarios> lista = _db.ObtenerHorarios(_conn);
            return View(lista);
        }

        // POST: Frm_Horarios/Create
        [HttpPost]
        public ActionResult Create(Horarios model)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                // TODO: Add insert logic here
                _db.AgregarHorario(model, _conn);
                resp = 1;

            }
            catch
            {
                resp = 0;
            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult detalles(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = _db.ObtenerHorarios(_conn).Where(x => x.idHorarios == id).FirstOrDefault();
           return Json(obj, JsonRequestBehavior.AllowGet);
        }

        // POST: Frm_Horarios/Edit/5
        [HttpPost]
        public ActionResult Edit(Horarios model)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                // TODO: Add insert logic here
                _db.ActualizarHorario(model, _conn);
                resp = 1;

            }
            catch
            {
                resp = 0;
            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        // GET: Frm_Horarios/Delete/5
        public ActionResult Delete(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        // POST: Frm_Horarios/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
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
