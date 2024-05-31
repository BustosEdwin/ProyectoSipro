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
    public class GestionActividades
    {
        #region Atributos
        private List<SiproBitacoraDto> lstActividades;
        private EstadoRespuesta estadoRespuesta;
        private SiproBitacoraDto actividad;
        #endregion

        #region Propiedades
        public List<SiproBitacoraDto> LstActividades
        {
            get
            {
                return this.lstActividades;
            }
            set
            {
                this.lstActividades = value;
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

        public SiproBitacoraDto Actividad
        {
            get
            {
                return this.actividad;
            }
            set
            {
                this.actividad = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerActividadesVigentesProyectoAsync(string _idProyecto)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstActividades = await (from bitacora in db.SiproBitacora
                                                 where bitacora.Vigente == EstadoRegistro.VIGENTE
                                                 && bitacora.IdProyecto == _idProyecto
                                                 orderby bitacora.FechaCreacion ascending
                                                 select new SiproBitacoraDto
                                                 {
                                                     Descripcion = bitacora.Descripcion,
                                                     FechaCreacion = bitacora.FechaCreacion,
                                                     FechaFin = bitacora.FechaFin,
                                                     FechaInicio = bitacora.FechaInicio,
                                                     IdBitacora = bitacora.IdBitacora,
                                                     IdFase = bitacora.IdFase,
                                                     IdObservacion = bitacora.IdObservacion,
                                                     IdProyecto = bitacora.IdProyecto,
                                                     MaquinaCreacion = bitacora.MaquinaCreacion,
                                                     UsuarioCreacion = bitacora.UsuarioCreacion,
                                                     Vigente = bitacora.Vigente,
                                                     Observacion = bitacora.ObservacionBitacotra.Descripcion,
                                                     Fase = bitacora.FaseBitacora.Descripcion
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

        public async Task AgregarActividadAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {

                    //if (db.SiproBitacora.Any(x => x.IdFase == this.actividad.IdFase && x.IdProyecto == this.actividad.IdProyecto))
                    //{
                    //    this.estadoRespuesta = new EstadoRespuesta
                    //    {
                    //        Codigo = 0,
                    //        Estado = false,
                    //        Mensaje = "Señor Funcionario, La fase ya fue agregada"
                    //    };
                    //}
                    //else{



                    db.Entry(new SiproBitacora()
                    {
                        Descripcion = this.actividad.Descripcion.ToUpper(),
                        FechaCreacion = DateTime.Now,
                        FechaFin = this.actividad.FechaFin,
                        FechaInicio = this.actividad.FechaInicio,
                        IdBitacora = this.actividad.IdBitacora,
                        IdFase = this.actividad.IdFase,
                        IdObservacion = this.actividad.IdObservacion,
                        IdProyecto = this.actividad.IdProyecto,
                        MaquinaCreacion = this.actividad.MaquinaCreacion,
                        UsuarioCreacion = this.actividad.UsuarioCreacion.ToUpper(),
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
        #endregion
    }
}
