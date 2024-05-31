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

    public class AdministracionUsuarios
    {

        private EstadoRespuesta estadoRespuesta;

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


        public async Task<FuncionarioDTO> ConsultarPorUsuarioEmpresarial(string _email)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                var resultado = await db.VmRehuPersonal.Where(x => x.UsuarioEmpresarial == _email).Select(x => new FuncionarioDTO
                {
                    Identificacion = x.Identificacion,
                    GradAlfabetico = x.GradAlfabetico,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    CargoActual = x.CargoActual,
                    CorreoElectronico = x.CorreoElectronico,
                    DescripcionDependencia = x.DescripcionDependencia,
                    Fisica = x.Fisica,
                    NumeroCelular = x.NumeroCelular,
                    Sexo = x.Sexo,
                    SiglaPapa = x.SiglaPapa,
                    UndeConsecutivoLaborando = x.UndeConsecutivoLaborando,
                    UsuarioEmpresarial = x.UsuarioEmpresarial,
                    Consecutivo = x.Consecutivo,
                    UndeConsecutivo = x.UndeConsecutivo,
                    UndeFuerza = x.UndeFuerza
                }).FirstOrDefaultAsync();

                return resultado;
            }
        }

        public async Task<FuncionarioDTO> ConsultarUsuarioPorIdentificacion(decimal _identificacion)
        {
            using (ContextoSipro db = new ContextoSipro())
            {
                var resultado = await db.VmRehuPersonal.Where(x => x.Identificacion == _identificacion).Select(x => new FuncionarioDTO
                {
                    Identificacion = x.Identificacion,
                    GradAlfabetico = x.GradAlfabetico,
                    Nombres = x.Nombres,
                    Apellidos = x.Apellidos,
                    CargoActual = x.CargoActual,
                    CorreoElectronico = x.CorreoElectronico,
                    DescripcionDependencia = x.DescripcionDependencia,
                    Fisica = x.Fisica,
                    NumeroCelular = x.NumeroCelular,
                    Sexo = x.Sexo,
                    SiglaPapa = x.SiglaPapa,
                    UndeConsecutivoLaborando = x.UndeConsecutivoLaborando,
                    UsuarioEmpresarial = x.UsuarioEmpresarial
                }).FirstOrDefaultAsync();

                return resultado;
            }
        }

        public async Task<List<SiproRolDto>> ConsultaRol(string _usuario)
        {
            //try
            //{
                using (ContextoSipro db = new ContextoSipro())
                {

                    var usuariof = (from m in db.SiproUsuarios
                                    where m.UsuarioEmpresarial == _usuario
                                    select m.IdUsuario).FirstOrDefault();

                    var Resultado = await (from n in db.SiproUsuarioRol
                                           from s in db.SiproRoles
                                           where n.IdUsuario == usuariof
                                           && s.IdRol == n.IdRol
                                           orderby n.FechaCreacion descending
                                           select new SiproRolDto
                                           {
                                               DescripcionRol = s.DescripcionRol
                                           }).ToListAsync();


                    return Resultado;
                }
               
            }
            //catch (Exception ex)
            //{
            //    this.estadoRespuesta = new EstadoRespuesta
            //    {
            //        Codigo = -1,
            //        Estado = false,
            //        Mensaje = $"Ocurrio Una excepción: {ex.Message}"
            //    };
            //}
          

        
      
    }
}
