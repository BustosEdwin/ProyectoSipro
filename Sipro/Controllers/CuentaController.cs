namespace Sipro.Controllers
{
    using Comun.Sipro.Dto;
    using Comun.Sipro.Utilidades;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;
    using Microsoft.SqlServer.Server;
    using Negocio.Sipro;
    using Servicio.Sipro;
    using Sipro.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    public class CuentaController : BaseController
    {
        ApplicationDbContext userContext;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        public CuentaController()
        {
            userContext = new ApplicationDbContext(/*new OracleConnection(ConfigurationManager.ConnectionStrings["IdentidadSigoa"].ConnectionString)*/);
        }

        public CuentaController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

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


        //GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(Usuario model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login", "Cuenta", new { area = "" });
            }

            string usuario = model.usuario.Trim().ToLower();

            //if (General.LoginOud(usuario, model.clave))
            if (true)
            {

                FuncionarioDTO funcionarioDTO = await new AdministracionUsuarios().ConsultarPorUsuarioEmpresarial(usuario);
                List<SiproRolDto> UsurioRoles = await new AdministracionUsuarios().ConsultaRol(usuario);

                ClaimsIdentity identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.CorreoElectronico, funcionarioDTO.CorreoElectronico));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.GradAlfabetico, funcionarioDTO.GradAlfabetico));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Nombres, funcionarioDTO.Nombres));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Fisica, funcionarioDTO.Fisica));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Apellidos, funcionarioDTO.Apellidos));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Identificacion, funcionarioDTO.Identificacion.ToString()));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Sexo, funcionarioDTO.Sexo));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.CargoActual, funcionarioDTO.CargoActual));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.NumeroCelular, funcionarioDTO.NumeroCelular.ToString()));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.UsuarioEmpresarial, funcionarioDTO.UsuarioEmpresarial));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.DescripcionDependencia, funcionarioDTO.DescripcionDependencia));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.SiglaPapa, funcionarioDTO.SiglaPapa));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Consecutivo, funcionarioDTO.Consecutivo.ToString()));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.UndeConsecutivo, funcionarioDTO.UndeConsecutivo.ToString()));
                identity.AddClaim(new Claim(ClaimPersonalizadoDTO.UndeFuerza, funcionarioDTO.UndeFuerza.ToString()));

                //identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Role, "Administrador"));

                foreach (var roles in UsurioRoles)
                {
                    identity.AddClaim(new Claim(ClaimPersonalizadoDTO.Role, roles.DescripcionRol.ToString()));

                }               


                AuthenticationManager.SignIn(identity);

                GestionUsuario gestionUsuario = new GestionUsuario();

                await gestionUsuario.ExisteFuncionario(funcionarioDTO.Consecutivo, funcionarioDTO.UndeConsecutivo);


                //if ( gestionUsuario.EstadoRespuesta.Estado)


                //if (!gestionUsuario.EstadoRespuesta.Estado)
                //{
                //    gestionUsuario.Usuario = new SiproUsuariosDto();                  

                //    gestionUsuario.Usuario.Consecutivo = funcionarioDTO.Consecutivo;
                //    gestionUsuario.Usuario.UndeConsecutivo = funcionarioDTO.UndeConsecutivo;
                //    gestionUsuario.Usuario.UndeFuerza = funcionarioDTO.UndeFuerza;
                //    gestionUsuario.Usuario.UsuarioCreacion = model.usuario;
                //    gestionUsuario.Usuario.MaquinaCreacion = Request.UserHostAddress;
                //    gestionUsuario.Usuario.UsuarioEmpresarial = model.usuario;
                //    gestionUsuario.Usuario.IdUsuario = Guid.NewGuid().ToString();

                //    await gestionUsuario.AgregarUsuarioAsync();

                //    if (gestionUsuario.EstadoRespuesta.Estado)
                //    {
                //        GestionUsuarioRol gestionUsuarioRol = new GestionUsuarioRol();

                //        gestionUsuarioRol.UsuarioRol = new SiproUsuarioRolDto();

                //        gestionUsuarioRol.UsuarioRol.IdUsuario = gestionUsuario.Usuario.IdUsuario;
                //        gestionUsuarioRol.UsuarioRol.IdRol = Guid.NewGuid().ToString();
                //        gestionUsuarioRol.UsuarioRol.IdRol = IdentificadoresDb.IDROL;
                //        gestionUsuarioRol.UsuarioRol.MaquinaCreacion = Request.UserHostAddress;
                //        gestionUsuarioRol.UsuarioRol.UsuarioCreacion = gestionUsuario.Usuario.UsuarioCreacion;


                //        await gestionUsuarioRol.AgregarRolBasicoAsync();
                //    }
                //}

                try
                {
                    //Variables de sesion del funcionario
                    //string foto = new FotoEmpleado().ObtenerFotoBase64(funcionarioDTO.Identificacion.ToString());
                    string foto = null;
                    if (foto.Length < 9999999)
                        Session["FOTO"] = foto;
                    else
                        Session["FOTO"] = null;
                    Session.Timeout = 500;
                }
                catch (Exception ex)
                {
                    Session["FOTO"] = null;
                }

                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mensaje = "Intento de inicio de sesión no válido.";
            return View(model);

            //var resultadoSinOid = await SignInManager.PasswordSignInAsync(model.usuario, model.clave, isPersistent: true, shouldLockout: false);
            //return await Direccionar(resultadoSinOid, model);
        }

        private async Task<ActionResult> Direccionar(SignInStatus _resultado, Usuario _model)
        {
            return await Task<ActionResult>.Factory.StartNew(() =>
            {
                switch (_resultado)
                {
                    case SignInStatus.Success:
                        return RedirectToAction("Aplicaciones", "BandejaAplicacion");
                    //switch (_model.sistema)
                    //{
                    //    case "SICEX":
                    //        return RedirectToAction("BandejaUsuarios", "GestionUsuario", new { area = "SICEX" });
                    //    case "CACIV":
                    //        return RedirectToAction("BandejaUsuarios", "GestionUsuario", new { area = "CACIV" });
                    //    case "SGSO":
                    //        return RedirectToAction("BandejaUsuarios", "GestionUsuario", new { area = "SGSO" });
                    //    default:
                    //        ModelState.AddModelError("", "No ha tomas un sistema de información");
                    //        return View(_model);
                    //}
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.usuario });
                    case SignInStatus.Failure:
                        return RedirectToAction("Login", "Cuenta", new { area = "" });

                    default:
                        ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                        return View(_model);
                }
            });

        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Requerir que el usuario haya iniciado sesión con nombre de usuario y contraseña o inicio de sesión externo
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // El código siguiente protege de los ataques por fuerza bruta a los códigos de dos factores. 
            // Si un usuario introduce códigos incorrectos durante un intervalo especificado de tiempo, la cuenta del usuario 
            // se bloqueará durante un período de tiempo especificado. 
            // Puede configurar el bloqueo de la cuenta en IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(Usuario model)
        {
            if (ModelState.IsValid)
            {
                //PersonalPonal personaPonal = new PersonalPonal();
                //var personaObtenida = await personaPonal.ObtenerPersonalConUsuario(model.usuario);
                var user = new ApplicationUser
                {
                    UserName = model.usuario + "@policia.gov.co",
                    Email = model.usuario + "@policia.gov.co",

                };
                //var result = await UserManager.CreateAsync(user, model.Password);
                //if (result.Succeeded)
                //{
                //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                // Para obtener más información sobre cómo habilitar la confirmación de cuenta y el restablecimiento de contraseña, visite http://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                // await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar la cuenta, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");

                //    return RedirectToAction("Index", "Home");
                //}
                //AddErrors(result);
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuenta y el restablecimiento de contraseña, visite http://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic <a href=\"" + callbackUrl + "\">aquí</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generar el token y enviarlo
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Si el usuario ya tiene un inicio de sesión, iniciar sesión del usuario con este proveedor de inicio de sesión externo
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Si el usuario no tiene ninguna cuenta, solicitar que cree una
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Obtener datos del usuario del proveedor de inicio de sesión externo
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login", "Cuenta");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
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

        #region Aplicaciones auxiliares
        // Se usa para la protección XSRF al agregar inicios de sesión externos
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