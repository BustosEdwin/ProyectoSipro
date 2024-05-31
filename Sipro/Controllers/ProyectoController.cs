namespace Sipro.Controllers
{
    using Comun.Sipro.Dto;
    using Negocio.Sipro;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using System;
    using System.Activities.Statements;
    using Comun.Sipro.Utilidades;

    [Authorize]
    public class ProyectoController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        #region Metodos del controlador externos

        [HttpGet]
        //[Authorize(Roles = "DESARROLLADOR, ADMINISTADORJEFE, LIDERPROYECTO")]
        [Authorize]
        public async Task<ActionResult> BandejaProyectos()
        {
            #region Variables Generales del metodo
            GestionProyectos gestionProyecto = new GestionProyectos();
            GestionUnidades gestionUnidades = new GestionUnidades();
            GestionResponsable gestionResponsable = new GestionResponsable();
            string identificacionLogeado = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString();
            #endregion
            
            //Obtenemos las unidades vigentes en la policia nacional.
            await gestionUnidades.ObtenerUnidadesAsync();

            ViewBag.Unidades = new SelectList(gestionUnidades.LstPortalUnidades, nameof(PortalUnidadesSiglasDto.Consecutivo), nameof(PortalUnidadesSiglasDto.Sigla));

            var roles = gestionClaims.ObtenerClaimRoles(ClaimPersonalizadoDTO.Role);

            if (User.IsInRole("ADMINISTRADORJEFE"))
            {
                await gestionProyecto.ObtenerProyectosVigentesAsync();
            }
            else if (User.IsInRole("ADMINISTRADORUNIDAD"))
            {
                await gestionProyecto.ObtenerProyectosUnidadAsync(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Fisica).ToString());
            }
            else
            {
                await gestionProyecto.ConsultarProyectosResponsableAsync(Convert.ToInt64(identificacionLogeado));
            }

            return View(gestionProyecto.LstProyectos);
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> CrearProyecto()
        {
            GestionUnidades gestionUnidades = new GestionUnidades();

            await gestionUnidades.ObtenerUnidadesAsync();

            ViewBag.Unidades = new SelectList(gestionUnidades.LstPortalUnidades, nameof(PortalUnidadesSiglasDto.Consecutivo), nameof(PortalUnidadesSiglasDto.Sigla));

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CrearProyectoAjax(SiproProyectoDto _siproProyecto)
        {

            //Validaciones
            EstadoRespuesta estadoValidacion = await Validaciones.ValidarUnidades(_siproProyecto.IdUnidadResponsable, _siproProyecto.IdUnidadSolicitante, "Unidad Responsable", "Unidad Solicitante");

            if (!estadoValidacion.Estado)
                return Json(estadoValidacion);


            GestionProyectos gestionProyecto = new GestionProyectos();
            GestionObservaciones gestionObservacion = new GestionObservaciones();
            GestionUnidades gestionUnidades = new GestionUnidades();
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionFuncionarios gestionFuncionario = new GestionFuncionarios();

            //Agregando el proyecto a la base de datos 
            gestionProyecto.SiproProyecto = _siproProyecto;
            gestionProyecto.SiproProyecto.IdProyecto = Guid.NewGuid().ToString();
            gestionProyecto.SiproProyecto.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionProyecto.SiproProyecto.MaquinaCreacion = Request.UserHostAddress;

            //Consulta la unidad para asignar la SiglaUnidadesResponsables
            await gestionUnidades.ObtenerUnidadSiglaAsync(_siproProyecto.IdUnidadResponsable);

            gestionProyecto.SiproProyecto.SiglaUnidadResponsable = gestionUnidades.PortalUnidad.Sigla;

            //Consulta la unidad para asignar la SiglaUnidadSolicitante
            await gestionUnidades.ObtenerUnidadSiglaAsync(_siproProyecto.IdUnidadSolicitante);

            gestionProyecto.SiproProyecto.SiglaUnidadSolicitante = gestionUnidades.PortalUnidad.Sigla;
            gestionProyecto.SiproProyecto.IdUnidadResponsable = _siproProyecto.IdUnidadResponsable;
            gestionProyecto.SiproProyecto.IdUnidadSolicitante = _siproProyecto.IdUnidadSolicitante;
            gestionProyecto.SiproProyecto.Identificacion = Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion));


            //agregar responsable como lider proyecto al creador del proyecto            

            await gestionFuncionario.ObtenerFuncionarioAsync(gestionProyecto.SiproProyecto.UsuarioCreacion);
            await gestionUnidades.ObtenerUnidadSiglaAsync(gestionFuncionario.Funcionario.SiglaPapa); //Obtener ConsecutivoSiglaUnidad
            gestionResponsable.SiproResponsable = new SiproResponsableDto
            {
                Apellidos = gestionFuncionario.Funcionario.Apellidos,
                Cargo = gestionFuncionario.Funcionario.CargoActual,
                Consecutivo = gestionFuncionario.Funcionario.Consecutivo,
                FechaAsignacion = DateTime.Now,
                FechaCreacion = DateTime.Now,
                Grado = gestionFuncionario.Funcionario.GradAlfabetico,
                Identificacion = gestionFuncionario.Funcionario.Identificacion,
                IdProyecto = gestionProyecto.SiproProyecto.IdProyecto,
                IdResponsable = Guid.NewGuid().ToString(),
                IdTipoResponsable = "ad5ac280-755c-4fec-8fec-59b3813ba25d",//Este se debe mejorar no deben quedar texto magicos
                IdUnidad = gestionUnidades.PortalUnidad.Consecutivo,
                MaquinaCreacion = Request.UserHostAddress,
                Nombres = gestionFuncionario.Funcionario.Nombres,
                UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo,
                UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza,
                UsuarioCreacion = gestionProyecto.SiproProyecto.UsuarioCreacion,
                Vigente = 1,
                Activo = true

            };


            //Agregando la observación o descripción en la base de datos
            gestionObservacion.SiproObservaciones = new SiproObservacionesDto
            {
                IdObservacion = Guid.NewGuid().ToString(),
                IdProyecto = gestionProyecto.SiproProyecto.IdProyecto,
                Descripcion = _siproProyecto.Descripcion,
                MaquinaCreacion = Request.UserHostAddress,
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString()
            };

            bool existenciaSistema = await gestionProyecto.VerificarExistenciaProyecto(gestionProyecto.SiproProyecto.Acronimo);

            if (!existenciaSistema)
            {
                await gestionProyecto.AgregarProyectoAsync();
                await gestionResponsable.AgregarResponsableAsync();
                await gestionObservacion.AgregarObservacionAsync();
            }
            else
            {
                EstadoRespuesta estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "El Proyecto ya existe por favor validar con el administrador."
                };
                return Json(estadoRespuesta);
            }

            //Revisar logica de programación, ya que puede fallar una inserción y la otra no.
            return Json(gestionProyecto.EstadoRespuesta);

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> DetalleProyectoAjax(string _idProyecto)
        {
            EstadoRespuesta resultadoValidacionCadena = await Validaciones.LimiteCadenaAsync(_idProyecto, 50);

            if (!resultadoValidacionCadena.Estado)
                return Json(resultadoValidacionCadena, JsonRequestBehavior.AllowGet);

            GestionObservaciones gestionObservacion = new GestionObservaciones();

            await gestionObservacion.ObtenerDescripcionProyectoAsync(_idProyecto);

            return Json(gestionObservacion.EstadoRespuesta, JsonRequestBehavior.AllowGet);

        }
        #endregion

    }
}