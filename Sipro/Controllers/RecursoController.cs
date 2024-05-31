namespace Sipro.Controllers
{
    using Comun.Sipro.Dto;
    using Comun.Sipro.Utilidades;
    using Negocio.Sipro;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    [Authorize]
    public class RecursoController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        #region Metodos de Controlador
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CrearRecursoAjax(SiproRecursoDto _siproRecurso)
        {
            GestionRecursos gestionRecurso = new GestionRecursos();

            gestionRecurso.SiproRecurso = _siproRecurso;

            gestionRecurso.SiproRecurso.MaquinaCreacion = Request.UserHostAddress;
            gestionRecurso.SiproRecurso.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionRecurso.SiproRecurso.IdRecurso = Guid.NewGuid().ToString();


            await gestionRecurso.AgregarRecursoAsync();

            return Json(gestionRecurso.EstadoRespuesta);
        }
        #endregion
    }
}