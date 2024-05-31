namespace Sipro.Controllers
{
    using Comun.Sipro;
    using Comun.Sipro.Dto;
    using Comun.Sipro.Utilidades;
    using Negocio.Sipro;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    [Authorize]
    public class EvidenciaController : BaseController
    {
        GestionClaims gestionClaims = new GestionClaims((System.Security.Claims.ClaimsIdentity)System.Threading.Thread.CurrentPrincipal.Identity);

        #region Metodos de controlador
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> SubirEvidenciaAjax(SiproEvidenciaDto _siproEvidencia)
        {

            //if (!General.LoginOud(_siproEvidencia.Usuario, _siproEvidencia.Contrasena))
            //    return Json(new EstadoRespuesta
            //    {
            //        Codigo = 0,
            //        Estado = false,
            //        Mensaje = "El usuario o contraseña no son correctos."
            //    });

            if (_siproEvidencia.Archivo == null || string.IsNullOrEmpty(_siproEvidencia.Archivo.FileName))
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "Por favor ingrese el archivo Pdf."
                });

            if (string.IsNullOrEmpty(_siproEvidencia.Observacion))
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "Describa el documento"
                });

            GestionActividadesResponsables gestionActividadesResponsables = new GestionActividadesResponsables();
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionFuncionarios gestionFuncionarios = new GestionFuncionarios();
            GestionObservaciones gestionObservaciones = new GestionObservaciones();



            await gestionActividadesResponsables.ObtenerActividadesVigentesProyectoAsync(_siproEvidencia.IdBitacora);
            short contadorResponsabilidades = 0;
            foreach (var itemResponsable in gestionActividadesResponsables.LstActividadesBitacora)
            {
                await gestionFuncionarios.ObtenerFuncionarioAsync(Convert.ToInt64(itemResponsable.Identificacion));
                if (gestionFuncionarios.EstadoRespuesta.Estado && gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().Equals(gestionFuncionarios.Funcionario.UsuarioEmpresarial))
                {
                    contadorResponsabilidades++;
                    _siproEvidencia.IdResponsable = itemResponsable.IdResponsable;
                }
            }

            if (contadorResponsabilidades == 0)
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "No eres responsable de ninguna actividad, no puedes subir evidencias."
                });

            // Guardar observación
            gestionObservaciones.SiproObservaciones = new SiproObservacionesDto
            {
                IdObservacion = Guid.NewGuid().ToString(),
                Descripcion = _siproEvidencia.Observacion,
                MaquinaCreacion = Request.UserHostAddress,
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString()

            };

            await gestionObservaciones.AgregarObservacionAsync();

            gestionEvidencias.Evidencia = _siproEvidencia;

            gestionEvidencias.Evidencia.UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            gestionEvidencias.Evidencia.MaquinaCreacion = Request.UserHostAddress;
            gestionEvidencias.Evidencia.IdEvidencia = Guid.NewGuid().ToString();
            gestionEvidencias.Evidencia.IdObservaciones = gestionObservaciones.SiproObservaciones.IdObservacion;
            gestionEvidencias.Evidencia.FechaCreacion = DateTime.Now;
            //Guardar archivo Pdf
            //string rutaGuardarArchivo = @"\\srvfilesponal3\OFITE\AITEC\GRUDE\PT BUSTOS EDWIN\Trabajo\Documentacion\SIPRO\Archivos";
            try
            {
                string rutaGuardarArchivo = @"C:\\sipro";
                //string rutaGuardarArchivo = @"\\srvfilesponal3\OFITE\AITEC\GRUDE\SI_REYES\ArchivoSipro\sipro";
                _siproEvidencia.Archivo.SaveAs($"{rutaGuardarArchivo}/{_siproEvidencia.Archivo.FileName}");
                //_siproEvidencia.Archivo.SaveAs($"{rutaGuardarArchivo}/{_siproEvidencia.Archivo.FileName}-{gestionEvidencias.Evidencia.IdEvidencia}");
                gestionEvidencias.Evidencia.UrlRuta = $"{rutaGuardarArchivo}/{_siproEvidencia.Archivo.FileName}";


            }
            catch (Exception ex)
            {
                return Json(new EstadoRespuesta
                {
                    Codigo = -1,
                    Mensaje = $"Ocurrio una excepción, {ex.Message}",
                    Estado = false
                });
            }

            await gestionActividadesResponsables.FimarActividadResponsableAsync(_siproEvidencia.IdResponsable, _siproEvidencia.IdBitacora);
            await gestionEvidencias.AgregarEvidenciaAsync();

            return Json(gestionEvidencias.EstadoRespuesta);
        }

        [HttpGet]
        public async Task<ActionResult> BandejaEvidencias(string _idActividad, string _idProyecto)
        {
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionActividadesResponsables gestionActividadesResponsables = new GestionActividadesResponsables();

            await gestionEvidencias.ObtenerEvidenciasActividadesAsync(_idActividad);
            await gestionResponsable.ObtenerResponsablesProyectoVigentesAsync(_idProyecto, 1);
            await gestionActividadesResponsables.ObtenerActividadesVigentesProyectoAsync(_idActividad);
            ViewBag.Estado = new SelectList(new GestionEvidencias().NombreProcedimiento2((int)1), "IdDominio", "Descripcion");

            ViewBag.ActividadesResponsables = gestionActividadesResponsables.LstActividadesBitacora;
            ViewBag.ResponsablesProyecto = new SelectList(gestionResponsable.LstResonsables, nameof(SiproResponsableDto.IdResponsable), nameof(SiproResponsableDto.NombreCompletoGrado), nameof(SiproResponsableDto.Identificacion));

            ViewBag.IdProyecto = _idProyecto;
            ViewBag.IdActividad = _idActividad;

            //Saber El tipo responsabilidad del quien este logeado
            foreach (var responsable in gestionResponsable.LstResonsables)
            {
                if (responsable.Identificacion == Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion)))
                    await this.TipoResponsabilidad(responsable.IdResponsable);
            }

            await gestionResponsable.ObtenerResponsablesComentarioAsync(_idProyecto);
            ViewBag.ResponsablesComentario = new SelectList(gestionResponsable.LstResonsables, nameof(SiproResponsableDto.IdResponsable), nameof(SiproResponsableDto.NombreCompletoGrado));

            return View(gestionEvidencias.LstEvidencias);
        }



        [HttpGet]
        [Authorize]
        public ActionResult VerArchivoPdf(string _nombreArchivo)
        {
            //byte[] bytesArchivoPdf = System.IO.File.ReadAllBytes($@"\\srvfilesponal3\OFITE\AITEC\GRUDE\PT BUSTOS EDWIN\Trabajo\Documentacion\SIPRO\Archivos\{_nombreArchivo}");
            //byte[] bytesArchivoPdf = System.IO.File.ReadAllBytes($@"C:\\sipro\{_nombreArchivo}");
            byte[] bytesArchivoPdf = System.IO.File.ReadAllBytes($@"C:\\sipro\{_nombreArchivo}");



            return File(bytesArchivoPdf, "application/pdf");
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EnviarAprobarEvidenciaAnexaAjax(ParametrosAprobarEvidenciaAnexaDto _parametrosAprobarEvidencia)
        {
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionFuncionarios gestionFuncionario = new GestionFuncionarios();
            GestionComentarios gestionComentario = new GestionComentarios();

            #region Consultar o validar que el funcionario tenga reponsabilidad sobre las actividades
            GestionActividadesResponsables gestionActividadesResponsables = new GestionActividadesResponsables();
            GestionEvidencias gestionEvidencias = new GestionEvidencias();

            await gestionEvidencias.ObtenerEvidenciaAsync(_parametrosAprobarEvidencia.IdEvidencia);

            await gestionActividadesResponsables.ObtenerActividadesVigentesProyectoAsync(gestionEvidencias.Evidencia.IdBitacora);

            short contadorResponsabilidades = 0;
            foreach (var itemResponsable in gestionActividadesResponsables.LstActividadesBitacora)
            {
                await gestionFuncionario.ObtenerFuncionarioAsync(Convert.ToInt64(itemResponsable.Identificacion));
                if (gestionFuncionario.EstadoRespuesta.Estado && gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().Equals(gestionFuncionario.Funcionario.UsuarioEmpresarial))
                    contadorResponsabilidades++;
            }

            if (contadorResponsabilidades == 0)
                return Json(new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "No eres responsable de ninguna actividad, no puedes generar comentarios."
                });
            #endregion

            //Obtener Funcionario que crear el comentario a la evidencia
            await gestionFuncionario.ObtenerFuncionarioAsync(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString());

            await gestionResponsable.ObtenerResponsableAsync(Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString()), _parametrosAprobarEvidencia.IdProyecto);

            gestionComentario.Comentario = new SiproComentarioDto
            {
                Grado = gestionFuncionario.Funcionario.GradAlfabetico,
                Apellidos = gestionFuncionario.Funcionario.Apellidos,
                Nombres = gestionFuncionario.Funcionario.Nombres,
                Consecutivo = gestionFuncionario.Funcionario.Consecutivo,
                UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo,
                UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza,
                DescripcionComentario = _parametrosAprobarEvidencia.Comentario,
                FechaCreacion = DateTime.Now,
                UsuarioComentario = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().ToUpper(),
                Identificacion = gestionFuncionario.Funcionario.Identificacion,
                IdEvidencia = _parametrosAprobarEvidencia.IdEvidencia,
                MaquinaCreacion = Request.UserHostAddress,
                Vigente = 1,
                IdTipoResponsabilidad = gestionResponsable.SiproResponsable.IdTipoResponsabilidad,
                UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString(),
                IdCometario = Guid.NewGuid().ToString(),
                IdEstado = "824595f6-6707-442e-847d-790bcd84413d"
            };


            await gestionComentario.AgregarComentario();

            if (gestionComentario.EstadoRespuesta.Estado)
            {
                GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();

                foreach (var responsable in _parametrosAprobarEvidencia.LstIdentificadorResponsables)
                {

                    await gestionResponsable.ObtenerResponsableAsync(responsable);

                    await gestionFuncionario.ObtenerFuncionarioAsync(Convert.ToInt64(gestionResponsable.SiproResponsable.Identificacion));

                    gestionEstadoComentario.EstadoComentario = new SiproEstadoComentarioDto
                    {
                        IdComentario = gestionComentario.Comentario.IdCometario,
                        IdEstado = "824595f6-6707-442e-847d-790bcd84413d",
                        IdResponsable = responsable,
                        IdTipoResponsabilidad = gestionResponsable.SiproResponsable.IdTipoResponsabilidad,
                        Consecutivo = gestionFuncionario.Funcionario.Consecutivo,
                        UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo,
                        UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza,
                        Identificacion = gestionFuncionario.Funcionario.Identificacion,
                        Descripcion = _parametrosAprobarEvidencia.Comentario,
                        FechaCreacion = DateTime.Now,
                        MaquinaCreacion = Request.UserHostAddress,
                        UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().ToUpper(),
                        Vigente = 1
                    };

                    await gestionEstadoComentario.AgregarEstadoComentario();
                }

                return Json(gestionEstadoComentario.EstadoRespuesta);
            }

            return Json(gestionComentario.EstadoRespuesta);

        }


        [HttpGet]
        public async Task<ActionResult> EvidenciaEnviada()
        {
            string _identificacion = "";
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            GestionResponsable gestionResponsable = new GestionResponsable();
            GestionActividadesResponsables gestionActividadesResponsables = new GestionActividadesResponsables();

            _identificacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString();

            await gestionEvidencias.ObtenerEnvioDucumento(_identificacion);
            ViewBag.Estado = new SelectList(new GestionEvidencias().NombreProcedimiento2((int)1), "IdDominio", "Descripcion");
            //ViewBag.ResponsablesProyecto = new SelectList(gestionResponsable.LstResonsables, nameof(SiproResponsableDto.IdResponsable), nameof(SiproResponsableDto.NombreCompletoGrado), nameof(SiproResponsableDto.Identificacion));
            ViewBag.ResponsablesProyectos = new SelectList(gestionEvidencias.LstEvidencias, nameof(SiproEvidenciaDto.IdResponsableEnvia), nameof(SiproEvidenciaDto.NombresEnvia), nameof(SiproEvidenciaDto.ApellidoEnvia));
            return View(gestionEvidencias.LstEvidencias);
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> EliminarEvidencia(SiproEvidenciaDto _siproEvidenciaDto)
        {
            GestionEvidencias gestionEvidencias = new GestionEvidencias();
            string _usuario = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString();
            await gestionEvidencias.EliminarDocumento(_siproEvidenciaDto.IdEvidencia, _usuario);

            return Json(gestionEvidencias.EstadoRespuesta);
            //return RedirectToAction("BandejaEvidencias", "Evidencia", new { _siproEvidenciaDto.IdActividad, _siproEvidenciaDto.IdProyecto });
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> IrAEvidenciaAjax(string _idComentario)
        {
            try
            {
                GestionEvidencias gestionEvidencias = new GestionEvidencias();
                GestionComentarios gestionComentarios = new GestionComentarios();
                GestionObservaciones gestionObservaciones = new GestionObservaciones();
                GestionResponsable gestionResponsable = new GestionResponsable();
                GestionProyectos gestionProyectos = new GestionProyectos();

                await gestionComentarios.ObtenerComentarioAsync(_idComentario);

                await gestionEvidencias.ObtenerEvidenciaAsync(gestionComentarios.Comentario.IdEvidencia);

                await gestionObservaciones.ObtenerObservacionAsync(gestionEvidencias.Evidencia.IdObservaciones);

                gestionEvidencias.Evidencia.Observacion = gestionObservaciones.SiproObservaciones.Descripcion;

                await gestionResponsable.ObtenerResponsableAsync(gestionEvidencias.Evidencia.IdResponsable);

                gestionEvidencias.Evidencia.NombreGradoResponsable = gestionResponsable.SiproResponsable.NombreCompletoGrado;


                await gestionProyectos.ObtenerProyectosVigentesPorIdProyectoAsync(gestionResponsable.SiproResponsable.IdProyecto);

                gestionEvidencias.Evidencia.IdProyecto = gestionProyectos.SiproProyecto.IdProyecto;
                gestionEvidencias.Evidencia.NombreProyecto = gestionProyectos.SiproProyecto.Nombre;
                gestionEvidencias.Evidencia.DescripcionProyecto = gestionProyectos.SiproProyecto.Descripcion;
                gestionEvidencias.Evidencia.IdComentario = _idComentario;

                EstadoRespuesta estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = "Datos Encontrados",
                    Objeto = gestionEvidencias.Evidencia
                };

                return Json(estadoRespuesta, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                EstadoRespuesta estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = "Datos no Encontrados"
                };
                return Json(estadoRespuesta, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> AprobarEvidenciaAjax(string _idComentario, string _idProyecto)
        {
            GestionComentarios gestionComentarios = new GestionComentarios();
            GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();
            GestionFuncionarios gestionFuncionario = new GestionFuncionarios();
            GestionResponsable gestionResponsable = new GestionResponsable();
            
            if (await gestionEstadoComentario.ConsultarComentarioAprobadoCorregido(_idComentario)) {
                EstadoRespuesta estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 0,
                    Mensaje = "La evidencia fue aprobada o corregida, por favor validar.",
                    Objeto= false
                };
                return Json(estadoRespuesta, JsonRequestBehavior.AllowGet);
            }

            await gestionComentarios.ObtenerComentarioAsync(_idComentario);

            gestionComentarios.Comentario.IdEstado = "1ca8b15a-bfda-492e-b5a3-9e67348f66op";

            await gestionComentarios.AprobarComentarioAsync();

            if (gestionComentarios.EstadoRespuesta.Estado) {
                await gestionFuncionario.ObtenerFuncionarioAsync(Convert.ToInt64(Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString())));

                await gestionResponsable.ObtenerResponsableIdentificacionIdProyectoAsync(Convert.ToDecimal(gestionFuncionario.Funcionario.Identificacion), _idProyecto);

                gestionEstadoComentario.EstadoComentario = new SiproEstadoComentarioDto
                {
                    IdComentario = gestionComentarios.Comentario.IdCometario,
                    IdEstado = "1ca8b15a-bfda-492e-b5a3-9e67348f66op",
                    IdResponsable = gestionResponsable.SiproResponsable.IdResponsable,
                    IdTipoResponsabilidad = gestionResponsable.SiproResponsable.IdTipoResponsabilidad,
                    Consecutivo = gestionFuncionario.Funcionario.Consecutivo,
                    UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo,
                    UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza,
                    Identificacion = gestionFuncionario.Funcionario.Identificacion,
                    Descripcion = "La evidencia es aprobada.",
                    FechaCreacion = DateTime.Now,
                    MaquinaCreacion = Request.UserHostAddress,
                    UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().ToUpper(),
                    Vigente = 1
                };

                await gestionEstadoComentario.AgregarEstadoComentario();
            }

         

            return Json(gestionComentarios.EstadoRespuesta, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        [Authorize]
        public async Task<ActionResult> CorregirEvidenciaAjax(CorregirEvidenciaDto _corregirEvidencia)
        {
            GestionComentarios gestionComentarios = new GestionComentarios();
            GestionEstadoComentario gestionEstadoComentario = new GestionEstadoComentario();
            GestionFuncionarios gestionFuncionario = new GestionFuncionarios();
            GestionResponsable gestionResponsable = new GestionResponsable();

            if (await gestionEstadoComentario.ConsultarComentarioAprobadoCorregido(_corregirEvidencia.IdComentario))
            {
                EstadoRespuesta estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 0,
                    Mensaje = "La evidencia fue aprobada o corregida, por favor validar.",
                    Objeto = false
                };
                return Json(estadoRespuesta, JsonRequestBehavior.AllowGet);

            }
            await gestionComentarios.ObtenerComentarioAsync(_corregirEvidencia.IdComentario);

            gestionComentarios.Comentario.IdEstado = "213d4e1b-f138-40c7-b2fb-5794e7ed1f25";

            await gestionComentarios.AprobarComentarioAsync();

            if (gestionComentarios.EstadoRespuesta.Estado)
            {
                await gestionFuncionario.ObtenerFuncionarioAsync(Convert.ToInt64(Convert.ToDecimal(gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.Identificacion).ToString())));

                await gestionResponsable.ObtenerResponsableIdentificacionIdProyectoAsync(Convert.ToDecimal(gestionFuncionario.Funcionario.Identificacion), _corregirEvidencia.IdProyecto);

                gestionEstadoComentario.EstadoComentario = new SiproEstadoComentarioDto
                {
                    IdComentario = gestionComentarios.Comentario.IdCometario,
                    IdEstado = "213d4e1b-f138-40c7-b2fb-5794e7ed1f25",
                    IdResponsable = gestionResponsable.SiproResponsable.IdResponsable,
                    IdTipoResponsabilidad = gestionResponsable.SiproResponsable.IdTipoResponsabilidad,
                    Consecutivo = gestionFuncionario.Funcionario.Consecutivo,
                    UndeConsecutivo = gestionFuncionario.Funcionario.UndeConsecutivo,
                    UndeFuerza = gestionFuncionario.Funcionario.UndeFuerza,
                    Identificacion = gestionFuncionario.Funcionario.Identificacion,
                    Descripcion = _corregirEvidencia.DescripcionComentario,
                    FechaCreacion = DateTime.Now,
                    MaquinaCreacion = Request.UserHostAddress,
                    UsuarioCreacion = gestionClaims.ObtenerClaim(ClaimPersonalizadoDTO.UsuarioEmpresarial).ToString().ToUpper(),
                    Vigente = 1
                };

                await gestionEstadoComentario.AgregarEstadoComentario();
            }



            return Json(gestionComentarios.EstadoRespuesta, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}