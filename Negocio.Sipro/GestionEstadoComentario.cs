namespace Negocio.Sipro
{
    using Comun.Sipro;
    using Comun.Sipro.Utilidades;
    using Datos.Sipro;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class GestionEstadoComentario
    {
        #region Atributos
        private SiproEstadoComentarioDto estadoComentario;
        private EstadoRespuesta estadoRespuesta;
        private List<SiproEstadoComentarioDto> lstEstadoComentario;
        #endregion

        #region Propiedades
        public SiproEstadoComentarioDto EstadoComentario
        {
            get
            {
                return this.estadoComentario;
            }
            set
            {
                this.estadoComentario = value;
            }
        }

        public EstadoRespuesta EstadoRespuesta
        {
            get
            {
                return this.estadoRespuesta;
            }
            set
            {
                this.estadoRespuesta = value;
            }
        }

        public List<SiproEstadoComentarioDto> LstEstadoComentario
        {
            get
            {
                return this.lstEstadoComentario;
            }
            set
            {
                this.lstEstadoComentario = value;
            }
        }
        #endregion

        #region Metodos Externos
        /// <summary>
        /// Métodos para agregar estados comentarios
        /// </summary>
        /// <returns></returns>
        public async Task AgregarEstadoComentario()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry<SiproEstadoComentario>(new SiproEstadoComentario
                    {
                        IdEstadoComentario = Guid.NewGuid().ToString(),
                        Descripcion = this.estadoComentario.Descripcion,
                        Vigente = 1,
                        FechaCreacion = DateTime.Now,
                        UsuarioCreacion = this.estadoComentario.UsuarioCreacion.ToUpper(),
                        MaquinaCreacion = this.estadoComentario.MaquinaCreacion,
                        Consecutivo = this.estadoComentario.Consecutivo,
                        UndeConsecutivo = this.estadoComentario.UndeConsecutivo,
                        UndeFuerza = this.estadoComentario.UndeFuerza,
                        IdComentario = this.estadoComentario.IdComentario,
                        IdEstado = this.estadoComentario.IdEstado,
                        IdTipoResponsabilidad = this.estadoComentario.IdTipoResponsabilidad,
                        IdResponsable = this.estadoComentario.IdResponsable,
                        Identificacion = this.estadoComentario.Identificacion
                    }).State = EntityState.Added;


                    if (await db.SaveChangesAsync() != 0)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El registro fue guardado exitosamente."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue guardado exitosamente."
                        };
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio una excepción : {ex.Message} - {ex.InnerException}"
                };
            }
        }

        /// <summary>
        /// Método para obtener el estado comentario asignados por identificador
        /// </summary>
        /// <param name="_identificacion"></param>
        /// <returns></returns>
        public async Task ObtenerEstadoComentariosAsignadosAsync(decimal _identificacion)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.lstEstadoComentario = await (from Estadocomentarios in db.SiproEstadoComentario
                                                  from comen in db.SiproComentario
                                                  where Estadocomentarios.Identificacion == _identificacion
                                                  && Estadocomentarios.IdEstado == "824595f6-6707-442e-847d-790bcd84413d" //Estado En proceso
                                                  && Estadocomentarios.Vigente == 1
                                                  && comen.IdCometario == Estadocomentarios.IdComentario
                                                  && comen.IdEstado == "824595f6-6707-442e-847d-790bcd84413d"
                                                  select new SiproEstadoComentarioDto
                                                  {
                                                      Consecutivo = Estadocomentarios.Consecutivo,
                                                      Descripcion = Estadocomentarios.Descripcion,
                                                      FechaCreacion = Estadocomentarios.FechaCreacion,
                                                      IdComentario = Estadocomentarios.IdComentario,
                                                      Identificacion = Estadocomentarios.Identificacion,
                                                      IdEstado = Estadocomentarios.IdEstado,
                                                      IdEstadoComentario = Estadocomentarios.IdEstadoComentario,
                                                      IdResponsable = Estadocomentarios.IdResponsable,
                                                      IdTipoResponsabilidad = Estadocomentarios.IdTipoResponsabilidad,
                                                      MaquinaCreacion = Estadocomentarios.MaquinaCreacion,
                                                      UndeConsecutivo = Estadocomentarios.UndeConsecutivo,
                                                      UndeFuerza = Estadocomentarios.UndeFuerza,
                                                      UsuarioCreacion = Estadocomentarios.UsuarioCreacion,
                                                      Vigente = Estadocomentarios.Vigente
                                                  }).ToListAsync();


                if (this.lstEstadoComentario.Count > 0)
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 1,
                        Estado = true,
                        Mensaje = "Se encontraron registros"
                    };

                else
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = "No se encontraron registros"
                    };

            }
        }

        /// <summary>
        /// Método para obtener estado comentario para corregir por identificación
        /// </summary>
        /// <param name="_identificacion"></param>
        /// <returns></returns>
        public async Task ObtenerEstadoComentariosParaCorregirAsync(decimal _identificacion)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.lstEstadoComentario = await (from comentarios in db.SiproEstadoComentario
                                                  where comentarios.Identificacion == _identificacion
                                                  && comentarios.IdEstado == "213d4e1b-f138-40c7-b2fb-5794e7ed1f25" //Estado Para Corregir
                                                  && comentarios.Vigente == 1
                                                  select new SiproEstadoComentarioDto
                                                  {
                                                      Consecutivo = comentarios.Consecutivo,
                                                      Descripcion = comentarios.Descripcion,
                                                      FechaCreacion = comentarios.FechaCreacion,
                                                      IdComentario = comentarios.IdComentario,
                                                      Identificacion = comentarios.Identificacion,
                                                      IdEstado = comentarios.IdEstado,
                                                      IdEstadoComentario = comentarios.IdEstadoComentario,
                                                      IdResponsable = comentarios.IdResponsable,
                                                      IdTipoResponsabilidad = comentarios.IdTipoResponsabilidad,
                                                      MaquinaCreacion = comentarios.MaquinaCreacion,
                                                      UndeConsecutivo = comentarios.UndeConsecutivo,
                                                      UndeFuerza = comentarios.UndeFuerza,
                                                      UsuarioCreacion = comentarios.UsuarioCreacion,
                                                      Vigente = comentarios.Vigente
                                                  }).ToListAsync();


                if (this.lstEstadoComentario.Count > 0)
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 1,
                        Estado = true,
                        Mensaje = "Se encontraron registros"
                    };

                else
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = "No se encontraron registros"
                    };

            }
        }

        /// <summary>
        /// Método para consultar comentarios aprobados corregidos por identificación
        /// </summary>
        /// <param name="_idComentario"></param>
        /// <returns></returns>
        public async Task<bool> ConsultarComentarioAprobadoCorregido(string _idComentario)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                if (db.SiproEstadoComentario.Where(x => x.IdComentario == _idComentario).Any(x => (x.IdEstado == "213d4e1b-f138-40c7-b2fb-5794e7ed1f25" || x.IdEstado == "1ca8b15a-bfda-492e-b5a3-9e67348f66op")))
                {
                    return await Task.FromResult(true);
                }

                return await Task.FromResult(false);
            }
        }

        /// <summary>
        /// Método para obtener trazabilidad comentarios por id del comentario
        /// </summary>
        /// <param name="_idComentario"></param>
        /// <returns></returns>
        public async Task ObtenerTrazabilidadAsync(string _idComentario)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.lstEstadoComentario = await (from Estadocomentarios in db.SiproEstadoComentario                                                
                                                  where Estadocomentarios.IdComentario == _idComentario
                                                  && Estadocomentarios.Vigente == 1                                               
                                                  select new SiproEstadoComentarioDto
                                                  {
                                                      Consecutivo = Estadocomentarios.Consecutivo,
                                                      Descripcion = Estadocomentarios.Descripcion,
                                                      FechaCreacion = Estadocomentarios.FechaCreacion,
                                                      IdComentario = Estadocomentarios.IdComentario,
                                                      Identificacion = Estadocomentarios.Identificacion,
                                                      IdEstado = Estadocomentarios.IdEstado,
                                                      IdEstadoComentario = Estadocomentarios.IdEstadoComentario,
                                                      IdResponsable = Estadocomentarios.IdResponsable,
                                                      IdTipoResponsabilidad = Estadocomentarios.IdTipoResponsabilidad,
                                                      MaquinaCreacion = Estadocomentarios.MaquinaCreacion,
                                                      UndeConsecutivo = Estadocomentarios.UndeConsecutivo,
                                                      UndeFuerza = Estadocomentarios.UndeFuerza,
                                                      UsuarioCreacion = Estadocomentarios.UsuarioCreacion,
                                                      Vigente = Estadocomentarios.Vigente
                                                  }).ToListAsync();


                if (this.lstEstadoComentario.Count > 0)
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 1,
                        Estado = true,
                        Mensaje = "Se encontraron registros",
                        Objeto = this.lstEstadoComentario
                    };

                else
                    this.estadoRespuesta = new EstadoRespuesta()
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = "No se encontraron registros"
                    };

            }
        }

        #endregion
    }
}
