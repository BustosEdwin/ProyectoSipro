
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

    public class GestionUsuarioRol
    {
        #region Atributos
        private SiproUsuarioRolDto usuarioRol;
        private EstadoRespuesta estadoRespuesta;
        #endregion

        #region Propiedades
        public SiproUsuarioRolDto UsuarioRol
        {
            get
            {
                return this.usuarioRol;
            }
            set
            {
                this.usuarioRol = value;
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

        #region Metodos externos
        public async Task AgregarRolBasicoAsync()
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                db.Entry(new SiproUsuarioRol
                {
                      FechaCreacion = DateTime.Now,
                      IdRol = this.usuarioRol.IdRol,
                      IdUsuario = this.usuarioRol.IdUsuario,
                      IdUsuarioRol = Guid.NewGuid().ToString(),
                      MaquinaCreacion = this.usuarioRol.MaquinaCreacion,
                      UsuarioCreacion = this.usuarioRol.UsuarioCreacion,
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
        #endregion
    }
}
