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

    public class GestionEstados
    {
        #region Atributos
        private List<SiproEstadosDto> lstSiproEstados;
        private EstadoRespuesta estadoRespuesta;
        private SiproEstadosDto siproEstados;
        #endregion

        #region Propiedades
        public List<SiproEstadosDto> LstSiproEstados
        {
            get
            {
                return this.lstSiproEstados;
            }
            set
            {
                this.lstSiproEstados = value;
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

        public SiproEstadosDto SiproEstados
        {
            get
            {
                return this.siproEstados;
            }
            set {
                this.siproEstados = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerEstadosVigentesAsync()
        {
            try
            {

                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstSiproEstados = await (from fase in db.SiproEstados
                                                  where fase.Vigente == EstadoRegistro.VIGENTE
                                                  select new SiproEstadosDto
                                                  {
                                                      Descripcion = fase.Descripcion,
                                                      FechaCreacion = fase.FechaCreacion,
                                                      IdEstado = fase.IdEstado,
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

        public async Task ObtenerEstadoVigenteAsync(string _idEstado)
        {
            try
            {

                using (ContextoSipro db = new ContextoSipro())
                {
                    this.siproEstados = await (from fase in db.SiproEstados
                                   where fase.Vigente == EstadoRegistro.VIGENTE
                                   && fase.IdEstado == _idEstado
                                   select new SiproEstadosDto
                                   {
                                       Descripcion = fase.Descripcion,
                                       FechaCreacion = fase.FechaCreacion,
                                       IdEstado = fase.IdEstado,
                                       MaquinaCreacion = fase.MaquinaCreacion,
                                       UsuarioCreacion = fase.UsuarioCreacion,
                                       Vigente = fase.Vigente
                                   }).FirstOrDefaultAsync();


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
        #endregion
    }
}
