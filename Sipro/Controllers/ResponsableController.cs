using Comun.Sipro.Dto;
using Comun.Sipro.Utilidades;
using Negocio.Sipro;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sipro.Controllers
{
    [Authorize]
    public class ResponsableController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CrearResponsableAjax(ParametrosAgregarResponsable _agregarResponsable)
        {
            //Se necesita que se ingrese el numero de cedula con el rol que va a tener, para el rol es necesario una lista en la vista.
            GestionFuncionarios gestionFuncionario = new GestionFuncionarios();
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionUnidades gestionUnidades = new GestionUnidades();

            await gestionFuncionario.ObtenerFuncionarioAsync(_agregarResponsable.Identificacion);
            await gestionUnidades.ObtenerUnidadSiglaAsync(gestionFuncionario.Funcionario.SiglaPapa);
            //Se valida si el funcionario fue encontrado
            if (!gestionFuncionario.EstadoRespuesta.Estado)
            {
                return Json(gestionFuncionario.EstadoRespuesta);
            }

            await gestionResponsable.ObtenerResponsablesProyectoVigentesAsync(_agregarResponsable.IdProyecto);

            if (gestionResponsable.LstResonsables.Any((x => x.IdTipoResponsabilidad == "ad5ac280-755c-4fec-8fec-59b3813ba25d" && x.FechaFin == null)) && (_agregarResponsable.IdTipoResponsable == "ad5ac280-755c-4fec-8fec-59b3813ba25d"))
            {
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "Existe un lider de proyecto activo."
                });
                //if (gestionResponsable.LstResonsables.Count(s => s.IdTipoResponsabilidad == "ad5ac280-755c-4fec-8fec-59b3813ba25d") <= 1)
                //    return Json(new EstadoRespuesta
                //    {
                //        Codigo = 0,
                //        Estado = false,
                //        Mensaje = "Existe un lider de proyecto activo o eres el lider de proyecto."
                //    });
                //else
                //{
                //    return Json(new EstadoRespuesta
                //    {
                //        Codigo = 0,
                //        Estado = false,
                //        Mensaje = "Existe un lider de proyecto activo o eres el lider de proyecto."
                //    });
                //}

            }

            if (gestionResponsable.LstResonsables.Any(x => x.Identificacion == _agregarResponsable.Identificacion && x.FechaFin == null))
            {
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "El funcionario ya tiene asignada una responsabilidad."
                });
            }

            gestionResponsable.SiproResponsable = new SiproResponsableDto();
            gestionResponsable.SiproResponsable.IdResponsable = Guid.NewGuid().ToString();
            gestionResponsable.SiproResponsable.IdProyecto = _agregarResponsable.IdProyecto;
            gestionResponsable.SiproResponsable.IdTipoResponsable = _agregarResponsable.IdTipoResponsable;
            gestionResponsable.SiproResponsable.Apellidos = gestionFuncionario.Funcionario.Apellidos;
            gestionResponsable.SiproResponsable.Cargo = gestionFuncionario.Funcionario.CargoActual;
            gestionResponsable.SiproResponsable.Consecutivo = gestionFuncionario.Funcionario.Consecutivo;
            gestionResponsable.SiproResponsable.FechaAsignacion = DateTime.Now;
            gestionResponsable.SiproResponsable.FechaCreacion = DateTime.Now;
            gestionResponsable.SiproResponsable.Grado = gestionFuncionario.Funcionario.GradAlfabetico;
            gestionResponsable.SiproResponsable.Identificacion = gestionFuncionario.Funcionario.Identificacion;
            gestionResponsable.SiproResponsable.MaquinaCreacion = Request.UserHostAddress;
            gestionResponsable.SiproResponsable.Nombres = gestionFuncionario.Funcionario.Nombres;
            gestionResponsable.SiproResponsable.UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo;
            gestionResponsable.SiproResponsable.UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza;
            gestionResponsable.SiproResponsable.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionResponsable.SiproResponsable.Vigente = EstadoRegistro.VIGENTE;
            gestionResponsable.SiproResponsable.IdUnidad = gestionUnidades.PortalUnidad.Consecutivo;

            await gestionResponsable.AgregarResponsableAsync();

            return Json(gestionResponsable.EstadoRespuesta);

        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> FinalizarResponsableAjax(SiproResponsableDto _siproResponsableDto)
        {
            GestionResponsable gestionResponsable = new GestionResponsable();
            await gestionResponsable.FinalizarResponsableAsync(_siproResponsableDto);
            return Json(gestionResponsable.EstadoRespuesta);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AsignarResponsableActividadAjax(SiproBitacoResponsablesDto _SiproBitacoResponsablesDto)
        {
            GestionActividadesResponsables gestionActividadesResponsable = new GestionActividadesResponsables();

            gestionActividadesResponsable.ActividadBitacora = _SiproBitacoResponsablesDto;
            gestionActividadesResponsable.ActividadBitacora.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionActividadesResponsable.ActividadBitacora.MaquinaCreacion = Request.UserHostAddress;
            gestionActividadesResponsable.ActividadBitacora.IdBitacoResponsable = Guid.NewGuid().ToString();


            await gestionActividadesResponsable.AgregarActividadResponsableAsync();

            return Json(gestionActividadesResponsable.EstadoRespuesta);
        }



    }
}