using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Syscome.ControlEmpleados.BL;
using ControlDeEmpleados.Models;

namespace ControlDeEmpleados.Controllers
{
    public class frm_ClientesController : Controller
    {

        public string Controlador = "frm_Clientes";

        // GET: CuentasPorCobrar/frm_MantenimientoClientes
        private QuickfatControlEmplEntities1 db = new QuickfatControlEmplEntities1();

        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.nombre = db.sp_RegistroOpcionesPorControlador_Select(Controlador).FirstOrDefault();
            return View();
        }
        public JsonResult OntenerDeptoPorPais(int pais)
        {
            
            var lista = db.sp_DepartamentosPorPais(pais);
            return Json(lista, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<TipoContribuyente> _LstTipoContribuyente = new List<TipoContribuyente>();
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 1, descripcion = "PEQUEÑA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 2, descripcion = "MEDIANA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 3, descripcion = "GRANDE" });
            ViewBag.TipoContribuyente = _LstTipoContribuyente.ToList();
            ViewBag.Departamentos = db.deptos.OrderBy(x => x.nombre).ToList();
            ViewBag.Minicipios = db.municipios.OrderBy(x => x.nombre).ToList();
            ViewBag.Paises = db.sp_pais_Select(0).ToList();
            ViewBag.VendedorLST = db.vendedores.OrderBy(x => x.nombre).ToList();
            ViewBag.TipCliente = db.Tipclientes.OrderBy(x => x.Nombre).ToList();
            IEnumerable<sp_ZonaBusqueda_Result> zona = db.sp_ZonaBusqueda(0, 1).ToList();
            ViewBag.ZONA = new SelectList(zona, "Zona", "Nombre");
            Clientes_Modelo modelo = new Clientes_Modelo();
            return PartialView(modelo);
        }

        [HttpPost]
        public ActionResult Create(Clientes_Modelo modelo)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Paises = db.sp_pais_Select(0).ToList();
            ViewBag.Departamentos = db.deptos.OrderBy(x => x.nombre).ToList();
            ViewBag.Minicipios = db.municipios.ToList();

            ViewBag.VendedorLST = db.vendedores.OrderBy(x => x.nombre).ToList();
            ViewBag.TipCliente = db.Tipclientes.OrderBy(x => x.Nombre).ToList();
            List<TipoContribuyente> _LstTipoContribuyente = new List<TipoContribuyente>();
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 1, descripcion = "PEQUEÑA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 2, descripcion = "MEDIANA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 3, descripcion = "GRANDE" });
            ViewBag.TipoContribuyente = _LstTipoContribuyente.ToList();
            cliente pValidacionRegistro = new cliente();
            try
            {
                string pParametro = "";
                pParametro = modelo.Codigo.Trim();
                pValidacionRegistro = db.cliente.Where(x => x.Codigo.Trim().ToLower() == pParametro.Trim().ToLower()).FirstOrDefault();
                if (pValidacionRegistro == null)
                {
                    pValidacionRegistro = new cliente();
                    pValidacionRegistro.Codigo = "";
                }
            }
            catch { pValidacionRegistro = new cliente(); pValidacionRegistro.Codigo = ""; }

            //if (pValidacionRegistro.Codigo != "")
            //{
            //	ModelState.AddModelError("Codigo", "El codigo de este Cliente ya existe");
            //	return PartialView(modelo);
            //}
            //if (ModelState.IsValid)
            //{
            try
            {


                if (modelo.contrib == true) { modelo.contrib_campo = 1; } else { modelo.contrib_campo = 0; }
                if (modelo.EXENTO == true) { modelo.EXENTO_campo = 1; } else { modelo.EXENTO_campo = 0; }
                if (modelo.DECLAIVA == true) { modelo.DECLAIVA_campo = 1; } else { modelo.DECLAIVA_campo = 0; }
                if (modelo.LOC_EXT == true) { modelo.LOC_EXT_campo = 1; } else { modelo.LOC_EXT_campo = 0; }
                //db.Grupos.Add(Grupostoadd);
                db.sp_Cliente_Insert(modelo.Codigo, modelo.Codempresa, modelo.nombre, modelo.nombre2, modelo.contacto, modelo.puesto,
                    modelo.telef1, modelo.telef2, modelo.EMAIL, modelo.WEB, modelo.direccion, modelo.ZONA, modelo.nacionalidad, modelo.DEPTO, modelo.municip,
                    modelo.TIPO_CLI, modelo.contrib_campo, modelo.EXENTO_campo, modelo.DECLAIVA_campo, modelo.NRC, modelo.giro, modelo.NIT, modelo.clasifi, modelo.LOC_EXT_campo,
                    modelo.LIMCRED, modelo.DIACOBRO, modelo.DIAQUEDAN, modelo.PLAZO, modelo.tipopago, modelo.CTAABO, modelo.CTACAR, modelo.ULTABO,
                    modelo.CUENTA, modelo.Vcomision, modelo.dui, modelo.Classclie, modelo.cod_anterior, modelo.apellido, modelo.ciudad, modelo.fecha_naci, modelo.lugar_naci, modelo.edad,
                    modelo.sexo, modelo.id_estado_civil, modelo.id_profesion, modelo.movil1, modelo.movil2, modelo.email2, modelo.ciudad_trabajo, modelo.municipio_trab, modelo.depto_trab,
                    modelo.telef3, modelo.fax, modelo.email3, modelo.codasoc_respada, modelo.codasoc_respada2, modelo.cate_afiliacion, modelo.fecha_solic, modelo.fecha_aprob,
                    modelo.clase, modelo.acta_numero, modelo.afiliacion_numero, modelo.foto_titular, modelo.foto_pasaporte, modelo.foto_acta, modelo.conta_trab, modelo.comple_solic,
                    modelo.es_indepen, modelo.es_depen, modelo.depende_de, modelo.motivo, modelo.aficiones, modelo.es_hijo, modelo.parentesco_depen, modelo.vendedor, modelo.codigocvc, modelo.ctacvc,
                    modelo.CodPag, modelo.IdPaises, modelo.Area);

                //ControlDeEmpleados.Models.GetIp.bitacora(Controlador, "Guardar", "GuardarClienteCodigo " + modelo.Codigo, User.Identity.GetUserName());
                TempData["exito"] = "exito";
            }
            catch
            {
                TempData["error"] = "exito";
            }
            //db.SaveChanges();
            //
            //ViewData["mensaje"] = "El registro ha sido agregado!";

            //return PartialView("_AlertSuccess");

            //}
            //catch (Exception)
            //{
            //TempData["error"] = "error";
            //ViewData["mensaje"] = "Error en la inserción de Datos!";
            //return PartialView("_AlertFailure");
            //TempData["error"] = "error";
            //}
            //}
            return RedirectToAction("Index");
            //return PartialView(modelo);
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                ViewData["mensaje"] = "Elemento para busqueda vacio";
                return PartialView("_AlertFailure");
            }
            cliente Grupo = db.cliente.Where(X => X.Codigo.ToString() == id).FirstOrDefault();
            if (Grupo == null)
            {
                ViewData["mensaje"] = "El registro que busca no existe";
                return PartialView("_AlertFailure");
            }
            ViewBag.Paises = db.sp_pais_Select(0).ToList();
            ViewBag.Departamentos = db.deptos.OrderBy(x => x.nombre).ToList();
            ViewBag.Minicipios = db.municipios.OrderBy(x => x.nombre).ToList();
            ViewBag.VendedorLST = db.vendedores.OrderBy(x => x.nombre).ToList();
            ViewBag.TipCliente = db.Tipclientes.OrderBy(x => x.Nombre).ToList();
            List<TipoContribuyente> _LstTipoContribuyente = new List<TipoContribuyente>();
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 1, descripcion = "PEQUEÑA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 2, descripcion = "MEDIANA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 3, descripcion = "GRANDE" });
            ViewBag.TipoContribuyente = _LstTipoContribuyente.ToList();
            IEnumerable<sp_ZonaBusqueda_Result> zona = db.sp_ZonaBusqueda(0, 1).ToList();
            ViewBag.ZONA = new SelectList(zona, "Zona", "Nombre", Grupo.ZONA);
            Clientes_Modelo modelo = new Clientes_Modelo();
            modelo.Asignar(Grupo);
            return PartialView(modelo);
        }

        [HttpPost]
        public ActionResult Edit(Clientes_Modelo modelo)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }

            ViewBag.Paises = db.sp_pais_Select(0).ToList();
            ViewBag.Departamentos = db.deptos.OrderBy(x => x.nombre).ToList();
            ViewBag.Minicipios = db.municipios.ToList();
            ViewBag.VendedorLST = db.vendedores.OrderBy(x => x.nombre).ToList();
            ViewBag.TipCliente = db.Tipclientes.OrderBy(x => x.Nombre).ToList();

            List<TipoContribuyente> _LstTipoContribuyente = new List<TipoContribuyente>();
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 1, descripcion = "PEQUEÑA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 2, descripcion = "MEDIANA" });
            _LstTipoContribuyente.Add(new TipoContribuyente { codigo = 3, descripcion = "GRANDE" });
            ViewBag.TipoContribuyente = _LstTipoContribuyente.ToList();
            //if (ModelState.IsValid)
            //{
            var linea = db.cliente.Find(modelo.Codigo);
            if (linea == null)
            {
                ViewData["mensaje"] = "Error en la inserción de Datos, el registro pudo ser eliminado!";
                return PartialView("_AlertFailure");
            }
            //linea.Descripcion = modelo.Descripcion;
            //db.Entry(linea).State = System.Data.Entity.EntityState.Modified;
            try
            {
                if (modelo.contrib == true) { modelo.contrib_campo = 1; } else { modelo.contrib_campo = 0; }
                if (modelo.EXENTO == true) { modelo.EXENTO_campo = 1; } else { modelo.EXENTO_campo = 0; }
                if (modelo.DECLAIVA == true) { modelo.DECLAIVA_campo = 1; } else { modelo.DECLAIVA_campo = 0; }
                if (modelo.LOC_EXT == true) { modelo.LOC_EXT_campo = 1; } else { modelo.LOC_EXT_campo = 0; }
                db.sp_Cliente_Update(modelo.Codigo, modelo.Codempresa, modelo.nombre, modelo.nombre2, modelo.contacto, modelo.puesto,
modelo.telef1, modelo.telef2, modelo.EMAIL, modelo.WEB, modelo.direccion, modelo.ZONA, modelo.nacionalidad, modelo.DEPTO, modelo.municip,
modelo.TIPO_CLI, modelo.contrib_campo, modelo.EXENTO_campo, modelo.DECLAIVA_campo, modelo.NRC, modelo.giro, modelo.NIT, modelo.clasifi, modelo.LOC_EXT_campo,
modelo.LIMCRED, modelo.DIACOBRO, modelo.DIAQUEDAN, modelo.PLAZO, modelo.tipopago, modelo.CTAABO, modelo.CTACAR, modelo.ULTABO,
modelo.CUENTA, modelo.Vcomision, modelo.dui, modelo.Classclie, modelo.cod_anterior, modelo.apellido, modelo.ciudad, modelo.fecha_naci, modelo.lugar_naci, modelo.edad,
modelo.sexo, modelo.id_estado_civil, modelo.id_profesion, modelo.movil1, modelo.movil2, modelo.email2, modelo.ciudad_trabajo, modelo.municipio_trab, modelo.depto_trab
, modelo.telef3, modelo.fax, modelo.email3, modelo.codasoc_respada, modelo.codasoc_respada2, modelo.cate_afiliacion, modelo.fecha_solic, modelo.fecha_aprob,
modelo.clase, modelo.acta_numero, modelo.afiliacion_numero, modelo.foto_titular, modelo.foto_pasaporte, modelo.foto_acta, modelo.conta_trab, modelo.comple_solic,
modelo.es_indepen, modelo.es_depen, modelo.depende_de, modelo.motivo, modelo.aficiones, modelo.es_hijo, modelo.parentesco_depen, modelo.vendedor, modelo.codigocvc, modelo.ctacvc,
modelo.CodPag, modelo.IdPaises, "");
                //QuickFactWeb.Models.GetIp.bitacora(Controlador, "Actualizar", "Actualizar Cliente Codigo" + modelo.Codigo, User.Identity.GetUserName());
                TempData["exitoedit"] = "exito";
                //ViewData["mensaje"] = "El registro ha sido agregado!";
                //return PartialView("_AlertSuccess");
            }
            catch
            {
                TempData["error"] = "error";
                //ViewData["mensaje"] = "Error en la inserción de Datos!";
                //return PartialView("_AlertFailure");
            }
            //}

            //if (ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).FirstOrDefault().Contains("The field Descripcion del Grupo must be a string or array type with a maximum length of") == true)
            //{
            //    ModelState.AddModelError("Descripcion", "La descripcion de este grupo es demasiado grande Max. 125");
            //}
            return RedirectToAction("Index");
            //return PartialView(modelo);
        }


        public ActionResult Lista()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            List<sp_ClienteTotal_Select_Result> lstLista = db.sp_ClienteTotal_Select().ToList();
            List<Clientes_Modelo> modelo = new List<Clientes_Modelo>();
            foreach (var item in lstLista)
            {
                Clientes_Modelo _gp = new Clientes_Modelo();

                _gp.Codigo = item.Codigo;
                _gp.contacto = item.contacto;
                _gp.nombre = item.Nombre;
                _gp.NRC = item.NRC;

                _gp.telef1 = item.telef1;
                _gp.CUENTA = item.cuenta;
                _gp.EMAIL = item.email;

                modelo.Add(_gp);
            }
            return PartialView(modelo.OrderBy(x => x.nombre).ToList());
        }

        public JsonResult getMunicipios(Int32 IdDepartamento)
        {
            
            List<municipios> _Lst = new List<municipios>();
            _Lst = db.municipios.Where(x => x.coddepto == IdDepartamento).ToList();
            return Json(_Lst.OrderBy(x => x.nombre).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarcodigoexistente(string codigo)
        {
            
            var existe = 0;
            var e = db.cliente.Where(x => x.Codigo == codigo).FirstOrDefault();
            try
            {
                if (e != null)
                {
                    existe = 1;
                    if (e.Codigo == "")
                    {
                        existe = 0;
                    }
                }
            }
            catch
            { }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }

        public JsonResult buscarnit(string nit)
        {
            
            var existe = 0;
            var e = db.cliente.Where(x => x.NIT == nit).FirstOrDefault();
            try
            {
                if (e != null)
                {
                    existe = 1;
                    if (e.NIT == "")
                    {
                        existe = 0;
                    }
                }
            }
            catch
            { }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }
        public JsonResult buscardui(string dui)
        {
            
            var existe = 0;
            var e = db.cliente.Where(x => x.dui == dui).FirstOrDefault();
            try
            {
                if (e != null)
                {
                    existe = 1;
                    if (e.dui == "")
                    {
                        existe = 0;
                    }
                }
            }
            catch
            { }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }
        public JsonResult buscarnrc(string nrc)
        {
            
            var existe = 0;
            var e = db.cliente.Where(x => x.NRC == nrc).FirstOrDefault();
            try
            {
                if (e != null)
                {
                    existe = 1;
                    if (e.NRC == "")
                    {
                        existe = 0;
                    }
                }
            }
            catch
            { }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ListaFacturaspendientes(string codigo)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var lista = db.sp_ProveedorClienteConsultaSaldosFactura(2, codigo).ToList();
            if (lista.Count > 0)
            {
                return PartialView(lista);
            }
            else
            {
                return PartialView(lista);
            }


        }

        public JsonResult buscar(string cuenta, string descrip)
        {
            
            bool star = false;
            if (descrip != "")
            {
                var busquedaPorCuenta = db.sp_CatalogoCuentas_Select("", descrip).ToList();
                if (busquedaPorCuenta.Count > 0)
                {

                    return Json(busquedaPorCuenta, JsonRequestBehavior.AllowGet);
                }

            }
            else if (cuenta != "")
            {
                var busquedaPorDescripcion = db.sp_CatalogoCuentas_Select(cuenta, "").ToList();
                if (busquedaPorDescripcion.Count > 0)
                {

                    return Json(busquedaPorDescripcion, JsonRequestBehavior.AllowGet);
                }

            }


            return Json(star, JsonRequestBehavior.AllowGet);

        }
    }
}