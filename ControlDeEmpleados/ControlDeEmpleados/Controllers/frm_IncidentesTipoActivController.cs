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
    public class frm_IncidentesTipoActivController : Controller
    {
        String _conn = System.Configuration.ConfigurationManager.ConnectionStrings["QuickfatControlEmplEntities"].ConnectionString;
        BLIncidentes _db = new BLIncidentes();

        // GET: frm_IncidentesTipoActiv
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(Incidentes incidente)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            //Guardar en BD

            var resp = 0;
            try
            {
                _db.AgregarIncidente(incidente, _conn);
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
            }
          
            return Json(resp,JsonRequestBehavior.AllowGet);
        }

        public ActionResult lista()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int codigo = 0;
            int TipoProc = 1;
            if(TipoProc == 1){
                List<Incidentes> _lista1 = _db.ObtenerIncidentes(codigo,TipoProc, _conn);
            }
           
            List<Incidentes> _lista = _db.ObtenerIncidentes(codigo, TipoProc, _conn);
            return View(_lista);
        }
        public ActionResult detalles(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = _db.ObtenerIncidentes(0, 1, _conn).Where(x => x.idIncidentesTipoActiv == id).FirstOrDefault();
           ViewBag.fecha = obj.FechaAdicionActiv.Year + "-" + obj.FechaAdicionActiv.Month + "-" + obj.FechaAdicionActiv.Day;
         

            return Json(obj , JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActualizarIncidente(int id, string descripcion, DateTime fecha)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var resp = 0;
            try
            {
                _db.ActualizarIncidente(id, descripcion,Convert.ToDateTime(fecha), _conn);
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }
    }
}