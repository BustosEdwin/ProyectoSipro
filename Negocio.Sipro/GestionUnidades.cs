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
    public class GestionUnidades
    {
        #region Atributos
        private List<PortalUnidadesSiglasDto> lstPortalUnidades;
        private EstadoRespuesta estadoRespuesta;
        private PortalUnidadesSiglasDto portalUnidad;
        #endregion

        #region Propiedades
        public List<PortalUnidadesSiglasDto> LstPortalUnidades
        {
            get
            {
                return this.lstPortalUnidades;
            }
            set
            {
                this.lstPortalUnidades = value;
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

        public PortalUnidadesSiglasDto PortalUnidad
        {
            get
            {
                return this.portalUnidad;
            }
            set
            {
                this.portalUnidad = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerUnidadesAsync()
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstPortalUnidades = await (from unidades in db.PortalUnidadesSiglas
                                                    select new PortalUnidadesSiglasDto
                                                    {
                                                        Consecutivo = unidades.Consecutivo,
                                                        Sigla = unidades.Sigla
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


        public async Task ObtenerUnidadSiglaAsync(decimal? _idSigla)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                try
                {
                    this.portalUnidad = await (from unidad in db.PortalUnidadesSiglas
                                               where unidad.Consecutivo == _idSigla
                                               select new PortalUnidadesSiglasDto
                                               {
                                                   Consecutivo = unidad.Consecutivo,
                                                   Sigla = unidad.Sigla
                                               }).FirstOrDefaultAsync();

                    if (this.portalUnidad != null)
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
        }

        public async Task ObtenerUnidadSiglaAsync(string _sigla)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                try
                {
                    this.portalUnidad = await (from unidad in db.PortalUnidadesSiglas
                                               where unidad.Sigla == _sigla
                                               select new PortalUnidadesSiglasDto
                                               {
                                                   Consecutivo = unidad.Consecutivo,
                                                   Sigla = unidad.Sigla
                                               }).FirstOrDefaultAsync();

                    if (this.portalUnidad != null)
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
        }


        #endregion
    }
}
