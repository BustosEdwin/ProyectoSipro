namespace Negocio.Sipro
{
    using Comun.Sipro.Dto;
    using Comun.Sipro.Utilidades;
    using Datos.Sipro;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    public class GestionActividadesResponsables
    {
        #region Atributos
        private List<SiproBitacoResponsablesDto> lstActividadesBitacora;
        private EstadoRespuesta estadoRespuesta;
        private SiproBitacoResponsablesDto actividadBitacora;
        private SiproComentarioDto actividadComentario;

        #endregion

        #region Propiedades
        public List<SiproBitacoResponsablesDto> LstActividadesBitacora
        {
            get
            {
                return this.lstActividadesBitacora;
            }
            set
            {
                this.lstActividadesBitacora = value;
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

        public SiproBitacoResponsablesDto ActividadBitacora
        {
            get
            {
                return this.actividadBitacora;
            }
            set
            {
                this.actividadBitacora = value;
            }
        }


        #endregion

        #region Metodos Externos

        /// <summary>
        /// Método para obtener actividades vigentes por proyecto teniendo en cuenta el id de la actividad
        /// </summary>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public async Task ObtenerActividadesVigentesProyectoAsync(string _idActividad)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstActividadesBitacora = await (from bitacora in db.SiproBitacoResponsables
                                                         where bitacora.Vigente == EstadoRegistro.VIGENTE
                                                         && bitacora.IdBitacora == _idActividad
                                                         select new SiproBitacoResponsablesDto
                                                         {
                                                             FechaCreacion = bitacora.FechaCreacion,
                                                             Firma = bitacora.Firma,
                                                             IdBitacora = bitacora.IdBitacora,
                                                             IdBitacoResponsable = bitacora.IdBitacoResponsable,
                                                             IdResponsable = bitacora.IdResponsable,
                                                             MaquinaCreacion = bitacora.MaquinaCreacion,
                                                             UsuarioCreacion = bitacora.UsuarioCreacion,
                                                             Vigente = bitacora.Vigente,
                                                             Grado = bitacora.ResponsableBitacoResponsable.Grado,
                                                             Nombres = bitacora.ResponsableBitacoResponsable.Nombres,
                                                             Apellidos = bitacora.ResponsableBitacoResponsable.Apellidos,
                                                             Cargo = bitacora.ResponsableBitacoResponsable.Cargo,
                                                             TipoResponsabilidad = bitacora.ResponsableBitacoResponsable.ResponsableTipoResponsabilidad.Descripcion,
                                                             Identificacion = bitacora.ResponsableBitacoResponsable.Identificacion,
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

        /// <summary>
        /// Método para agregar actividad a responsable
        /// </summary>
        /// <returns></returns>
        public async Task AgregarActividadResponsableAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    if (db.SiproBitacoResponsables.Any(x => x.IdBitacora == this.actividadBitacora.IdBitacora && x.IdResponsable == this.actividadBitacora.IdResponsable)) {
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro ya fue agregado"
                        };
                    }
                    else
                    {
                        db.Entry(new SiproBitacoResponsables()
                        {
                            FechaCreacion = DateTime.Now,
                            Firma = this.actividadBitacora.Firma,
                            IdBitacora = this.actividadBitacora.IdBitacora,
                            IdBitacoResponsable = this.actividadBitacora.IdBitacoResponsable,
                            IdResponsable = this.actividadBitacora.IdResponsable,
                            MaquinaCreacion = this.actividadBitacora.MaquinaCreacion,
                            UsuarioCreacion = this.actividadBitacora.UsuarioCreacion.ToUpper(),
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

        /// <summary>
        /// Firmar actividad por el responsable
        /// </summary>
        /// <param name="_idResponsable"></param>
        /// <param name="_idActividad"></param>
        /// <returns></returns>
        public async Task FimarActividadResponsableAsync(string _idResponsable, string _idActividad)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    SiproBitacoResponsables bitacoraResponsableObtenido = await (from bitacoraResonsable in db.SiproBitacoResponsables
                                                                                 where bitacoraResonsable.IdResponsable == _idResponsable
                                                                                 && bitacoraResonsable.IdBitacora == _idActividad
                                                                                 select bitacoraResonsable).FirstOrDefaultAsync();

                    bitacoraResponsableObtenido.Firma = Firma.SI;

                    db.Entry(bitacoraResponsableObtenido).State = EntityState.Modified;

                    if (await db.SaveChangesAsync() != 0)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El Registro fue firmado."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue firmado."
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
