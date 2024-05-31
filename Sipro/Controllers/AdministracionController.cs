using Comun.Sipro.Dto;
using Comun.Sipro.Utilidades;
using Datos.Sipro;
using Negocio.Sipro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Sipro.Controllers
{

    public class AdministracionController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);
        // GET: Administracion

        [HttpGet]
        [Authorize]
        public ActionResult CrearRol()
        {
            ViewBag.IdVigente = new SelectList(new GestionAdministracion().ControlDominios((int)5), "IdDominio", "Descripcion");
            ViewBag.IdRol = new SelectList(new GestionAdministracion().ConsultaRol(), "IdRol", "DescripcionRol");
            return View();
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CrearRol(SiproRolDto _SiproRol)
        {
  
            GestionAdministracion gestionAdministracion = new GestionAdministracion();


            gestionAdministracion.LstRoles = new SiproRolDto
            {
                IdRol = Guid.NewGuid().ToString(),
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString(),
                MaquinaCreacion = Request.UserHostAddress,
                DescripcionRol = _SiproRol.DescripcionRol,

            };
            // gestionAdministracion.LstRoles.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            //gestionAdministracion.LstRoles.UsuarioCreacion = Request.UserHostAddress;

            await gestionAdministracion.AgregarRolAsync();

        
            return Json(gestionAdministracion.EstadoRespuesta);
        }

        public async Task<ActionResult> ConsultaFuncionarioAjax(int _Identificacion)
        {

            GestionAdministracion gestionAdministracion = new GestionAdministracion();

            ViewBag.IdVigente = new SelectList(new GestionAdministracion().ControlDominios((int)5), "IdDominio", "Descripcion");
            ViewBag.IdRol = new SelectList(new GestionAdministracion().ConsultaRol(), "IdRol", "DescripcionRol");
            await gestionAdministracion.ConsultaFuncionarioAsync(_Identificacion);


            return Json(gestionAdministracion.LstusuarioRol);
        }


        public async Task<ActionResult> CrearUsuario(SiproUsuarioRolDto _SiproUsuario)
        {

            GestionAdministracion gestionAdministracion = new GestionAdministracion();

            gestionAdministracion.Lstusuario = new SiproUsuarioRolDto()
            {
                IdUsuario = Guid.NewGuid().ToString(),
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString(),
                MaquinaCreacion = Request.UserHostAddress,
                usuario = _SiproUsuario.usuario,
                Consecutivo = _SiproUsuario.Consecutivo,
                UndeConsecutivo = _SiproUsuario.UndeConsecutivo,
                UndeFuerza = _SiproUsuario.UndeFuerza,
                Vigente = _SiproUsuario.Vigente,
                IdRol = _SiproUsuario.IdRol,
                FechaInicio = _SiproUsuario.FechaInicio,
                FechaFin = _SiproUsuario.FechaFin,
            };

   

            await gestionAdministracion.AgregarUsuarioAsync();


            return Json(gestionAdministracion.EstadoRespuesta);
        }




    }
}