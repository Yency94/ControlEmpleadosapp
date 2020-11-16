using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syscome.ControlEmpleados.BL;
using ControlDeEmpleados.Models;
using Syscome.ControlEmpleados.Entidades;

namespace ControlDeEmpleados.Controllers
{
    
    public class frm_AgendaController : Controller
    {
        QuickfatControlEmplEntities1 db = new QuickfatControlEmplEntities1();
        String _conn = System.Configuration.ConfigurationManager.ConnectionStrings["QuickfatControlEmplEntities"].ConnectionString;
        BLmaempl _db = new BLmaempl();
        // GET: frm_Agenda
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            var fecha = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("D2");
            var fechaConvertida = Convert.ToDateTime(fecha);
            ViewBag.listaZona = db.Zona.ToList();
            ViewBag.listaAgenda = db.Agenda.Where(x => x.Fecha == fechaConvertida).ToList();
            ViewBag.conteo = db.Agenda.Where(x => x.Fecha == fechaConvertida).ToList().Count();
            return View();
        }

        public ActionResult listaCliente(Int32 tipoBusqueda, String codigo, String nombre)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var lista = db.sp_Cliente_Select(tipoBusqueda, codigo, nombre);
           return Json(lista, JsonRequestBehavior.AllowGet); 
        }

        public ActionResult listaEmpleado()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var lista = _db.obtenerEmpleados(_conn);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult guardarAgenda(Int32 zona, String codigoCliente, String detalle, DateTime fecha, Int32 idEmpleado, Int32 prioridad )
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                var obj = db.sp_Agenda_Insert(zona, codigoCliente, detalle, fecha, idEmpleado, prioridad);
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult busquedaEmpleado(String parametroBusqueda)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var lista = _db.obtenerEmpleados(_conn).Where(x => parametroBusqueda.All(s => x.nombre.ToLower().Contains(s) || x.apellido.ToLower().Contains(s) || x.dui.ToLower().Contains(s)));
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult agregarCliente(String nombre, String telefono, String direccion, Int32 zona, String nrc, String nit)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int rsp = 0;
            try
            {
                var obj = db.sp_Cliente_InsertModal(nombre, telefono, direccion, zona, nrc, nit);
                rsp = 1;
            }
            catch (Exception)
            {
                rsp = 0;
            }

            return Json(rsp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult eliminarAgenda(Int32 id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                var obj = db.sp_Agenda_Delete(id);
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }
            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult busquedaAgendaFecha(DateTime fecha)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            Convert.ToDateTime(fecha);
            var obj = db.Agenda.Where(x => x.Fecha == fecha).ToList();
            var conteo = db.Agenda.Where(x => x.Fecha == fecha).ToList().Count();
            return Json(new { obj, conteo }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult conteoAgendas()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var fecha = DateTime.Now.Year + "-" + DateTime.Now.Month.ToString("00") + "-" + DateTime.Now.Day.ToString("D2");
            var fechaConvertida = Convert.ToDateTime(fecha);
            var conteo = db.Agenda.Where(x => x.Fecha == fechaConvertida).Count();
            return Json(conteo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult actualizarCodigo(String codigo, String nrc)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                var obj = db.sp_Cliente_Update_Codigo(codigo, nrc);
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult verificarNrc(String nrc)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                var obj = db.cliente.Where(x => x.NRC == nrc).ToList().FirstOrDefault();
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult validarCodigo(String codigo)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            int resp = 0;
            try
            {
                var obj = db.cliente.Where(x => x.Codigo == codigo).ToList().FirstOrDefault();
                resp = 1;
            }
            catch (Exception)
            {
                resp = 0;
                throw;
            }

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        public ActionResult listaAgendaDet()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult busquedaAgendaDet(Int32 codigo, DateTime fechaDesde, DateTime fechaHasta)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = db.sp_ObtenerAgendaporEmpleado(codigo, fechaDesde, fechaHasta);
                return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult reporteEstadisticoAgendas()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        public ActionResult reportePorEmpleado(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = db.sp_EstadisticoporNombre(fechaDesde, fechaHasta).ToList();

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult reporteFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = db.sp_EstadisticoporFecha(fechaDesde, fechaHasta).ToList();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}
