using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syscome.ControlEmpleados.Entidades;
using Syscome.ControlEmpleados.BL;


namespace ControlDeEmpleados.Controllers
{
    public class frm_MantenimientoDeEmpleadosController : Controller
    {
        String _conn = System.Configuration.ConfigurationManager.ConnectionStrings["QuickfatControlEmplEntities"].ConnectionString;
        BLmaempl _db = new BLmaempl();
        // GET: frm_MantenimientoDeEmpleados
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(maEmpl _m)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (_m.autocod < 1)
            {
                var resp = 0;
            try
            {
                if(_m.nomduno == null){
                    _m.nomduno = "";
                }
                if (_m.nomddos == null)
                {
                    _m.nomddos = "";
                }
                if(_m.nomdtres == null){
                    _m.nomdtres = "";
                }
                if(_m.nomdcuat == null){
                    _m.nomdcuat = "";
                }

                if (_m.dirduno == null)
                {
                    _m.dirduno = "";
                }
                if (_m.dirddos == null)
                {
                    _m.dirddos = "";
                }
                if (_m.dirdtres == null)
                {
                    _m.dirdtres = "";
                }
                if (_m.dirdcuat == null)
                {
                    _m.dirdcuat = "";
                }

                if (_m.parentuno == null)
                {
                    _m.parentuno = "";
                }

                if (_m.parentdos == null)
                {
                    _m.parentdos = "";
                }
                if (_m.parenttres == null)
                {
                    _m.parenttres = "";
                }
                if (_m.parentcuat == null)
                {
                    _m.parentcuat = "";
                }
                _m.codturno = 1;
                _m.codplanilla = 1;
                _m.retirado = 1;
                _m.coddespido = 1;
                _m.tipcontrato = "";
                _m.observaciones = "";
                _m.codigoplan = "";
                _m.sindicato = 1;
                _m.foto = "IMG-2020-10-30.jpg";
                _m.tipplan = 1;
                _m.codlin = "";
                _m.jefim = "";
                _m.tarjeta = "";
                _m.no_marca = 1;
                _m.cent_cost = "";
                _m.aliment = Convert.ToDecimal(12.5);
                _m.viaticos = Convert.ToDecimal(12.5);
                _m.comunica = Convert.ToDecimal(12.5);
                _m.renta_r = 12;
                _m.discapa_c = "";
                _m.t_discapa = 1;
                _m.idtd = "";
                _m.codcep = "";
                _m.cal_meses = 1;
                _m.tipo_pago = 1;
                _m.canal_dist = 1;
                _m.cent_cost = "";
                _m.codcep = "";
                _m.codsucursal = "";
                _m.discapa_c = "";
                _m.idtd = "";
                _m.jefim = "";
                _m.tarjeta = "";
                _m.tipcuentab = "";
                Convert.ToInt32(_m.genero);
                Convert.ToInt32(_m.est_civil);
                Convert.ToString(_m.tipcuentab);
                Convert.ToInt32(_m.no_isss);
                Convert.ToInt32(_m.no_afp);
                Convert.ToInt32(_m.no_renta);
                Convert.ToDecimal(_m.sueldo);
                Convert.ToInt32(_m.activo);
                Convert.ToInt32(_m.externo);
                Convert.ToDecimal(_m.renta_acum);
                Convert.ToDecimal(_m.deveng_acum);
                Convert.ToDecimal(_m.dieta);
                Convert.ToInt32(_m.centro_ope);
                Convert.ToInt32(_m.codigo_car);
                DateTime fechaComparar = new DateTime(1753, 1, 1);
                int fechaRespuesta1 = DateTime.Compare(fechaComparar, _m.fechaduno);
                if (fechaRespuesta1 > 0)
                {
                    _m.fechaduno = fechaComparar;
                }
                int fechaRespuesta2 = DateTime.Compare(fechaComparar, _m.fechaddos);
                if (fechaRespuesta2 > 0)
                {
                    _m.fechaddos = fechaComparar;
                }

                int fechaRespuesta3 = DateTime.Compare(fechaComparar, _m.fechadtres);
                if (fechaRespuesta3 > 0)
                {
                    _m.fechadtres = fechaComparar;
                }

                int fechaRespuesta4 = DateTime.Compare(fechaComparar, _m.fechadcuat);
                if (fechaRespuesta4 > 0)
                {
                    _m.fechadcuat = fechaComparar;
                }

                _db.guardarEmpleado(_m, _conn);
                resp = 1;
                if (resp == 1)
                {
                    return Content("<script language='javascript' type='text/javascript'>if(confirm('Guardado Con exito')){window.location.replace('/frm_MantenimientoDeEmpleados')}else{window.location.replace('/frm_MantenimientoDeEmpleados')}</script>");
                }

                return View();
            }
            catch (Exception e)
            {
                resp = 0;
            }
            }
            else if (_m.autocod >= 1)
            {
                var resp = 0;
            try
            {
                    if (_m.nomduno == null)
                    {
                        _m.nomduno = "";
                    }
                    if (_m.nomddos == null)
                    {
                        _m.nomddos = "";
                    }
                    if (_m.nomdtres == null)
                    {
                        _m.nomdtres = "";
                    }
                    if (_m.nomdcuat == null)
                    {
                        _m.nomdcuat = "";
                    }

                    if (_m.dirduno == null)
                    {
                        _m.dirduno = "";
                    }
                    if (_m.dirddos == null)
                    {
                        _m.dirddos = "";
                    }
                    if (_m.dirdtres == null)
                    {
                        _m.dirdtres = "";
                    }
                    if (_m.dirdcuat == null)
                    {
                        _m.dirdcuat = "";
                    }

                    if (_m.parentuno == null)
                    {
                        _m.parentuno = "";
                    }

                    if (_m.parentdos == null)
                    {
                        _m.parentdos = "";
                    }
                    if (_m.parenttres == null)
                    {
                        _m.parenttres = "";
                    }
                    if (_m.parentcuat == null)
                    {
                        _m.parentcuat = "";
                    }
                    _m.codturno = 1;
                _m.codplanilla = 1;
                _m.retirado = 1;
                _m.coddespido = 1;
                _m.tipcontrato = "";
                _m.observaciones = "";
                _m.codigoplan = "";
                _m.sindicato = 1;
                _m.foto = "IMG-2020-10-30.jpg";
                _m.tipplan = 1;
                _m.codlin = "ddd";
                _m.jefim = "";
                _m.tarjeta = "";
                _m.no_marca = 1;
                _m.cent_cost = "";
                _m.aliment = Convert.ToDecimal(12.5);
                _m.viaticos = Convert.ToDecimal(12.5);
                _m.comunica = Convert.ToDecimal(12.5);
                _m.renta_r = 12;
                _m.discapa_c = "";
                _m.t_discapa = 1;
                _m.idtd = "";
                _m.codcep = "";
                _m.cal_meses = 1;
                _m.tipo_pago = 1;
                _m.canal_dist = 1;
                _m.cent_cost = "";
                _m.codcep = "";
                _m.codsucursal = "";
                _m.discapa_c = "";
                _m.idtd = "";
                _m.jefim = "";
                _m.tarjeta = "";
                _m.tipcuentab = "";
                Convert.ToInt32(_m.genero);
                Convert.ToInt32(_m.est_civil);
                Convert.ToString(_m.tipcuentab);
                Convert.ToInt32(_m.no_isss);
                Convert.ToInt32(_m.no_afp);
                Convert.ToInt32(_m.no_renta);
                Convert.ToDecimal(_m.sueldo);
                Convert.ToInt32(_m.activo);
                Convert.ToInt32(_m.externo);
                Convert.ToDecimal(_m.renta_acum);
                Convert.ToDecimal(_m.deveng_acum);
                Convert.ToDecimal(_m.dieta);
                Convert.ToInt32(_m.centro_ope);
                Convert.ToInt32(_m.codigo_car);
                Convert.ToDateTime(_m.fecha_emision_acta);
                Convert.ToDateTime(_m.fecha_ing);
                Convert.ToDateTime(_m.fecha_nac);
                Convert.ToDateTime(_m.fecha_ret);
                Convert.ToDateTime(_m.fecha_ultvac);
                Convert.ToDateTime(_m.fechadcuat);
                Convert.ToDateTime(_m.fechaddos);
                Convert.ToDateTime(_m.fechadtres);
                Convert.ToDateTime(_m.fechadui);
                Convert.ToDateTime(_m.fechaduno);
                    DateTime fechaComparar = new DateTime(1753, 1, 1);
                    int fechaRespuesta1 = DateTime.Compare(fechaComparar, _m.fechaduno);
                    if (fechaRespuesta1 > 0)
                    {
                        _m.fechaduno = fechaComparar;
                    }
                    int fechaRespuesta2 = DateTime.Compare(fechaComparar, _m.fechaddos);
                    if (fechaRespuesta2 > 0)
                    {
                        _m.fechaddos = fechaComparar;
                    }

                    int fechaRespuesta3 = DateTime.Compare(fechaComparar, _m.fechadtres);
                    if (fechaRespuesta3 > 0)
                    {
                        _m.fechadtres = fechaComparar;
                    }

                    int fechaRespuesta4 = DateTime.Compare(fechaComparar, _m.fechadcuat);
                    if (fechaRespuesta4 > 0)
                    {
                        _m.fechadcuat = fechaComparar;
                    }
                    _db.actualizarEmpleado(_m, _conn);
                resp = 1;
            }
            catch (Exception e)
            {

            }
            }
            return View();
            
        }

        public ActionResult lista()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<maEmpl> lista = _db.obtenerEmpleados(_conn);
            return View(lista);
        }

        public ActionResult detalles(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var obj = _db.obtenerEmpleados(_conn).Where(x => x.autocod == id).FirstOrDefault();
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}