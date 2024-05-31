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

    public class GestionResponsable
    {
        #region Atributos
        private List<SiproResponsableDto> lstResonsables;
        private EstadoRespuesta estadoRespuesta;
        private SiproResponsableDto siproResponsable;
        #endregion

        #region Propiedades
        public List<SiproResponsableDto> LstResonsables
        {
            get
            {
                return this.lstResonsables;
            }
            set
            {
                this.lstResonsables = value;
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

        public SiproResponsableDto SiproResponsable
        {
            get
            {
                return this.siproResponsable;
            }
            set
            {
                this.siproResponsable = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task AgregarResponsableAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    //if(db.SiproResponsable.Any(x => x.Identificacion == this.siproResponsable.Identificacion && x.i))

                    db.Entry(new SiproResponsable
                    {
                        Apellidos = this.siproResponsable.Apellidos,
                        Cargo = this.siproResponsable.Cargo.ToUpper(),
                        Consecutivo = this.siproResponsable.Consecutivo,
                        FechaAsignacion = this.siproResponsable.FechaAsignacion,
                        FechaCreacion = this.siproResponsable.FechaCreacion,
                        Grado = this.siproResponsable.Grado,
                        Identificacion = this.siproResponsable.Identificacion,
                        IdProyecto = this.siproResponsable.IdProyecto,
                        IdResponsable = this.siproResponsable.IdResponsable,
                        IdTipoResponsable = this.siproResponsable.IdTipoResponsable,
                        IdUnidad = this.siproResponsable.IdUnidad,
                        MaquinaCreacion = this.siproResponsable.MaquinaCreacion,
                        Nombres = this.siproResponsable.Nombres,
                        UndeConsecutivo = this.siproResponsable.UndeConsecutivo,
                        UndeFuerza = this.siproResponsable.UndeFuerza,
                        UsuarioCreacion = this.siproResponsable.UsuarioCreacion.ToUpper(),
                        Vigente = this.siproResponsable.Vigente,
                        Activo = true
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

        public async Task FinalizarResponsableAsync(SiproResponsableDto siproResponsableDto)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    var responsable = await db.SiproResponsable.FirstOrDefaultAsync(x => x.IdResponsable == siproResponsableDto.IdResponsable);
                    responsable.FechaFin = DateTime.Now;
                    responsable.Observaciones = siproResponsableDto.Observaciones;
                    responsable.Activo = false;

                    db.Entry(responsable).State = EntityState.Modified;

                    if (await db.SaveChangesAsync() != 0)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El Registro Actualizado Correctamente."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue actualizado."
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


        public async Task ObtenerResponsablesProyectoVigentesAsync(string _idProyecto, int _opciones = -1)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    switch (_opciones)
                    {
                        //para los vigentes y activos
                        case 1:
                            this.lstResonsables = await (from responsable in db.SiproResponsable
                                                         where responsable.IdProyecto == _idProyecto
                                                         && responsable.Activo == true
                                                         && responsable.Vigente == EstadoRegistro.VIGENTE
                                                         orderby responsable.FechaCreacion ascending
                                                         select new SiproResponsableDto
                                                         {
                                                             Apellidos = responsable.Apellidos,
                                                             Cargo = responsable.Cargo,
                                                             Consecutivo = responsable.Consecutivo,
                                                             FechaAsignacion = responsable.FechaAsignacion,
                                                             FechaCreacion = responsable.FechaCreacion,
                                                             Grado = responsable.Grado,
                                                             Identificacion = responsable.Identificacion,
                                                             IdProyecto = responsable.IdProyecto,
                                                             IdResponsable = responsable.IdResponsable,
                                                             IdTipoResponsable = responsable.IdTipoResponsable,
                                                             IdUnidad = responsable.IdUnidad,
                                                             MaquinaCreacion = responsable.MaquinaCreacion,
                                                             Nombres = responsable.Nombres,
                                                             UndeConsecutivo = responsable.UndeConsecutivo,
                                                             UndeFuerza = responsable.UndeFuerza,
                                                             UsuarioCreacion = responsable.UsuarioCreacion,
                                                             Vigente = responsable.Vigente,
                                                             Activo = responsable.Activo,
                                                             FechaFin = responsable.FechaFin,
                                                             Observaciones = responsable.Observaciones,
                                                             TipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.Descripcion,
                                                             IdTipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.IdTipoResponsabilidad
                                                         }).ToListAsync();
                            break;
                        // para los vigentes y no activos
                        case 0:
                            this.lstResonsables = await (from responsable in db.SiproResponsable
                                                         where responsable.IdProyecto == _idProyecto
                                                         && responsable.Activo == false
                                                         && responsable.Vigente == EstadoRegistro.VIGENTE
                                                         orderby responsable.FechaCreacion ascending
                                                         select new SiproResponsableDto
                                                         {
                                                             Apellidos = responsable.Apellidos,
                                                             Cargo = responsable.Cargo,
                                                             Consecutivo = responsable.Consecutivo,
                                                             FechaAsignacion = responsable.FechaAsignacion,
                                                             FechaCreacion = responsable.FechaCreacion,
                                                             Grado = responsable.Grado,
                                                             Identificacion = responsable.Identificacion,
                                                             IdProyecto = responsable.IdProyecto,
                                                             IdResponsable = responsable.IdResponsable,
                                                             IdTipoResponsable = responsable.IdTipoResponsable,
                                                             IdUnidad = responsable.IdUnidad,
                                                             MaquinaCreacion = responsable.MaquinaCreacion,
                                                             Nombres = responsable.Nombres,
                                                             UndeConsecutivo = responsable.UndeConsecutivo,
                                                             UndeFuerza = responsable.UndeFuerza,
                                                             UsuarioCreacion = responsable.UsuarioCreacion,
                                                             Vigente = responsable.Vigente,
                                                             Activo = responsable.Activo,
                                                             FechaFin = responsable.FechaFin,
                                                             Observaciones = responsable.Observaciones,
                                                             TipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.Descripcion,
                                                             IdTipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.IdTipoResponsabilidad
                                                         }).ToListAsync();
                            break;
                        // para todos los vigentes
                        default:
                            this.lstResonsables = await (from responsable in db.SiproResponsable
                                                         where responsable.IdProyecto == _idProyecto
                                                         && responsable.Vigente == EstadoRegistro.VIGENTE
                                                         orderby responsable.FechaCreacion ascending
                                                         select new SiproResponsableDto
                                                         {
                                                             Apellidos = responsable.Apellidos,
                                                             Cargo = responsable.Cargo,
                                                             Consecutivo = responsable.Consecutivo,
                                                             FechaAsignacion = responsable.FechaAsignacion,
                                                             FechaCreacion = responsable.FechaCreacion,
                                                             Grado = responsable.Grado,
                                                             Identificacion = responsable.Identificacion,
                                                             IdProyecto = responsable.IdProyecto,
                                                             IdResponsable = responsable.IdResponsable,
                                                             IdTipoResponsable = responsable.IdTipoResponsable,
                                                             IdUnidad = responsable.IdUnidad,
                                                             MaquinaCreacion = responsable.MaquinaCreacion,
                                                             Nombres = responsable.Nombres,
                                                             UndeConsecutivo = responsable.UndeConsecutivo,
                                                             UndeFuerza = responsable.UndeFuerza,
                                                             UsuarioCreacion = responsable.UsuarioCreacion,
                                                             Vigente = responsable.Vigente,
                                                             Activo = responsable.Activo,
                                                             FechaFin = responsable.FechaFin,
                                                             Observaciones = responsable.Observaciones,
                                                             TipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.Descripcion,
                                                             IdTipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.IdTipoResponsabilidad
                                                         }).ToListAsync();
                            break;
                    }


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


        public async Task ObtenerResponsablesComentarioAsync(string _idProyecto)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.lstResonsables = await (from responsable in db.SiproResponsable
                                             where responsable.IdProyecto == _idProyecto
                                             && responsable.Vigente == EstadoRegistro.VIGENTE
                                             && responsable.IdTipoResponsable == "ad5ac280-755c-4fec-8fec-59b3813ba25d"
                                             //|| responsable.IdTipoResponsable == "d1e00f11-b7f3-410e-9dd8-d2d2d27f6e9e") SE QUITA TEMPORALMENTE
                                             && responsable.Activo == true
                                             orderby responsable.FechaCreacion ascending
                                             select new SiproResponsableDto
                                             {
                                                 Apellidos = responsable.Apellidos,
                                                 Cargo = responsable.Cargo,
                                                 Consecutivo = responsable.Consecutivo,
                                                 FechaAsignacion = responsable.FechaAsignacion,
                                                 FechaCreacion = responsable.FechaCreacion,
                                                 Grado = responsable.Grado,
                                                 Identificacion = responsable.Identificacion,
                                                 IdProyecto = responsable.IdProyecto,
                                                 IdResponsable = responsable.IdResponsable,
                                                 IdTipoResponsable = responsable.IdTipoResponsable,
                                                 IdUnidad = responsable.IdUnidad,
                                                 MaquinaCreacion = responsable.MaquinaCreacion,
                                                 Nombres = responsable.Nombres,
                                                 UndeConsecutivo = responsable.UndeConsecutivo,
                                                 UndeFuerza = responsable.UndeFuerza,
                                                 UsuarioCreacion = responsable.UsuarioCreacion,
                                                 Vigente = responsable.Vigente,
                                                 Activo = responsable.Activo,
                                                 FechaFin = responsable.FechaFin,
                                                 Observaciones = responsable.Observaciones,
                                                 TipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.Descripcion,
                                                 IdTipoResponsabilidad = responsable.ResponsableTipoResponsabilidad.IdTipoResponsabilidad
                                             }).ToListAsync();

            }
            this.estadoRespuesta = new EstadoRespuesta
            {
                Codigo = 1,
                Estado = true,
                Mensaje = "Registros Obtenidos"
            };
        }


        public async Task ObtenerResponsableAsync(string _idResponsable)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.siproResponsable = await (from responsable in db.SiproResponsable
                                               where responsable.IdResponsable == _idResponsable
                                               select new SiproResponsableDto
                                               {
                                                   Activo = responsable.Activo,
                                                   IdResponsable = responsable.IdResponsable,
                                                   Apellidos = responsable.Apellidos,
                                                   Cargo = responsable.Cargo,
                                                   Consecutivo = responsable.Consecutivo,
                                                   FechaAsignacion = responsable.FechaAsignacion,
                                                   FechaCreacion = responsable.FechaCreacion,
                                                   FechaFin = responsable.FechaFin,
                                                   Grado = responsable.Grado,
                                                   Identificacion = responsable.Identificacion,
                                                   IdProyecto = responsable.IdProyecto,
                                                   IdTipoResponsabilidad = responsable.IdTipoResponsable,
                                                   UndeConsecutivo = responsable.UndeConsecutivo,
                                                   UndeFuerza = responsable.UndeFuerza,
                                                   MaquinaCreacion = responsable.MaquinaCreacion,
                                                   Nombres = responsable.Nombres,
                                                   IdUnidad = responsable.IdUnidad,
                                                   UsuarioCreacion = responsable.UsuarioCreacion,
                                                   Vigente = responsable.Vigente,
                                                   Observaciones = responsable.Observaciones
                                               }).FirstOrDefaultAsync();

                if (this.siproResponsable != null)
                    this.estadoRespuesta = new EstadoRespuesta
                    {
                        Codigo = 1,
                        Estado = true,
                        Mensaje = "Ya eres responsable de esta actividad."
                    };
                else
                    this.estadoRespuesta = new EstadoRespuesta
                    {
                        Codigo = 0,
                        Estado = false,
                        Mensaje = "No existen registros."
                    };
            }
        }

        public async Task ObtenerResponsableAsync(decimal _identificacion, string _idProyecto)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.siproResponsable = await (from responsable in db.SiproResponsable
                                               where responsable.Identificacion == _identificacion
                                               && responsable.IdProyecto == _idProyecto
                                               select new SiproResponsableDto
                                               {
                                                   Activo = responsable.Activo,
                                                   IdResponsable = responsable.IdResponsable,
                                                   Apellidos = responsable.Apellidos,
                                                   Cargo = responsable.Cargo,
                                                   Consecutivo = responsable.Consecutivo,
                                                   FechaAsignacion = responsable.FechaAsignacion,
                                                   FechaCreacion = responsable.FechaCreacion,
                                                   FechaFin = responsable.FechaFin,
                                                   Grado = responsable.Grado,
                                                   Identificacion = responsable.Identificacion,
                                                   IdProyecto = responsable.IdProyecto,
                                                   IdTipoResponsabilidad = responsable.IdTipoResponsable,
                                                   UndeConsecutivo = responsable.UndeConsecutivo,
                                                   UndeFuerza = responsable.UndeFuerza,
                                                   MaquinaCreacion = responsable.MaquinaCreacion,
                                                   Nombres = responsable.Nombres,
                                                   IdUnidad = responsable.IdUnidad,
                                                   UsuarioCreacion = responsable.UsuarioCreacion,
                                                   Vigente = responsable.Vigente,
                                                   Observaciones = responsable.Observaciones
                                               }).OrderByDescending(x => x.FechaCreacion).FirstOrDefaultAsync();

            }
        }


        public async Task ObtenerResponsableIdentificacionIdProyectoAsync(decimal _identificacion, string _idProyecto)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.siproResponsable = await (from responsable in db.SiproResponsable
                                               where responsable.Identificacion == _identificacion
                                               && responsable.IdProyecto == _idProyecto
                                               && responsable.IdProyecto == _idProyecto
                                               && responsable.Vigente == 1
                                               select new SiproResponsableDto
                                               {
                                                   Activo = responsable.Activo,
                                                   IdResponsable = responsable.IdResponsable,
                                                   Apellidos = responsable.Apellidos,
                                                   Cargo = responsable.Cargo,
                                                   Consecutivo = responsable.Consecutivo,
                                                   FechaAsignacion = responsable.FechaAsignacion,
                                                   FechaCreacion = responsable.FechaCreacion,
                                                   FechaFin = responsable.FechaFin,
                                                   Grado = responsable.Grado,
                                                   Identificacion = responsable.Identificacion,
                                                   IdProyecto = responsable.IdProyecto,
                                                   IdTipoResponsabilidad = responsable.IdTipoResponsable,
                                                   UndeConsecutivo = responsable.UndeConsecutivo,
                                                   UndeFuerza = responsable.UndeFuerza,
                                                   MaquinaCreacion = responsable.MaquinaCreacion,
                                                   Nombres = responsable.Nombres,
                                                   IdUnidad = responsable.IdUnidad,
                                                   UsuarioCreacion = responsable.UsuarioCreacion,
                                                   Vigente = responsable.Vigente,
                                                   Observaciones = responsable.Observaciones
                                               }).OrderByDescending(x => x.FechaCreacion).FirstOrDefaultAsync();

            }
        }
    }

    #endregion
}

