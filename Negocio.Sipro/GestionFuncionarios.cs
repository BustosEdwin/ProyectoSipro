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
    public class GestionFuncionarios
    {
        #region Atributos
        private List<VmRehuPersonalDto> lstFuncioanrios;
        private EstadoRespuesta estadoRespuesta;
        private VmRehuPersonalDto funcionario;

        #endregion

        #region Propiedades
        public List<VmRehuPersonalDto> LstFuncioanrios
        {
            get
            {
                return this.lstFuncioanrios;
            }
            set
            {
                this.lstFuncioanrios = value;
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

        public VmRehuPersonalDto Funcionario
        {
            get
            {
                return this.funcionario;
            }
            set
            {
                this.funcionario = value;
            }
        }
        #endregion

        #region Metodos Externos
        public async Task ObtenerFuncionarioAsync(long _identificacion)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.funcionario = await (from persona in db.VmRehuPersonal
                                              where persona.Identificacion == _identificacion
                                              select new VmRehuPersonalDto
                                              {
                                                  Apellidos = persona.Apellidos,
                                                  CargoActual = persona.CargoActual,
                                                  Consecutivo = persona.Consecutivo,
                                                  CorreoElectronico = persona.CorreoElectronico,
                                                  DescripcionDependencia = persona.DescripcionDependencia,
                                                  Fisica = persona.Fisica,
                                                  GradAlfabetico = persona.GradAlfabetico,
                                                  Identificacion = persona.Identificacion,
                                                  NombreGrado = persona.NombreGrado,
                                                  Nombres = persona.Nombres,
                                                  NumeroCelular = persona.NumeroCelular,
                                                  Sexo = persona.Sexo,
                                                  SiglaPapa = persona.SiglaPapa,
                                                  UndeConsecutivo = persona.UndeConsecutivo,
                                                  UndeConsecutivoLaborando = persona.UndeConsecutivoLaborando,
                                                  UndeFuerza = persona.UndeFuerza,
                                                  UsuarioEmpresarial = persona.UsuarioEmpresarial
                                              }).FirstOrDefaultAsync();


                    if (this.funcionario != null)
                        this.estadoRespuesta = new EstadoRespuesta
                        {
                            Codigo = 1,
                            Estado = true,
                            Mensaje = "El registro fue encontrado",
                            Objeto = funcionario

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

        public async Task ObtenerFuncionarioAsync(string _usuarioEmpresarial)
        {
            try
            {
                using (ContextoSipro db = new ContextoSipro())
                {
                    this.funcionario = await (from persona in db.VmRehuPersonal
                                              where persona.UsuarioEmpresarial == _usuarioEmpresarial
                                              select new VmRehuPersonalDto
                                              {
                                                  Apellidos = persona.Apellidos,
                                                  CargoActual = persona.CargoActual,
                                                  Consecutivo = persona.Consecutivo,
                                                  CorreoElectronico = persona.CorreoElectronico,
                                                  DescripcionDependencia = persona.DescripcionDependencia,
                                                  Fisica = persona.Fisica,
                                                  GradAlfabetico = persona.GradAlfabetico,
                                                  Identificacion = persona.Identificacion,
                                                  NombreGrado = persona.NombreGrado,
                                                  Nombres = persona.Nombres,
                                                  NumeroCelular = persona.NumeroCelular,
                                                  Sexo = persona.Sexo,
                                                  SiglaPapa = persona.SiglaPapa,
                                                  UndeConsecutivo = persona.UndeConsecutivo,
                                                  UndeConsecutivoLaborando = persona.UndeConsecutivoLaborando,
                                                  UndeFuerza = persona.UndeFuerza,
                                                  UsuarioEmpresarial = persona.UsuarioEmpresarial
                                              }).FirstOrDefaultAsync();


                    if (this.funcionario != null)
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

        #endregion

    }

}

