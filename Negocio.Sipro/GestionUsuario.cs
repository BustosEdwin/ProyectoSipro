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

    public class GestionUsuario
    {
        #region Atributos
        private SiproUsuariosDto usuario;
        private EstadoRespuesta estadoRespuesta;
        #endregion


        #region Propiedades
        public SiproUsuariosDto Usuario
        {
            get
            {
                return this.usuario;
            }
            set
            {
                this.usuario = value;
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

        public async Task AgregarUsuarioAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    db.Entry(new SiproUsuarios
                    {
                        Vigente = EstadoRegistro.VIGENTE,
                        Consecutivo = this.usuario.Consecutivo,
                        FechaCreacion = DateTime.Now,
                        IdUsuario = this.usuario.IdUsuario,
                        MaquinaCreacion = this.usuario.MaquinaCreacion,
                        UsuarioCreacion = this.usuario.UsuarioCreacion.ToUpper(),
                        UndeConsecutivo = this.usuario.UndeConsecutivo,
                        UndeFuerza = this.usuario.UndeFuerza,
                        UsuarioEmpresarial = this.usuario.UsuarioEmpresarial
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

        public async Task ExisteFuncionario(decimal _consecutivo, decimal _undeConsecutivo)
        {
            await Task.Factory.StartNew(() => {
                using (ContextoSipro db = new ContextoSipro())
                {
                    if (db.SiproUsuarios.Any(x => x.Consecutivo == _consecutivo && x.UndeConsecutivo == _undeConsecutivo))
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El usuario existe."
                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El usuario no existe."
                        };
                }
            });            
        }

        #endregion
    }
}
