namespace Comun.Sipro.Utilidades
{
    using System;
    public class FuncionarioDTO
    {
        public string GradAlfabetico { set; get; }
        public string Nombres { set; get; }
        public string Apellidos { set; get; }
        public decimal Identificacion { set; get; }
        public string Sexo { set; get; }
        public string CargoActual { set; get; }
        public decimal? NumeroCelular { set; get; }
        public string CorreoElectronico { set; get; }
        public string Fisica { set; get; }
        public string DescripcionDependencia { set; get; }
        public string SiglaPapa { set; get; }
        public decimal UndeConsecutivoLaborando { get; set; }
        public decimal Consecutivo { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal UndeFuerza { get; set; }
        public string UsuarioEmpresarial { get; set; }
    }
}
