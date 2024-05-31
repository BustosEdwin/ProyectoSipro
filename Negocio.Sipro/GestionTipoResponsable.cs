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

    public class GestionTipoResponsable
    {
        #region Atributos
        private List<SiproTipoResponsabilidadDto> lstTipoResponsabilidades;
        private EstadoRespuesta estadoRespuesta;
        private SiproTipoResponsabilidadDto tipoResponsabilidad;
        #endregion

        #region Propiedades
        public List<SiproTipoResponsabilidadDto> LstTipoResponsabilidades
        {
            get
            {
                return this.lstTipoResponsabilidades;
            }
            set
            {
                this.lstTipoResponsabilidades = value;
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

        public SiproTipoResponsabilidadDto Funcionario
        {
            get
            {
                return this.tipoResponsabilidad;
            }
            set
            {
                this.tipoResponsabilidad = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerTpoResponsabilidadesAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.lstTipoResponsabilidades = await (from tResponsabilidad in db.SiproTipoResponsabilidad
                                                           where tResponsabilidad.Vigente == EstadoRegistro.VIGENTE
                                                           select new SiproTipoResponsabilidadDto
                                                           {
                                                               Descripcion = tResponsabilidad.Descripcion,
                                                               FechaCreacion = tResponsabilidad.FechaCreacion,
                                                               IdTipoResponsabilidad = tResponsabilidad.IdTipoResponsabilidad,
                                                               MaquinaCreacion = tResponsabilidad.MaquinaCreacion,
                                                               UsuarioCreacion = tResponsabilidad.UsuarioCreacion,
                                                               Vigente = tResponsabilidad.Vigente
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


        public async Task ObtenerTipoResponsabilidadResponsableProyecto(string _idResponsable)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                this.lstTipoResponsabilidades = await (from tipoResponsabilidad in db.SiproTipoResponsabilidad
                                                       from responsable in db.SiproResponsable
                                                       //from proyecto in db.SiproProyecto
                                                       where responsable.IdResponsable == _idResponsable
                                                       && tipoResponsabilidad.IdTipoResponsabilidad == responsable.IdTipoResponsable
                                                       && tipoResponsabilidad.Vigente == 1
                                                       select new SiproTipoResponsabilidadDto
                                                       {
                                                           Descripcion = tipoResponsabilidad.Descripcion,
                                                           FechaCreacion = tipoResponsabilidad.FechaCreacion,
                                                           IdTipoResponsabilidad = tipoResponsabilidad.IdTipoResponsabilidad,
                                                           MaquinaCreacion = tipoResponsabilidad.MaquinaCreacion,
                                                           UsuarioCreacion = tipoResponsabilidad.UsuarioCreacion,
                                                           Vigente = tipoResponsabilidad.Vigente
                                                       }).ToListAsync();
            }
        }
        #endregion
    }
}
