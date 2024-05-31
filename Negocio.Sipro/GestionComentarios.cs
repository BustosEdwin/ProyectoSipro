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

    public class GestionComentarios
    {
        #region Atributos
        private SiproComentarioDto comentario;
        private SiproComentarioDto actividadComentario;
        private EstadoRespuesta estadoRespuesta;
        private List<SiproComentarioDto> lstProyectos;
        #endregion

        #region Propiedades
        public EstadoRespuesta EstadoRespuesta
        {
            get { return this.estadoRespuesta; }
            set { this.estadoRespuesta = value; }
        }

        public SiproComentarioDto Comentario
        {
            get
            {
                return this.comentario;
            }
            set
            {
                this.comentario = value;
            }
        }

        public SiproComentarioDto ActividadComentario
        {
            get
            {
                return this.actividadComentario;
            }
            set
            {
                this.actividadComentario = value;
            }
        }

        public List<SiproComentarioDto> LstProyectos
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
        #endregion

        #region Metodos externos
        public async Task AgregarComentario()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproComentario
                    {
                        DescripcionComentario = this.comentario.DescripcionComentario.ToUpper(),
                        FechaCreacion = DateTime.Now,
                        IdCometario = this.comentario.IdCometario,
                        IdEvidencia = this.comentario.IdEvidencia,
                        MaquinaCreacion = this.comentario.MaquinaCreacion,
                        UsuarioCreacion = this.comentario.UsuarioCreacion.ToUpper(),
                        Consecutivo = this.comentario.Consecutivo,
                        UndeConsecutivo = this.comentario.UndeConsecutivo,
                        UndeFuerza = this.comentario.UndeFuerza,
                        Identificacion = this.comentario.Identificacion,
                        IdTipoResponsabilidad = this.comentario.IdTipoResponsabilidad,
                        Vigente = EstadoRegistro.VIGENTE,
                        UsuarioComentario = this.comentario.UsuarioComentario,
                        IdEstado = this.comentario.IdEstado
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
                    Mensaje = $"Ocurrio una excepción: {ex.Message} - {ex.InnerException}"
                };
            }
        }

        public async Task EnviarActividadComentarioAsync()
        {
            try
            {
                //var trazabilidad = 0;
                //using (ContextoSipro db = new ContextoSipro())
                //{
                //if (db.SiproComentario.Any(x => x.IdEvidencia == this.actividadComentario.IdEvidencia && x.Trazabilidad == this.actividadComentario.Estados))
                //{
                //    this.estadoRespuesta = new EstadoRespuesta
                //    {
                //        Codigo = 0,
                //        Estado = false,
                //        Mensaje = "El registro ya fue agregado"
                //    };
                //}
                //else
                //{
                //if (db.SiproComentario.Any(x => x.IdEvidencia == this.actividadComentario.IdEvidencia))
                //{
                //    SiproComentario siproComentario = await db.SiproComentario.Where(x => x.IdEvidencia == this.actividadComentario.IdEvidencia).OrderByDescending(t => t.FechaCreacion).FirstOrDefaultAsync();
                //    siproComentario.Trazabilidad = 0;
                //    db.Entry(siproComentario).State = EntityState.Modified;
                //}

                //trazabilidad = (int)this.actividadComentario.Estados;

                //db.Entry(new SiproComentario()
                //{
                //    IdCometario = this.actividadComentario.IdCometario,
                //    DescripcionComentario = this.actividadComentario.DescripcionComentario,
                //    UsuarioComentario = this.actividadComentario.UsuarioComentario,
                //    Consecutivo = Convert.ToInt32(this.actividadComentario.Consecutivo),
                //    UndeConsecutivo = Convert.ToInt32(this.actividadComentario.UndeConsecutivo),
                //    UndeFuerza = Convert.ToInt32(this.actividadComentario.UndeFuerza),
                //    FechaCreacion = DateTime.Now,
                //    UsuarioCreacion = this.actividadComentario.UsuarioCreacion,
                //    MaquinaCreacion = this.actividadComentario.MaquinaCreacion,
                //    Vigente = 1,
                //    IdEvidencia = this.actividadComentario.IdEvidencia,
                //    IdFuncionarioEnvia = this.actividadComentario.IdFuncionarioEnvia,
                //    Estados = Convert.ToInt32(this.actividadComentario.Estados),
                //    Trazabilidad = trazabilidad
                //}).State = EntityState.Added;


                //    if (await db.SaveChangesAsync() != 0)
                //        this.estadoRespuesta = new EstadoRespuesta
                //        {
                //            Codigo = 1,
                //            Estado = true,
                //            Mensaje = "El Registro Agregado Correctamente."
                //        };
                //    else
                //        this.estadoRespuesta = new EstadoRespuesta
                //        {
                //            Codigo = 0,
                //            Estado = false,
                //            Mensaje = "El registro no fue agregado."
                //        };
                //}
                //}
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

        public async Task ConsultarComentario(string _idEvidencia)
        {
            try
            {

                using (ContextoSipro db = new ContextoSipro())
                {
                    //this.lstProyectos = await db.SiproComentario.Where(x => x.IdEvidencia == _idEvidencia).OrderByDescending(x => x.FechaCreacion).Select(a => new SiproComentarioDto
                    //{
                    //    DescripcionComentario = a.DescripcionComentario,
                    //    Estados2 = db.SiproCtrlDominios.Where(x => x.IdDominio == a.Estados).Select(y => y.Descripcion).FirstOrDefault(),
                    //    FechaCreacion = a.FechaCreacion,
                    //    IdFuncionarioEnvia = a.IdFuncionarioEnvia,
                    //    Grado = db.SiproResponsable.Where(x => x.IdResponsable == a.IdFuncionarioEnvia).Select(y => y.Grado).FirstOrDefault(),
                    //    Nombres = db.SiproResponsable.Where(x => x.IdResponsable == a.IdFuncionarioEnvia).Select(y => y.Nombres).FirstOrDefault(),
                    //    Apellidos = db.SiproResponsable.Where(x => x.IdResponsable == a.IdFuncionarioEnvia).Select(y => y.Apellidos).FirstOrDefault(),
                    //    idUnidadEnvia = (decimal)db.SiproResponsable.Where(x => x.IdResponsable == a.IdFuncionarioEnvia).Select(y => y.IdUnidad).FirstOrDefault(),
                    //    Unidad = db.PortalUnidadesSiglas.Where(x => x.Consecutivo == db.SiproResponsable.Where(u => u.IdResponsable == a.IdFuncionarioEnvia).Select(i => i.IdUnidad).FirstOrDefault()).Select(y => y.Sigla).FirstOrDefault()
                    //}).ToListAsync();
                    //this.lstProyectos = await (from Comentario in db.SiproComentario
                    //                               //from funcionario in db.SiproResponsable
                    //                               //where comentario.IdFuncionarioEnvia == funcionario.IdResponsable
                    //                           where comentario.IdEvidencia == _idEvidencia
                    //                           select new SiproComentarioDto
                    //                           {
                    //                               DescripcionComentario = comentario.DescripcionComentario,
                    //                               Estados = comentario.Estados,
                    //                               FechaCreacion = comentario.FechaCreacion

                    //                           }).ToListAsync();
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

        public async Task ObtenerComentarioAsync(string _idComentario)
        {
            using (ContextoSipro db = new ContextoSipro())
            {


                this.comentario = await (from comen in db.SiproComentario
                                         where comen.IdCometario == _idComentario
                                         select new SiproComentarioDto
                                         {
                                             IdCometario = comen.IdCometario,
                                             Consecutivo = comen.Consecutivo,
                                             UndeConsecutivo = comen.UndeConsecutivo,
                                             DescripcionComentario = comen.DescripcionComentario,
                                             FechaCreacion = comen.FechaCreacion,
                                             Identificacion = comen.Identificacion,
                                             IdEstado = comen.IdEstado,
                                             IdEvidencia = comen.IdEvidencia,
                                             IdTipoResponsabilidad = comen.IdTipoResponsabilidad,
                                             MaquinaCreacion = comen.MaquinaCreacion,
                                             UndeFuerza = comen.UndeFuerza,
                                             UsuarioComentario = comen.UsuarioComentario,
                                             UsuarioCreacion = comen.UsuarioCreacion,
                                             Vigente = comen.Vigente,

                                         }).FirstOrDefaultAsync();
            }
        }
        public async Task ObtenerComentarioIdEvidenciaAsync(string _idEvidencia)
        {
            using (ContextoSipro db = new ContextoSipro())
            {


                this.comentario = await (from comen in db.SiproComentario
                                         where comen.IdEvidencia == _idEvidencia
                                         select new SiproComentarioDto
                                         {
                                             IdCometario = comen.IdCometario,
                                             Consecutivo = comen.Consecutivo,
                                             UndeConsecutivo = comen.UndeConsecutivo,
                                             DescripcionComentario = comen.DescripcionComentario,
                                             FechaCreacion = comen.FechaCreacion,
                                             Identificacion = comen.Identificacion,
                                             IdEstado = comen.IdEstado,
                                             IdEvidencia = comen.IdEvidencia,
                                             IdTipoResponsabilidad = comen.IdTipoResponsabilidad,
                                             MaquinaCreacion = comen.MaquinaCreacion,
                                             UndeFuerza = comen.UndeFuerza,
                                             UsuarioComentario = comen.UsuarioComentario,
                                             UsuarioCreacion = comen.UsuarioCreacion,
                                             Vigente = comen.Vigente,

                                         }).FirstOrDefaultAsync();
            }
        }


        public async Task AprobarComentarioAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproComentario
                    {
                        Consecutivo = this.comentario.Consecutivo,
                        DescripcionComentario = this.comentario.DescripcionComentario,
                        FechaCreacion = this.comentario.FechaCreacion,
                        IdCometario= this.comentario.IdCometario,
                        Identificacion= this.comentario.Identificacion,
                        IdEvidencia = this.comentario.IdEvidencia,
                        IdTipoResponsabilidad = this.comentario.IdTipoResponsabilidad,
                        MaquinaCreacion= this.comentario.MaquinaCreacion,
                        UndeConsecutivo = this.comentario.UndeConsecutivo,
                        UndeFuerza = this.comentario.UndeFuerza,
                        UsuarioComentario = this.comentario.UsuarioComentario,
                        UsuarioCreacion = this.comentario.UsuarioCreacion.ToUpper(),
                        Vigente = this.comentario.Vigente,
                        IdEstado = this.comentario.IdEstado
                    }).State = EntityState.Modified;

                    if (await db.SaveChangesAsync() != 0)
                    {
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "La evidencia fue aprobada"
                        };
                    }
                    else
                    {
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "Ocurrio un incoveniente, consultar con el administrador."
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = 1,
                    Estado = true,
                    Mensaje = $"Ocurrio una excepción:{ex.Message} - {ex.InnerException}"
                };
            }
        }
        #endregion
    }
}
