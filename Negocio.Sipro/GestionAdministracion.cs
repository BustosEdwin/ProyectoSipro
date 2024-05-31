using Comun.Sipro.Dto;
using Comun.Sipro.Utilidades;
using Datos.Sipro;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio.Sipro
{


  public class GestionAdministracion
    {

        #region Atributos
        private List<SiproRolDto> lstRolesList;
        private SiproRolDto lstRoles;

        private SiproUsuarioRolDto lstusuario;
        private VmRehuPersonalDto lstusuarioRol;
        private EstadoRespuesta estadoRespuesta;



        #endregion

        #region Propiedades

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
        public List<SiproRolDto> LstRolesList
        {
            get
            {
                return this.lstRolesList;
            }
            set
            {
                this.lstRolesList = value;
            }
        }

        public SiproUsuarioRolDto Lstusuario
        {
            get
            {
                return this.lstusuario;
            }
            set
            {
                this.lstusuario = value;
            }
        }

        public SiproRolDto LstRoles
        {
            get
            {
                return this.lstRoles;
            }
            set
            {
                this.lstRoles = value;
            }
        }

        public VmRehuPersonalDto LstusuarioRol
        {
            get
            {
                return this.lstusuarioRol;
            }
            set
            {
                this.lstusuarioRol = value;
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método para agregar rol a usuario
        /// </summary>
        /// <returns></returns>
        public async Task AgregarRolAsync()
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    SiproRolDto siproRolDto = new SiproRolDto();

                    if (db.SiproRoles.Any(x => x.DescripcionRol == this.lstRoles.DescripcionRol))
                    {
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "Señor Funcionario El Rol ya Existe"
                        };
                    }
                    else
                    {

                        db.Entry(new SiproRoles
                        {
                            IdRol = this.lstRoles.IdRol,
                            DescripcionRol = this.lstRoles.DescripcionRol.ToUpper(),
                            Vigente = EstadoRegistro.VIGENTE,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = this.lstRoles.UsuarioCreacion.ToUpper(),
                            MaquinaCreacion = this.LstRoles.MaquinaCreacion,
                        }).State = EntityState.Added;

                        if (await db.SaveChangesAsync() != 0)
                            this.estadoRespuesta = new EstadoRespuesta
                            {
                                Codigo = 1,
                                Estado = true,
                                Mensaje = "El Registro es Agregado Correctamente."
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

        /// <summary>
        /// Método para consultar funcionario por identificación
        /// </summary>
        /// <param name="_Identificacion"></param>
        /// <returns></returns>
        public async Task ConsultaFuncionarioAsync(int _Identificacion)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    SiproRolDto siproRolDto = new SiproRolDto();

                    this.lstusuarioRol = await (from funcion in db.VmRehuPersonal
                                                where funcion.Identificacion == _Identificacion
                                                select new VmRehuPersonalDto
                                                {
                                                    NombreGrado = funcion.NombreGrado,
                                                    Nombres = funcion.Nombres,
                                                    Apellidos = funcion.Apellidos,
                                                    CargoActual = funcion.CargoActual,
                                                    Fisica = funcion.Fisica,
                                                    Consecutivo = funcion.Consecutivo,
                                                    UndeConsecutivo = funcion.UndeConsecutivo,
                                                    UndeFuerza = funcion.UndeFuerza,
                                                    UsuarioEmpresarial = funcion.UsuarioEmpresarial
                                                }).FirstOrDefaultAsync();


                    if (this.lstusuarioRol != null)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El registro fue encontrado"

                        };
                    else
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 0,
                            Estado = false,
                            Mensaje = "El funcionario no fue encontrado."
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

        /// <summary>
        /// Método para obtener lista de dominio por id dominio
        /// </summary>
        /// <param name="_id"></param>
        /// <returns></returns>
        public List<SiproCtrlDominioDTO> ControlDominios(int _id)
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

        /// <summary>
        /// Método para consulta de roles vigentes
        /// </summary>
        /// <returns></returns>
        public List<SiproRolDto> ConsultaRol()
        {
            List<SiproRolDto> ResultadoProcedimiento = new List<SiproRolDto>();

            using (ContextoSipro db = new ContextoSipro())
            {
                ResultadoProcedimiento = db.SiproRoles.Where(x => x.Vigente == 1).Select(x => new SiproRolDto
                {
                    IdRol = x.IdRol,
                    DescripcionRol = x.DescripcionRol
                }).ToList();
            }
            return ResultadoProcedimiento;
        }

        /// <summary>
        /// Método para agregar o guardad usuario
        /// </summary>
        /// <returns></returns>
        public async Task AgregarUsuarioAsync()
        {
            var vigentes = 0;
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    SiproUsuarioRolDto siproRolDto = new SiproUsuarioRolDto();

                    if (this.lstusuario.Vigente == 6)
                    {
                        vigentes = 1;
                    }
                    if (this.lstusuario.Vigente == 7)
                    {
                        vigentes = 0;
                    }



                    if (db.SiproUsuarios.Any(x => x.UsuarioEmpresarial == this.lstusuario.usuario && x.Vigente == 1))
                    {

                        var id_usuario = (from m in db.SiproUsuarios
                                          where m.UsuarioEmpresarial == this.lstusuario.usuario
                                          && m.Vigente == 1
                                          select m.IdUsuario).FirstOrDefault();

                        if (db.SiproUsuarioRol.Any(x => x.IdUsuario == id_usuario && x.IdRol == this.lstusuario.IdRol))

                            this.estadoRespuesta = new EstadoRespuesta
                            {
                                Codigo = 0,
                                Estado = false,
                                Mensaje = "Señor Funcionario El Usuario ya Existe y esta vigente"
                            };

                        else
                        {
                            db.Entry(new SiproUsuarioRol
                            {
                                IdUsuarioRol = Guid.NewGuid().ToString(),
                                IdUsuario = id_usuario,
                                IdRol = this.lstusuario.IdRol,
                                Vigente = vigentes,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = this.lstusuario.UsuarioCreacion,
                                MaquinaCreacion = this.lstusuario.MaquinaCreacion,
                                FechaInicio = this.lstusuario.FechaInicio,
                                FechaFin = this.lstusuario.FechaFin,
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

                    else
                    {

                        db.Entry(new SiproUsuarios
                        {
                            IdUsuario = this.lstusuario.IdUsuario,
                            UsuarioEmpresarial = this.lstusuario.usuario,
                            Consecutivo = this.lstusuario.Consecutivo,
                            UndeConsecutivo = this.lstusuario.UndeConsecutivo,
                            UndeFuerza = this.lstusuario.UndeFuerza,
                            FechaCreacion = DateTime.Now,
                            UsuarioCreacion = this.lstusuario.UsuarioCreacion.ToUpper(),
                            MaquinaCreacion = this.lstusuario.MaquinaCreacion,
                            Vigente = vigentes,
                        }).State = EntityState.Added;

                        if (await db.SaveChangesAsync() != 0)
                        {
                            var id_usuario = (from m in db.SiproUsuarios
                                              where m.UsuarioEmpresarial == this.lstusuario.usuario
                                              && m.Vigente == 1
                                              select m.IdUsuario).FirstOrDefault();

                            db.Entry(new SiproUsuarioRol
                            {
                                IdUsuarioRol = Guid.NewGuid().ToString(),
                                IdUsuario = id_usuario,
                                IdRol = this.lstusuario.IdRol,
                                Vigente = vigentes,
                                FechaCreacion = DateTime.Now,
                                UsuarioCreacion = this.lstusuario.UsuarioCreacion,
                                MaquinaCreacion = this.lstusuario.MaquinaCreacion,
                                FechaInicio = this.lstusuario.FechaInicio,
                                FechaFin = this.lstusuario.FechaFin,
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
                        else
                        {

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
                    Mensaje = $"Ocurrio Una excepción: {ex.Message}"
                };
            }
        }
        #endregion


    }
}
