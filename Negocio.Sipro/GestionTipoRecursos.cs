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

    public class GestionTipoRecursos
    {
        #region Atributos
        private List<SiproTipoRecursoDto> lstSiproTipoRecursos;
        private EstadoRespuesta estadoRespuesta;

        #endregion

        #region Propiedades
        public List<SiproTipoRecursoDto> LstSiproTipoRecursos
        {
            get
            {
                return this.lstSiproTipoRecursos;
            }
            set
            {
                this.lstSiproTipoRecursos = value;
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
        #endregion

        #region Metodos Externos
        public async Task ObtenerTipoRecursosVigentesAsync()
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstSiproTipoRecursos = await (from tipoRecurso in db.SiproTipoRecurso
                                                where tipoRecurso.Vigente == EstadoRegistro.VIGENTE
                                                select new SiproTipoRecursoDto
                                                {
                                                    Descripcion = tipoRecurso.Descripcion,
                                                    FechaCreacion = tipoRecurso.FechaCreacion,
                                                    IdTipoRecurso = tipoRecurso.IdTipoRecurso,
                                                    MaquinaCreacion = tipoRecurso.MaquinaCreacion,
                                                    UsuarioCreacion = tipoRecurso.UsuarioCreacion,
                                                    Vigente = tipoRecurso.Vigente
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

        #endregion
    }
}
