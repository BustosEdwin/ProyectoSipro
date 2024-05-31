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

    public class GestionTrazaResponsable
    {
        #region atributos
        private SiproTrazaResponsableDto trazaComentario;
        private EstadoRespuesta estadoRespuesta;
        private List<SiproTrazaResponsableDto> lstProyectos;
        #endregion

        #region Propiedades
        public SiproTrazaResponsableDto TrazaComentario
        {
            get
            {
                return this.trazaComentario;
            }
            set
            {
                this.trazaComentario = value;
            }
        }

        public EstadoRespuesta EstadoRepuesta
        {
            get
            {
                return this.estadoRespuesta;
            }
        }

        public List<SiproTrazaResponsableDto> LstProyectos
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
        public async Task AgregarTrazaResponsableAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproTrazaResponsable
                    {
                        IdComentario = this.trazaComentario.IdComentario,
                        IdEstadoComentario = this.trazaComentario.IdEstadoComentario,
                        IdResponsable = this.trazaComentario.IdResponsable,
                        IdTrazaResponsble = this.trazaComentario.IdTrazaResponsble
                    }).State = EntityState.Added;

                    if (await db.SaveChangesAsync() != 0)
                    {
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El registro fue agregado correctamente"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                this.estadoRespuesta = new EstadoRespuesta
                {
                    Codigo = -1,
                    Estado = false,
                    Mensaje = $"Ocurrio una excepción {ex.Message} - {ex.InnerException}"
                };
            }
        }
        
        #endregion
    }
}
