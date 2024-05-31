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
    public class ComentariosController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        [HttpGet]
        public async Task<ActionResult> BandejaEstadoComentariosAjax()
        {
            GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();

            await gestionEstadoComentario.ObtenerEstadoComentariosAsignadosAsync(Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString()));

            if (gestionEstadoComentario.EstadoRespuesta.Estado)
                return Json(new EstadoRespuesta
                {
                    Codigo = gestionEstadoComentario.EstadoRespuesta.Codigo,
                    Estado = gestionEstadoComentario.EstadoRespuesta.Estado,
                    Mensaje = gestionEstadoComentario.EstadoRespuesta.Mensaje,
                    Objeto = gestionEstadoComentario.LstEstadoComentario
                }, JsonRequestBehavior.AllowGet);

            return Json(gestionEstadoComentario.EstadoRespuesta, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public async Task<ActionResult> BandejaEstadoComentariosParaCorregirAjax()
        {
            GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();

            await gestionEstadoComentario.ObtenerEstadoComentariosParaCorregirAsync(Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString()));

            if (gestionEstadoComentario.EstadoRespuesta.Estado)
                return Json(new EstadoRespuesta
                {
                    Codigo = gestionEstadoComentario.EstadoRespuesta.Codigo,
                    Estado = gestionEstadoComentario.EstadoRespuesta.Estado,
                    Mensaje = gestionEstadoComentario.EstadoRespuesta.Mensaje,
                    Objeto = gestionEstadoComentario.LstEstadoComentario
                }, JsonRequestBehavior.AllowGet);

            return Json(gestionEstadoComentario.EstadoRespuesta, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EnviarComentarioActividadAjax(SiproComentarioDto _SiproComentarioDto)
        {
            //Validar si el que esta logeado es un responsable de la actividad para enviar comentarios en la evidencia.
            bool banderaResponsableEvidencia = false;
            string idResponsableActividad = string.Empty;
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionActividadesResponsables gestionActividadesResponsables = new GestionActividadesResponsables();

            await gestionEvidencias.ObtenerEvidenciaAsync(_SiproComentarioDto.IdEvidencia);
            await gestionActividadesResponsables.ObtenerActividadesVigentesProyectoAsync(gestionEvidencias.Evidencia.IdBitacora);

            foreach (var actividadesResponsables in gestionActividadesResponsables.LstActividadesBitacora)
            {
                if (gestionActividadesResponsables.ActividadBitacora.Identificacion == Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString()))
                {
                    banderaResponsableEvidencia = true;
                    idResponsableActividad = actividadesResponsables.IdResponsable;
                }
            }

            if (banderaResponsableEvidencia)
            {
                GestionComentarios gestionComentarios = new GestionComentarios();
                GestionResponsable gestionResponsable = new GestionResponsable();

                //Inicio Cambiar segun la logica de la base de datos
                gestionComentarios.ActividadComentario = _SiproComentarioDto;
                gestionComentarios.ActividadComentario.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
                gestionComentarios.ActividadComentario.UsuarioComentario = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
                gestionComentarios.ActividadComentario.Consecutivo = Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Consecutivo).ToString());
                gestionComentarios.ActividadComentario.UndeConsecutivo = Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UndeConsecutivo).ToString());
                gestionComentarios.ActividadComentario.UndeFuerza = Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UndeFuerza).ToString());
                gestionComentarios.ActividadComentario.MaquinaCreacion = Request.UserHostAddress;
                gestionComentarios.ActividadComentario.IdCometario = Guid.NewGuid().ToString();

                await gestionResponsable.ObtenerResponsablesProyectoVigentesAsync(_SiproComentarioDto.IdProyecto);
                gestionComentarios.ActividadComentario.IdFuncionarioEnvia = gestionResponsable.LstResonsables.Where(x => x.IdTipoResponsable == "ad5ac280-755c-4fec-8fec-59b3813ba25d" && x.Activo == true).FirstOrDefault().IdResponsable;
                gestionComentarios.ActividadComentario.Estados = (int)EnumEstadosComentario.Revision;

                await gestionComentarios.EnviarActividadComentarioAsync();
                //Fin Cambiar segun la logica de la base de datos

                GestionTrazaResponsable gestionTraza = new GestionTrazaResponsable();

                if (gestionComentarios.EstadoRespuesta.Estado)
                {
                    gestionTraza.TrazaComentario = new SiproTrazaResponsableDto
                    {
                        IdTrazaResponsble = Guid.NewGuid().ToString(),
                        IdComentario = gestionComentarios.Comentario.IdCometario,
                        IdEstadoComentario = "por asignar",
                        IdResponsable = idResponsableActividad
                    };

                    await gestionTraza.AgregarTrazaResponsableAsync();

                    if (!gestionTraza.EstadoRepuesta.Estado)
                        return Json(gestionComentarios.EstadoRespuesta);
                }

                return Json(gestionComentarios.EstadoRespuesta);
            }


            return Json(new EstadoRespuesta
            {
                Codigo = 0,
                Estado = false,
                Mensaje = "No se puede generar el comentario, posiblemente no eres responsable de la actividad."
            });


        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> BandejaEvidencias(string _idEvidencia)
        {

            GestionComentarios gestionComentarios = new GestionComentarios();
            await gestionComentarios.ConsultarComentario(_idEvidencia);
            return Json(gestionComentarios.LstProyectos);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> ObtenerTrazabilidadAjax(string _idEvidencia)
        {
            GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();
            GestionComentarios gestionComentarios = new GestionComentarios();
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionFuncionarios gestionFuncionarios = new GestionFuncionarios();
            GestionEstados gestionEstados = new GestionEstados();


            await gestionEvidencias.ObtenerEvidenciaAsync(_idEvidencia);

            await gestionComentarios.ObtenerComentarioIdEvidenciaAsync(_idEvidencia);

            await gestionEstadoComentario.ObtenerTrazabilidadAsync(gestionComentarios.Comentario.IdCometario);

            List<TrazabilidadDto> lstTrazabilidad = new List<TrazabilidadDto>();
            foreach (var trazabilidad in gestionEstadoComentario.LstEstadoComentario)
            {
                await gestionFuncionarios.ObtenerFuncionarioAsync(trazabilidad.UsuarioCreacion.ToLower());
                await gestionEstados.ObtenerEstadoVigenteAsync(trazabilidad.IdEstado);
                lstTrazabilidad.Add(new TrazabilidadDto
                {
                    DescripcionComentario = trazabilidad.Descripcion,
                    NombreGradoFuncionario = $"{gestionFuncionarios.Funcionario.GradAlfabetico} - {gestionFuncionarios.Funcionario.Apellidos} {gestionFuncionarios.Funcionario.Nombres}",
                    Unidad = gestionFuncionarios.Funcionario.SiglaPapa,
                    EstadoComentario = gestionEstados.SiproEstados.Descripcion
                });
            }

            return Json(new EstadoRespuesta
            {
                Codigo = 1,
                Estado = true,
                Mensaje = "Se encontraron registros",
                Objeto = lstTrazabilidad.OrderBy(x => x.FechaOrganizacion)
            }, JsonRequestBehavior.AllowGet);
        }
    }
}