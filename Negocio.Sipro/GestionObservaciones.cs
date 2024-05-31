namespace Negocio.Sipro
{
    using Comun.Sipro.Dto;
    using Comun.Sipro.Utilidades;
    using Datos.Sipro;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class GestionObservaciones
    {
        #region Atributos
        private List<SiproObservacionesDto> lstSiproObservaciones;
        private EstadoRespuesta estadoRespuesta;
        private SiproObservacionesDto siproObservaciones;
        #endregion

        #region Propiedades
        public List<SiproObservacionesDto> LstSiproObservaciones
        {
            get
            {
                return this.lstSiproObservaciones;
            }
            set
            {
                this.lstSiproObservaciones = value;
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

        public SiproObservacionesDto SiproObservaciones
        {
            get
            {
                return this.siproObservaciones;
            }
            set
            {
                this.siproObservaciones = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerObservacionesVigentesAsync()
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstSiproObservaciones = await (from fase in db.SiproObservaciones
                                                        where fase.Vigente == EstadoRegistro.VIGENTE
                                                        select new SiproObservacionesDto
                                                        {
                                                            Descripcion = fase.Descripcion,
                                                            FechaCreacion = fase.FechaCreacion,
                                                            IdObservacion = fase.IdObservacion,
                                                            MaquinaCreacion = fase.MaquinaCreacion,
                                                            UsuarioCreacion = fase.UsuarioCreacion,
                                                            Vigente = fase.Vigente
                                                        }).ToListAsync();


                    this.estadoRespuesta = new EstadoRespuesta
                    {
                        Codigo = 1,
                        Estado = true,
                        Mensaje = "Registros Obtenidos"
                    };
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio Una excepción: {ex.Message}"
                };
            }
        }

        public async Task ObtenerObservacionAsync(string _idObservacion)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.siproObservaciones = await (from observacion in db.SiproObservaciones
                                                     where observacion.IdObservacion == _idObservacion
                                                     && observacion.Vigente == EstadoRegistro.VIGENTE
                                                     select new SiproObservacionesDto
                                                     {
                                                         Descripcion = observacion.Descripcion,
                                                         FechaCreacion = observacion.FechaCreacion,
                                                         IdObservacion = observacion.IdObservacion,
                                                         MaquinaCreacion = observacion.MaquinaCreacion,
                                                         UsuarioCreacion = observacion.UsuarioCreacion,
                                                         Vigente = observacion.Vigente
                                                     }).FirstOrDefaultAsync();

                    if (this.siproObservaciones != null)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El registro fue encontrado."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue encontrado."
                        };
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio Una excepción: {ex.Message}"
                };
            }
        }

        public async Task AgregarObservacionAsync()
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproObservaciones
                    {
                        Descripcion = this.siproObservaciones.Descripcion.ToUpper(),
                        FechaCreacion = DateTime.Now,
                        IdObservacion = this.siproObservaciones.IdObservacion,
                        IdProyecto = this.siproObservaciones.IdProyecto,
                        MaquinaCreacion = this.siproObservaciones.MaquinaCreacion,
                        UsuarioCreacion = this.siproObservaciones.UsuarioCreacion.ToUpper(),
                        Vigente = EstadoRegistro.VIGENTE
                    }).State = EntityState.Added;

                    if (await db.SaveChangesAsync() != 0)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El Registro Agregado Correctamente."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue agregado."
                        };

                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio Una excepción: {ex.Message}"
                };
            }
        }

        public async Task ObtenerDescripcionProyectoAsync(string _idProyecto)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.siproObservaciones = await (from observacion in db.SiproObservaciones
                                                     where observacion.IdProyecto == _idProyecto &&
                                                     observacion.Vigente == EstadoRegistro.VIGENTE
                                                     select new SiproObservacionesDto
                                                     {
                                                         Descripcion = observacion.Descripcion,
                                                         FechaCreacion = observacion.FechaCreacion,
                                                         IdObservacion = observacion.IdObservacion,
                                                         IdProyecto = observacion.IdProyecto,
                                                         MaquinaCreacion = observacion.MaquinaCreacion,
                                                         UsuarioCreacion = observacion.UsuarioCreacion,
                                                         Vigente = observacion.Vigente
                                                     }).FirstOrDefaultAsync();

                    if (this.siproObservaciones != null)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = this.siproObservaciones
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue encontrado."
                        };
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio Una excepción: {ex.Message}"
                };
            }



        }
        #endregion
    }
}
