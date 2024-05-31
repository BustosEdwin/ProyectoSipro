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
    public class GestionFases
    {
        #region Atributos
        private List<SiproFasesDto> lstSiproFases;
        private EstadoRespuesta estadoRespuesta;

        #endregion

        #region Propiedades
        public List<SiproFasesDto> LstSiproFases
        {
            get
            {
                return this.lstSiproFases;
            }
            set
            {
                this.lstSiproFases = value;
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
        public async Task ObtenerFasesVigentesAsync()
        {

            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                   var resultado =  await (from fase in db.SiproFases
                                                where fase.Vigente == EstadoRegistro.VIGENTE
                                                select new SiproFasesDto
                                                {
                                                    Descripcion = fase.Descripcion,
                                                    FechaCreacion = fase.FechaCreacion,
                                                    IdFase = fase.IdFase,
                                                    MaquinaCreacion = fase.MaquinaCreacion,
                                                    UsuarioCreacion = fase.UsuarioCreacion,
                                                    Vigente = fase.Vigente
                                                }).ToListAsync();

                    this.lstSiproFases = resultado.OrderBy(x => x.Descripcion).ToList();

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
