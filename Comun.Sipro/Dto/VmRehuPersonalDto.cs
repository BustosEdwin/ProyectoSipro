namespace Comun.Sipro.Dto
{
    using System;
    public class VmRehuPersonalDto
    {
        #region Propiedades
        public string SiglaPapa { get; set; }
        public string Fisica { get; set; }
        public string DescripcionDependencia { get; set; }
        public decimal UndeConsecutivoLaborando { get; set; }
        public decimal Identificacion { get; set; }
        public string GradAlfabetico { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public string Sexo { get; set; }
        public string NombreGrado { get; set; }
        public decimal UndeFuerza { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal Consecutivo { get; set; }
        public Nullable<decimal> NumeroCelular { get; set; }
        public string CorreoElectronico { get; set; }
        public string CargoActual { get; set; }
        public string UsuarioEmpresarial { get; set; }

        #endregion
    }
}
