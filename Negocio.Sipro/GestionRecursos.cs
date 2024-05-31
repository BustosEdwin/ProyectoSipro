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

    public class GestionRecursos
    {
        #region Atributos
        private List<SiproRecursoDto> lstSiproRecursos;
        private EstadoRespuesta estadoRespuesta;
        private SiproRecursoDto siproRecurso;
        #endregion

        #region Propiedades
        public List<SiproRecursoDto> LstSiproRecursos
        {
            get
            {
                return this.lstSiproRecursos;
            }
            set
            {
                this.lstSiproRecursos = value;
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

        public SiproRecursoDto SiproRecurso
        {
            get
            {
                return this.siproRecurso;
            }
            set
            {
                this.siproRecurso = value;
            }
        }
        #endregion

        #region Metodos Externos

        public async Task ObtenerRecursosVigentesProyectoAsync(string _idProyecto)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstSiproRecursos = await (from recurso in db.SiproRecurso
                                                 where recurso.Vigente == EstadoRegistro.VIGENTE
                                                 && recurso.IdProyecto == _idProyecto
                                                 select new SiproRecursoDto
                                                 {
                                                     Adicionales = recurso.Adicionales,
                                                     BaseDatos = recurso.BaseDatos,
                                                     DireccionIp = recurso.DireccionIp,
                                                     FechaCreacion = recurso.FechaCreacion,
                                                     IdProyecto = recurso.IdProyecto,
                                                     IdRecurso = recurso.IdRecurso,
                                                     IdTipoRecurso = recurso.IdTipoRecurso,
                                                     TipoRecurso = recurso.TipoRecursoRecurso.Descripcion,
                                                     MaquinaCreacion = recurso.MaquinaCreacion,
                                                     Nombre = recurso.Nombre,
                                                     UsuarioCreacion = recurso.UsuarioCreacion,
                                                     Vigente = recurso.Vigente
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

        public async Task AgregarRecursoAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproRecurso
                    {
                        Adicionales = this.siproRecurso.Adicionales.ToUpper(),
                        BaseDatos = this.siproRecurso.BaseDatos.ToUpper(),
                        DireccionIp = this.siproRecurso.DireccionIp,
                        FechaCreacion = DateTime.Now,
                        IdProyecto = this.siproRecurso.IdProyecto,
                        IdRecurso = this.siproRecurso.IdRecurso,
                        IdTipoRecurso = this.siproRecurso.IdTipoRecurso,
                        MaquinaCreacion = this.siproRecurso.MaquinaCreacion,
                        Nombre = this.siproRecurso.Nombre.ToUpper(),
                        UsuarioCreacion = this.siproRecurso.UsuarioCreacion.ToUpper(),
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

        #endregion
    }
}
