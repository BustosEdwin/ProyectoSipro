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
    public class GestionEvidencias
    {
        #region Atributos
        private List<SiproEvidenciaDto> lstEvidencias;
        private EstadoRespuesta estadoRespuesta;
        private SiproEvidenciaDto evidencia;
        #endregion

        #region Propiedades
        public List<SiproEvidenciaDto> LstEvidencias
        {
            get
            {
                return this.lstEvidencias;
            }
            set
            {
                this.lstEvidencias = value;
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

        public SiproEvidenciaDto Evidencia
        {
            get
            {
                return this.evidencia;
            }
            set
            {
                this.evidencia = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerEvidenciasActividadesAsync(string _idActividad)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstEvidencias = await (from sEvidencia in db.SiproEvidencia
                                                where sEvidencia.Vigente == EstadoRegistro.VIGENTE
                                                && sEvidencia.IdBitacora == _idActividad
                                                && sEvidencia.Vigente == 1 
                                                orderby sEvidencia.FechaCreacion ascending
                                                select new SiproEvidenciaDto
                                                {
                                                    //FechaCreacion = sEvidencia.FechaCreacion,
                                                    IdBitacora = sEvidencia.IdBitacora,
                                                    IdEvidencia = sEvidencia.IdEvidencia,
                                                    IdObservaciones = sEvidencia.IdObservaciones,
                                                    IdResponsable = sEvidencia.IdResponsable,
                                                    MaquinaCreacion = sEvidencia.MaquinaCreacion,
                                                    UrlRuta = sEvidencia.UrlRuta,
                                                    UsuarioCreacion = sEvidencia.UsuarioCreacion,
                                                    FechaCreacion = sEvidencia.FechaCreacion
                                                    //Vigente = sEvidencia.Vigente,
                                                    //Estado = (from comentario in db.SiproComentario
                                                    //          from dominio in db.SiproCtrlDominios
                                                    //          where comentario.IdEvidencia == sEvidencia.IdEvidencia
                                                    //          && comentario.Estados == dominio.IdDominio
                                                    //          orderby comentario.FechaCreacion descending
                                                    //          select dominio.Descripcion

                                                    //          ).FirstOrDefault(),
                                                    //FechaCreacion = (from comentario in db.SiproComentario                                                             
                                                    //          where comentario.IdEvidencia == sEvidencia.IdEvidencia                                                           
                                                    //          orderby comentario.FechaCreacion descending
                                                    //          select comentario.FechaCreacion

                                                    //          ).FirstOrDefault()


                                                }).ToListAsync();


                    if (this.lstEvidencias == null)
                        this.lstEvidencias = new List<SiproEvidenciaDto>();

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

        public async Task ObtenerEvidenciaAsync(string _idEvidencia)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.evidencia = await (from evi in db.SiproEvidencia
                                            where evi.IdEvidencia == _idEvidencia
                                            && evi.Vigente == 1
                                            select new SiproEvidenciaDto
                                            {
                                                IdBitacora = evi.IdBitacora,
                                                IdEvidencia = evi.IdEvidencia,
                                                IdObservaciones = evi.IdObservaciones,
                                                IdResponsable = evi.IdResponsable,
                                                MaquinaCreacion = evi.MaquinaCreacion,
                                                UrlRuta = evi.UrlRuta,
                                                UsuarioCreacion = evi.UsuarioCreacion,
                                                FechaCreacion = evi.FechaCreacion
                                            }).FirstOrDefaultAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AgregarEvidenciaAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproEvidencia()
                    {
                        IdBitacora = this.evidencia.IdBitacora,
                        FechaCreacion = DateTime.Now,
                        IdEvidencia = this.evidencia.IdEvidencia,
                        IdObservaciones = this.evidencia.IdObservaciones,
                        IdResponsable = this.evidencia.IdResponsable,
                        MaquinaCreacion = this.evidencia.MaquinaCreacion,
                        UrlRuta = this.evidencia.UrlRuta,
                        UsuarioCreacion = this.evidencia.UsuarioCreacion,
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

        public async Task ObtenerEnvioDucumento(string _identificacion)
        {
            int identi = Convert.ToInt32(_identificacion);
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    var numero = db.SiproResponsable.Where(x => x.Identificacion == identi).Select(a => a.IdResponsable).FirstOrDefault();
                    //var numero = (from nu in db.SiproResponsable
                    //              where nu.Identificacion == Convert.ToInt32("1061702317") ViewBag.Estado = new SelectList(new GestionEvidencias().NombreProcedimiento2((int)1), "IdDominio", "Descripcion");
                    //              select new SiproResponsableDto {                                                         
                    //    IdResponsable = nu.IdResponsable
                    //}).FirstOrDefault();

                    this.lstEvidencias = await (from sEvidencia in db.SiproEvidencia
                                                from responsa in db.SiproComentario
                                                from bitacora in db.SiproBitacora
                                                from proyecto in db.SiproProyecto
                                                from responsable2 in db.SiproResponsable
                                                where responsa.IdEvidencia == sEvidencia.IdEvidencia
                                                && sEvidencia.IdBitacora == bitacora.IdBitacora
                                                && bitacora.IdProyecto == proyecto.IdProyecto
                                                && sEvidencia.Vigente == EstadoRegistro.VIGENTE
                                                //&& responsa.IdFuncionarioEnvia == responsable2.IdResponsable
                                                //&& responsable2.Identificacion == identi
                                                //&& responsa.Estados == 2
                                                //&& responsa.Trazabilidad == 2
                                                //&& responsa.IdFuncionarioEnvia == numero
                                                select new SiproEvidenciaDto
                                                {
                                                    FechaCreacion = responsa.FechaCreacion,
                                                    IdBitacora = sEvidencia.IdBitacora,
                                                    IdEvidencia = sEvidencia.IdEvidencia,
                                                    IdObservaciones = sEvidencia.IdObservaciones,
                                                    IdResponsable = sEvidencia.IdResponsable,
                                                    MaquinaCreacion = sEvidencia.MaquinaCreacion,
                                                    UrlRuta = sEvidencia.UrlRuta,
                                                    UsuarioCreacion = responsa.UsuarioCreacion,
                                                    Vigente = sEvidencia.Vigente,
                                                    Nombre_proyecto = proyecto.Nombre,
                                                    sigla_proyecto = proyecto.Acronimo,
                                                    fase_proyecto = (from fase in db.SiproFases
                                                                     where fase.IdFase == bitacora.IdFase
                                                                     select fase.Descripcion
                                                    ).FirstOrDefault(),
                                                    descripcion_documento = responsa.DescripcionComentario,
                                                    GradoEnvia = (from usuario in db.VmRehuPersonal
                                                                  where usuario.UsuarioEmpresarial == responsa.UsuarioCreacion
                                                                  select usuario.GradAlfabetico
                                                    ).FirstOrDefault(),
                                                    NombresEnvia = (from usuario in db.VmRehuPersonal
                                                                    where usuario.UsuarioEmpresarial == responsa.UsuarioCreacion
                                                                    select usuario.Nombres
                                                    ).FirstOrDefault(),
                                                    ApellidoEnvia = (from usuario in db.VmRehuPersonal
                                                                     where usuario.UsuarioEmpresarial == responsa.UsuarioCreacion
                                                                     select usuario.Apellidos
                                                    ).FirstOrDefault(),
                                                    IdResponsableEnvia = (from usuario in db.VmRehuPersonal
                                                                          from nombre in db.SiproResponsable
                                                                          where usuario.UsuarioEmpresarial == responsa.UsuarioCreacion
                                                                          && usuario.Identificacion == nombre.Identificacion
                                                                          select nombre.IdResponsable
                                                    ).FirstOrDefault()


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

        public async Task EliminarDocumento(string _IdEvidencia, string _usuario)
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {

                    SiproEvidencia siproEvidencia = await (from sEvidencia in db.SiproEvidencia
                                                           where sEvidencia.IdEvidencia == _IdEvidencia
                                                           && sEvidencia.UsuarioCreacion == _usuario
                                                           select sEvidencia).FirstOrDefaultAsync();

                    siproEvidencia.Vigente = EstadoRegistro.NOVIGENTE;

                    db.Entry(siproEvidencia).State = EntityState.Modified;


                    if (await db.SaveChangesAsync() != 0)

                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "Registros Eliminado"
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El registro no fue Eliminado."
                        };
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = "El documento debe ser eliminado por el funcionario quien lo creo"
                };
            }
        }


        public List<SiproCtrlDominioDTO> NombreProcedimiento2(int _id)
        {
            List<SiproCtrlDominioDTO> ResultadoProcedimiento = new List<SiproCtrlDominioDTO>();

            using (ContextoSipro db = new ContextoSipro())
            {
                ResultadoProcedimiento = db.SiproCtrlDominios.Where(x => x.PadreId == _id && x.Vigente == 1).Select(x => new SiproCtrlDominioDTO
                {
                    IdDominio = x.IdDominio,
                    Descripcion = x.Descripcion
                }).ToList();
            }
            return ResultadoProcedimiento;
        }


        #endregion
    }
}
