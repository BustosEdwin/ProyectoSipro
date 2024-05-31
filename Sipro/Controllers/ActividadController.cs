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
    public class ActividadController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        #region Metodos de controlador
        [HttpPost]
        //[Authorize(Roles = "BASICO,ADMINISTRADOR_JEFE")]
        [Authorize]
        public async Task<ActionResult> AgregarActividadProyectoAjax(SiproBitacoraDto _siproBitacora)
        {
            GestionActividades gestionActividades = new GestionActividades();
            GestionObservaciones gestionObservaciones = new GestionObservaciones();
            GestionProyectos gestionProyecto = new GestionProyectos();

            gestionObservaciones.SiproObservaciones = new SiproObservacionesDto
            {
                IdObservacion = Guid.NewGuid().ToString(),
                Descripcion = _siproBitacora.Observacion,
                MaquinaCreacion = Request.UserHostAddress,
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString()
            };

            await gestionObservaciones.AgregarObservacionAsync();

            gestionActividades.Actividad = _siproBitacora;

            gestionActividades.Actividad.IdBitacora = Guid.NewGuid().ToString();
            gestionActividades.Actividad.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionActividades.Actividad.MaquinaCreacion = Request.UserHostAddress;
            gestionActividades.Actividad.IdObservacion = gestionObservaciones.SiproObservaciones.IdObservacion;


            await gestionActividades.AgregarActividadAsync();
            await gestionProyecto.ActualizarEstadoProyectoAenProceso(_siproBitacora.IdProyecto);

            return Json(gestionActividades.EstadoRespuesta);
        }


        [HttpGet]
        [Authorize]
        public async Task<ActionResult> BandejaActividadesProyecto(string _idProyecto)
        {
            GestionActividades gestionActividades = new GestionActividades();
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionRecursos gestionRecursos = new GestionRecursos();
            GestionTipoRecursos gestionTipoRecurso = new GestionTipoRecursos();
            GestionTipoResponsable gestionTipoResponsable = new GestionTipoResponsable();
            GestionFases gestionFases = new GestionFases();
            GestionObservaciones gestionObservacion = new GestionObservaciones();
            GestionProyectos gestionProyectos = new GestionProyectos();

            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionComentarios gestionComentarios = new GestionComentarios();



            await gestionObservacion.ObtenerDescripcionProyectoAsync(_idProyecto);
            await gestionActividades.ObtenerActividadesVigentesProyectoAsync(_idProyecto);
            //Obtener los responsables del proyecto vigentes y no activos
            await gestionResponsable.ObtenerResponsablesProyectoVigentesAsync(_idProyecto, 0);
            ViewBag.ResponsablesProyectoNoActivos = gestionResponsable.LstResonsables;
            //Obtener los responsables del proyecto vigentes y activos
            await gestionResponsable.ObtenerResponsablesProyectoVigentesAsync(_idProyecto, 1);
            await gestionRecursos.ObtenerRecursosVigentesProyectoAsync(_idProyecto);
            await gestionTipoRecurso.ObtenerTipoRecursosVigentesAsync();
            await gestionTipoResponsable.ObtenerTpoResponsabilidadesAsync();
            await gestionFases.ObtenerFasesVigentesAsync();
            await gestionProyectos.ObtenerProyectosVigentesPorIdProyectoAsync(_idProyecto);

            //Obtener Evidencia aprobada
            // Se debe validar ese try catch
            try
            {
                for (int i = 0; i < gestionActividades.LstActividades.Count; i++)
                {
                    await gestionEvidencias.ObtenerEvidenciasActividadesAsync(gestionActividades.LstActividades[i].IdBitacora);

                    foreach (var comentarioEvidencia in gestionEvidencias.LstEvidencias)
                    {
                        await gestionComentarios.ObtenerComentarioIdEvidenciaAsync(comentarioEvidencia.IdEvidencia);

                        if (gestionComentarios.Comentario.IdEstado == "1ca8b15a-bfda-492e-b5a3-9e67348f66op")
                        {
                            gestionActividades.LstActividades[i].EvidenciaValida = true;
                        }
                        else
                        {
                            gestionActividades.LstActividades[i].EvidenciaValida = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }      

            ViewBag.Observacion = gestionObservacion.SiproObservaciones != null ? gestionObservacion.SiproObservaciones.Descripcion : null;
            ViewBag.ResponsablesProyectoActivos = gestionResponsable.LstResonsables;
          
            ViewBag.TipoRecursos = new SelectList(gestionTipoRecurso.LstSiproTipoRecursos, nameof(SiproTipoRecursoDto.IdTipoRecurso), nameof(SiproTipoRecursoDto.Descripcion));
            ViewBag.TipoResponsabilidad = new SelectList(gestionTipoResponsable.LstTipoResponsabilidades, nameof(SiproTipoResponsabilidadDto.IdTipoResponsabilidad), nameof(SiproTipoResponsabilidadDto.Descripcion));
            ViewBag.Fases = new SelectList(gestionFases.LstSiproFases, nameof(SiproFasesDto.IdFase), nameof(SiproFasesDto.Descripcion));
            ViewBag.Recursos = gestionRecursos.LstSiproRecursos;
            ViewBag.Proyecto = gestionProyectos.SiproProyecto;                     

            if (gestionResponsable.LstResonsables.Count > 0)
                foreach (var responsable in gestionResponsable.LstResonsables)
                {
                    if (responsable.Identificacion == Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion)))
                        await this.TipoResponsabilidad(responsable.IdResponsable);
                }
            else
                await this.TipoResponsabilidad(string.Empty);

            return View(gestionActividades.LstActividades);

        }

        [HttpPost]
        public async Task<ActionResult> ObtenerFuncionario(long _identificacion)
        {
            GestionFuncionarios gestionFuncionarios = new GestionFuncionarios();
            await gestionFuncionarios.ObtenerFuncionarioAsync(_identificacion);
            return Json(gestionFuncionarios.EstadoRespuesta);
        }
        #endregion
    }
}