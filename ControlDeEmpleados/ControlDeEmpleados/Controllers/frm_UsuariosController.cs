using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Data.Entity;
using System.Net;
using ControlDeEmpleados.Models;
using System.Threading.Tasks;

namespace ControlDeEmpleados.Controllers
{
    public class frm_UsuariosController : Controller
    {
        QuickfatControlEmplEntities1 db = new QuickfatControlEmplEntities1();
        // GET: frm_Usuarios
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.nombre = db.sp_RegistroOpcionesPorControlador_Select("frm_Usuarios").FirstOrDefault();
            //if (obj.customAccsses(user, "frm_Usuarios"))
            //{
            return View();
        }

        public ActionResult Lista()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            return View(db.Users.ToList());
        }

        // GET: frmUsuarios/Registro
        public ActionResult Registro()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            ViewBag.Superior = new SelectList(db.Users, "UserName", "UserName");
            ///ViewBag.preguntas = db.PreguntasAlternas.ToList();
            //IEnumerable<sp_Bodega_Select_Result> bodega = db.sp_Bodega_Select(0).ToList();
            //ViewBag.IdBodega = new SelectList(bodega, "id", "descrip");
            UsuariosRegistroViewModel model = new UsuariosRegistroViewModel();
            return View(model);
        }

        // POST: /frmUsuarios/Registro
        [HttpPost]
        public async Task<ActionResult> Registro(UsuariosRegistroViewModel users)
        {
            //IEnumerable<sp_Bodega_Select_Result> bodega = db.sp_Bodega_Select(0).ToList();
            //ViewBag.IdBodega = new SelectList(bodega, "id", "descrip");
            ViewBag.Superior = new SelectList(db.Users, "UserName", "UserName");
            if (ModelState.IsValid)
            {
                try
                {
                    if (users.RolesSeleccionados == null)
                    {
                        string[] rolSelect = { "CA" };
                        users.RolesSeleccionados = rolSelect;
                    }

                    Encryption _Encryption = new Encryption();
                    String EncryptedText = "";
                    EncryptedText = _Encryption.EncryptKeySecond(users.Clave);

                    users.SecurityPrevious = EncryptedText;

                    var user = new Users
                    {
                        UserName = users.Nombre,
                        Email = users.Nombre,
                        NombreCompleto =
                        users.NombreCompleto,
                        Superior = users.Superior,
                        ClaveAlterna = users.ClaveAlterna,
                        SecurityPrevious =
                        users.SecurityPrevious,
                        IdPreguntaAlterna = users.idTipoPregunta,
                        SuperAdmin = users.TipoUsuario,
                        LockoutEnabled = users.LockoutEnabled,
                        EsAdministrador = users.EsAdministrador,
                        IdBodega =
                        users.IdBodega
                    };
                    try
                    {
                        db.sp_UserInsert(user.UserName, user.UserName, user.NombreCompleto, user.Superior, user.ClaveAlterna, user.SecurityPrevious, user.SecurityPrevious, user.IdPreguntaAlterna, user.SuperAdmin, user.LockoutEnabled, user.EsAdministrador, user.IdBodega, user.PhoneNumber);
                        TempData["exito"] = "Exito";
                        //QuickFactWeb.Models.GetIp.bitacora("frm_UsuariosController", "Insertar", "Nuevo usuario creado: " + user.NombreCompleto, User.Identity.GetUserName());
                        //return PartialView(Modelado(new ApplicationUser()));
                        //return PartialView("_AlertSuccess");
                        return RedirectToAction("Index");
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine(ex.InnerException.Message);
                    }                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            var Molde = new UsuariosRegistroViewModel();
            Molde = Modelado(new ApplicationUser() { UserName = users.Nombre });
            //Molde.RolesSeleccionados = users.RolesSeleccionados;
            // If we got this far, something failed, redisplay form
            TempData["Error"] = "Error";
            return RedirectToAction("Index");
        }

        //// GET: frmUsuarios/Modificar/5
        public ActionResult Modificar(string id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = db.Users.Where(x => x.Id == id).FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            UsuariosModificar model = new UsuariosModificar();
            model.Id = user.Id;
            model.NombreCompleto = user.NombreCompleto;
            model.LockoutEnabled = user.LockoutEnabled;
            model.Nombre = user.UserName;
            model.Superior = user.Superior;
            model.ClaveAlterna = user.ClaveAlterna;
            model.TipoUsuario = user.SuperAdmin.GetValueOrDefault();
            model.Telefono = user.PhoneNumber;
            model.EsAdministrador = user.EsAdministrador;

            ViewBag.Superior = new SelectList(db.Users, "UserName", "UserName");
            ViewBag.Superiorusuario = user.Superior;
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Modificar(UsuariosModificar Usuario)
        {
            ViewBag.Superior = new SelectList(db.Users, "UserName", "UserName");
            var Molde = new UsuariosModificar();
            //IEnumerable<sp_Bodega_Select_Result> bodega = db.sp_Bodega_Select(0).ToList();
            //ViewBag.IdBodega = new SelectList(bodega, "id", "descrip", Usuario.IdBodega);
            //Molde = ModeladoModificar(new ApplicationUser() { UserName = Usuario.Nombre, Email = Usuario.Nombre, NombreCompleto = Usuario.NombreCompleto, Superior = Usuario.Superior, IdPreguntaAlterna = Usuario.idTipoPregunta, ClaveAlterna = Usuario.ClaveAlterna, SuperAdmin = Usuario.TipoUsuario, LockoutEnabled = Usuario.LockoutEnabled, IdBodega = Usuario.IdBodega });
           
            Molde = ModeladoModificar(new ApplicationUser()
            {
                UserName = Usuario.Nombre,
                Email = Usuario.Nombre,
                NombreCompleto = Usuario.NombreCompleto,
                Superior = Usuario.Superior,
                IdPreguntaAlterna = Usuario.idTipoPregunta,
                ClaveAlterna = Usuario.ClaveAlterna,
                SuperAdmin = Usuario.TipoUsuario,
                LockoutEnabled = Usuario.LockoutEnabled,
                IdBodega = Usuario.IdBodega,
                PhoneNumber = Usuario.Telefono
            });
            Molde.RolesSeleccionados = Usuario.RolesSeleccionados;
            if (Usuario.Nombre != null)
            {
                var UserDuplicado = db.Users.Where(x => x.NombreCompleto == Usuario.Nombre).FirstOrDefault();
                if (UserDuplicado != null)
                {
                    if (UserDuplicado.Id != Usuario.Id)
                    {
                        ViewBag.Success = "No es permitido nombres de usuarios duplicados";
                        return PartialView(Molde);
                    }
                }
            }
            if (ModelState.IsValid)
            {
                var user = db.Users.Where(x => x.Id == Usuario.Id).FirstOrDefault();
                if (user == null)
                {
                    return HttpNotFound();
                }
                string OldUserName = user.UserName;
                user.UserName = Usuario.Nombre;
                user.Superior = Usuario.Superior;
                user.NombreCompleto = Usuario.NombreCompleto;
                user.ClaveAlterna = Usuario.ClaveAlterna;
                user.IdPreguntaAlterna = Usuario.idTipoPregunta;
                user.LockoutEnabled = Usuario.LockoutEnabled;
                user.EsAdministrador = Usuario.EsAdministrador;
                user.IdBodega = Usuario.IdBodega;
                /*
                 */
                user.SuperAdmin = Usuario.TipoUsuario;
                //var userRoles = UserManager.GetRoles(user.Id);
                //Usuario.RolesSeleccionados = Usuario.RolesSeleccionados ?? new string[] { };
                //var Roles = db.Roles.Where(r => Usuario.RolesSeleccionados.Contains(r.Id)).Select(n => n.Name).ToArray();
                //var result = UserManager.AddToRoles(user.Id, Roles.Except(userRoles).ToArray<string>());

                List<Users> Subordinados = db.Users.Where(par => par.Superior == OldUserName).ToList();

                foreach (var item in Subordinados)
                {
                    item.Superior = user.UserName;
                    db.Entry(item).State = EntityState.Modified;
                }
                // agregado por yency pero no me funciono  
                //db.Entry(Molde).State = EntityState.Modified;

                ViewBag.Usuario = user.UserName;
                db.SaveChanges();



                //ViewBag.Success = "El Usuario ha sido modificado con exito";
                TempData["modificar"] = "exito";
                //return PartialView(ModeladoModificar(user));
                return RedirectToAction("Index");
            }
            return PartialView(Molde);
        }

        ////GET: frmUsuario/CambiarClave/5
        //public ActionResult CambiarClave(string Id)
        //{
        //    if (Session["Usuario"] == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    var User = UserManager.FindById(Id);
        //    ViewBag.Usuario = User.UserName;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CambiarClave(CambiarClaveUsuario Cambio)
        //{
        //    var User = UserManager.FindById(Cambio.Id);
        //    ViewBag.Usuario = User.UserName;
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Success = "Ingrese informacion adecuada para los campos";
        //        return PartialView(Cambio);
        //    }
        //    var result = UserManager.ChangePassword(Cambio.Id, Cambio.ClaveActual, Cambio.NuevaClave);
        //    if (result.Succeeded)
        //    {
        //        ViewBag.Success = "Clave cambiada con exito";
        //        PartialView();
        //    }
        //    string msj = result.Errors.FirstOrDefault().ToString();
        //    if (msj == "Incorrect password.")
        //    {
        //        ViewBag.Success = "Contraseña Actual no es correcta";
        //        PartialView();
        //    }
        //    else
        //    {
        //        ViewBag.Success = "Ingrese informacion adecuada para los campos EJ:Escuela2016$FP";
        //        PartialView();
        //    }


        //    AddErrors(result);
        //    return PartialView();
        //}

        //public ActionResult CambiarClaveExtravio(string Id)
        //{
        //    if (Session["Usuario"] == null)
        //    {
        //        return RedirectToAction("Index", "Login");
        //    }
        //    var User = UserManager.FindById(Id);
        //    ViewBag.Usuario = User.UserName;
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult CambiarClaveExtravio(ExtravioClaveUsuario Cambio)
        //{
        //    string msj = "";
        //    var User = UserManager.FindById(Cambio.Id);
        //    ViewBag.Usuario = User.UserName;
        //    if (!ModelState.IsValid)
        //    {
        //        ViewBag.Success = "Ingrese informacion adecuada para los campos EJ:Escuela2016$FP";
        //        PartialView();
        //        return PartialView();
        //    }

        //    Encryption _Encryption = new Encryption();
        //    String DecryptedText = "";
        //    DecryptedText = _Encryption.DecryptKeySecond(User.SecurityPrevious.ToString());

        //    var result = UserManager.ChangePassword(Cambio.Id, DecryptedText, Cambio.NuevaClave);
        //    if (result.Succeeded)
        //    {
        //        ViewBag.Success = "Clave cambiada con exito";
        //        String EncryptedText = "";
        //        EncryptedText = _Encryption.EncryptKeySecond(Cambio.NuevaClave);

        //        User.SecurityPrevious = EncryptedText;
        //        var usuario = db.Users.FirstOrDefault(a => a.Id == User.Id);
        //        usuario.SecurityPrevious = EncryptedText;
        //        //usuario.SecurityPrevious = Cambio.NuevaClave;
        //        db.SaveChanges();
        //        PartialView();
        //    }
        //    if (result.Errors.Count() != 0)
        //    {
        //        msj = result.Errors.FirstOrDefault().ToString();
        //        if (msj == "Incorrect password.")
        //        {
        //            ViewBag.Success = "Contraseña Actual no es correcta";
        //            PartialView();
        //        }
        //        else
        //        {
        //            ViewBag.Success = "Ingrese informacion adecuada para los campos EJ:Escuela2016$FP";
        //            PartialView();
        //        }
        //    }

        //    AddErrors(result);
        //    return PartialView();
        //}

        //Seccion de Funciones para frmUsuarios
        //Funcion de prepara el Modelo de Vista para la manipulacion de Perfiles con sus respectivas opciones de menu
        private UsuariosRegistroViewModel Modelado(ApplicationUser Usuario)
        {
            UsuariosRegistroViewModel UsuariosRegistro = new UsuariosRegistroViewModel();
            //var U = db.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == Usuario.Id);
            //if (U != null)
            //{
            //    UsuariosRegistro.UsuarioRoles = U.Roles.ToList();
            //}
            UsuariosRegistro.Nombre = Usuario.UserName;
            UsuariosRegistro.NombreCompleto = Usuario.NombreCompleto;
            UsuariosRegistro.Superior = Usuario.Superior;
            UsuariosRegistro.ClaveAlterna = Usuario.ClaveAlterna;
            UsuariosRegistro.idTipoPregunta = Usuario.IdPreguntaAlterna;
            UsuariosRegistro.IdBodega = Convert.ToInt32(Usuario.IdBodega);
            //UsuariosRegistro.Roles = db.Roles.Include(u => u.Users).Where(par => par.Name != Globales.RolWebMaster).ToList();
            UsuariosRegistro.RolesSeleccionados = Usuario.Roles.Select(x => x.RoleId).ToArray();
            return UsuariosRegistro;
        }

        ////Funcion de prepara el Modelo de Vista para la manipulacion de Perfiles con sus respectivas opciones de menu
        private UsuariosModificar ModeladoModificar(ApplicationUser Usuario)
        {
            UsuariosModificar UsuariosModificar = new UsuariosModificar();
            //var U = db.Users.Include(u => u.Roles).SingleOrDefault(u => u.Id == Usuario.Id);
            //if (U != null)
            //{
            //    UsuariosModificar.UsuarioRoles = U.Roles.ToList();
            //}
            UsuariosModificar.Nombre = Usuario.UserName;
            UsuariosModificar.NombreCompleto = Usuario.NombreCompleto;
            //UsuariosModificar.Roles = db.Roles.Include(u => u.Users).Where(par => par.Name != Globales.RolWebMaster).ToList();
            UsuariosModificar.RolesSeleccionados = Usuario.Roles.Select(x => x.RoleId).ToArray();
            UsuariosModificar.Id = Usuario.Id;
            UsuariosModificar.Superior = Usuario.Superior;
            UsuariosModificar.ClaveAlterna = Usuario.ClaveAlterna;
            UsuariosModificar.idTipoPregunta = Usuario.IdPreguntaAlterna;
            UsuariosModificar.EsAdministrador = Usuario.EsAdministrador;
            UsuariosModificar.IdBodega = Convert.ToInt32(Usuario.IdBodega);
            /*13/12/2016 Juan Tejada 
             * Campo para Ingreso acceso al sistema
             */
            UsuariosModificar.LockoutEnabled = Usuario.LockoutEnabled;

            /*06/12/2016 Alex
             * integracion del llenado del modelo UsuariosModificar a propiedad TipoUsuario de SuperAdmin
             */
            UsuariosModificar.TipoUsuario = Usuario.SuperAdmin;

            return UsuariosModificar;
        }

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        //public frm_UsuariosController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Helpers
        //// Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}