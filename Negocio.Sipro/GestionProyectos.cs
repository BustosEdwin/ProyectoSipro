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

    public class GestionProyectos
    {
        #region Atributos
        private List<SiproProyectoDto> lstProyectos;
        private EstadoRespuesta estadoRespuesta;
        private SiproProyectoDto siproProyecto;
        #endregion

        #region Propiedades
        public List<SiproProyectoDto> LstProyectos
        {
            get
            {
                return this.lstProyectos;
            }
            set
            {
                this.lstProyectos = value;
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

        public SiproProyectoDto SiproProyecto
        {
            get
            {
                return this.siproProyecto;
            }
            set
            {
                this.siproProyecto = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerProyectosVigentesAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstProyectos = await (from proyecto in db.SiproProyecto
                                               join personal in db.VmRehuPersonal
                                               on proyecto.Identificacion equals personal.Identificacion
                                               where proyecto.Vigente == EstadoRegistro.VIGENTE
                                               select new SiproProyectoDto
                                               {
                                                   FechaCreacion = proyecto.FechaCreacion,
                                                   FechaInicio = proyecto.FechaInicio,
                                                   IdEstado = proyecto.IdEstado,
                                                   IdProyecto = proyecto.IdProyecto,
                                                   MaquinaCreacion = proyecto.MaquinaCreacion,
                                                   Nombre = proyecto.Nombre,
                                                   UsuarioCreacion = proyecto.UsuarioCreacion,
                                                   Vigente = proyecto.Vigente,
                                                   IdUnidadResponsable = proyecto.IdUnidadResponsable,
                                                   SiglaUnidadResponsable = proyecto.SiglaUnidadResponsable,
                                                   DescripcionEstado = proyecto.EstadosProyecto.Descripcion,
                                                   Acronimo = proyecto.Acronimo,
                                                   IdUnidadSolicitante = proyecto.IdUnidadSolicitante,
                                                   SiglaUnidadSolicitante = proyecto.SiglaUnidadSolicitante,
                                                   Identificacion = proyecto.Identificacion,
                                                   Grado = personal.GradAlfabetico,
                                                   Nombres = personal.Nombres,
                                                   Apellidos = personal.Apellidos
                                               }).ToListAsync();
                }

                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = "Registros Obtenidos"
                };
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

        public async Task ObtenerProyectosVigentesPorIdProyectoAsync(string _idProyecto)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.SiproProyecto = await (from proyecto in db.SiproProyecto
                                                where proyecto.Vigente == EstadoRegistro.VIGENTE && proyecto.IdProyecto == _idProyecto
                                                select new SiproProyectoDto
                                                {
                                                    FechaCreacion = proyecto.FechaCreacion,
                                                    FechaInicio = proyecto.FechaInicio,
                                                    IdEstado = proyecto.IdEstado,
                                                    IdProyecto = proyecto.IdProyecto,
                                                    MaquinaCreacion = proyecto.MaquinaCreacion,
                                                    Nombre = proyecto.Nombre,
                                                    UsuarioCreacion = proyecto.UsuarioCreacion,
                                                    Vigente = proyecto.Vigente,
                                                    IdUnidadResponsable = proyecto.IdUnidadResponsable,
                                                    SiglaUnidadResponsable = proyecto.SiglaUnidadResponsable,
                                                    DescripcionEstado = proyecto.EstadosProyecto.Descripcion,
                                                    Acronimo = proyecto.Acronimo,
                                                    IdUnidadSolicitante = proyecto.IdUnidadSolicitante,
                                                    SiglaUnidadSolicitante = proyecto.SiglaUnidadSolicitante,
                                                    Identificacion = proyecto.Identificacion
                                                }).FirstOrDefaultAsync();
                }

                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = "Registros Obtenidos"
                };
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

        public async Task ObtenerProyectosUnidadAsync(string _sigla)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.LstProyectos = await (from proyecto in db.SiproProyecto
                                               join personal in db.VmRehuPersonal
                                               on proyecto.Identificacion equals personal.Identificacion
                                               where proyecto.SiglaUnidadResponsable == _sigla
                                               select new SiproProyectoDto
                                               {
                                                   FechaCreacion = proyecto.FechaCreacion,
                                                   FechaInicio = proyecto.FechaInicio,
                                                   IdEstado = proyecto.IdEstado,
                                                   IdProyecto = proyecto.IdProyecto,
                                                   MaquinaCreacion = proyecto.MaquinaCreacion,
                                                   Nombre = proyecto.Nombre,
                                                   UsuarioCreacion = proyecto.UsuarioCreacion,
                                                   Vigente = proyecto.Vigente,
                                                   IdUnidadResponsable = proyecto.IdUnidadResponsable,
                                                   SiglaUnidadResponsable = proyecto.SiglaUnidadResponsable,
                                                   DescripcionEstado = proyecto.EstadosProyecto.Descripcion,
                                                   IdUnidadSolicitante = proyecto.IdUnidadSolicitante,
                                                   SiglaUnidadSolicitante = proyecto.SiglaUnidadSolicitante,
                                                   Acronimo = proyecto.Acronimo,
                                                   Identificacion = proyecto.Identificacion,
                                                   Grado = personal.GradAlfabetico,
                                                   Nombres = personal.Nombres,
                                                   Apellidos = personal.Apellidos
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

        public async Task ConsultarProyectosResponsableAsync(long _identificacion)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstProyectos = await (from personal in db.VmRehuPersonal
                                               from proyecto in db.SiproProyecto
                                               from responsable in db.SiproResponsable
                                                   //join personal in db.VmRehuPersonal
                                                   //on proyecto.Identificacion equals _identificacion
                                                   //join responsable in db.SiproResponsable
                                                   //on proyecto.IdProyecto equals responsable.IdProyecto
                                               where responsable.Identificacion == _identificacion
                                               && proyecto.IdProyecto == responsable.IdProyecto
                                               && personal.Identificacion == responsable.Identificacion

                                               select new SiproProyectoDto
                                               {
                                                   FechaCreacion = proyecto.FechaCreacion,
                                                   FechaInicio = proyecto.FechaInicio,
                                                   IdEstado = proyecto.IdEstado,
                                                   IdProyecto = proyecto.IdProyecto,
                                                   MaquinaCreacion = proyecto.MaquinaCreacion,
                                                   Nombre = proyecto.Nombre,
                                                   UsuarioCreacion = proyecto.UsuarioCreacion,
                                                   Vigente = proyecto.Vigente,
                                                   IdUnidadResponsable = proyecto.IdUnidadResponsable,
                                                   SiglaUnidadResponsable = proyecto.SiglaUnidadResponsable,
                                                   DescripcionEstado = proyecto.EstadosProyecto.Descripcion,
                                                   IdUnidadSolicitante = proyecto.IdUnidadSolicitante,
                                                   SiglaUnidadSolicitante = proyecto.SiglaUnidadSolicitante,
                                                   Acronimo = proyecto.Acronimo,
                                                   Identificacion = proyecto.Identificacion,
                                                   Grado = personal.GradAlfabetico,
                                                   Nombres = personal.Nombres,
                                                   Apellidos = personal.Apellidos
                                               }).Distinct().ToListAsync();

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

        public async Task AgregarProyectoAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproProyecto
                    {
                        IdProyecto = this.siproProyecto.IdProyecto,
                        Vigente = EstadoRegistro.VIGENTE,
                        UsuarioCreacion = this.siproProyecto.UsuarioCreacion.ToUpper(),
                        MaquinaCreacion = this.siproProyecto.MaquinaCreacion,
                        Nombre = this.siproProyecto.Nombre.ToUpper(),
                        IdEstado = EstadoProyecto.INICIADO,
                        FechaInicio = DateTime.Now,
                        FechaCreacion = DateTime.Now,
                        IdUnidadResponsable = this.siproProyecto.IdUnidadResponsable,
                        IdUnidadSolicitante = this.siproProyecto.IdUnidadSolicitante,
                        SiglaUnidadResponsable = this.siproProyecto.SiglaUnidadResponsable,
                        Acronimo = this.siproProyecto.Acronimo.ToUpper(),
                        SiglaUnidadSolicitante = this.siproProyecto.SiglaUnidadSolicitante,
                        Identificacion = this.siproProyecto.Identificacion
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

        public async Task ActualizarEstadoProyectoAenProceso(string _idProyecto)
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    SiproProyecto proyectoObtenido = await (from proyecto in db.SiproProyecto
                                                            where proyecto.IdProyecto == _idProyecto
                                                            select proyecto).FirstOrDefaultAsync();

                    proyectoObtenido.IdEstado = EstadoProyecto.ENPROCESO;

                    db.Entry(proyectoObtenido).State = EntityState.Modified;

                    if (await db.SaveChangesAsync() != 0)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El Registro fue actualizado Correctamente."
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

        public async Task<bool> VerificarExistenciaProyecto(string _Acronimo)
        {

            try
            {
                string acronimoSistema = _Acronimo.Trim();

                using (ContextoSipro db = new ContextoSipro())
                {
                    bool existe = await db.SiproProyecto.AnyAsync(x => x.Acronimo == acronimoSistema);

                    if (existe)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El sistema ya fue registrado anteriormente !!!."
                        };

                    return existe;
                }
            }
            catch
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 0,
                    Estado = false,
                    Mensaje = "Error, No se pudo validar la existencia del sistema !!!."
                };
                return false;
            }
        }


        #endregion
    }
}
