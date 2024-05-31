namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("VM_REHU_PERSONAL", Schema = "USR_SATDE")]
    public class VmRehuPersonal
    {
        #region Propiedaees
        [Column("SIGLA_PAPA")]
        public string SiglaPapa { get; set; }
        [Column("FISICA")]
        public string Fisica { get; set; }
        [Column("DESCRIPCION_DEPENDENCIA")]
        public string DescripcionDependencia { get; set; }
        [Column("UNDE_CONSECUTIVO_LABORANDO")]
        public decimal UndeConsecutivoLaborando { get; set; }      
        [Column("IDENTIFICACION")]
        public decimal Identificacion { get; set; }
        [Column("GRAD_ALFABETICO")]
        public string GradAlfabetico { get; set; }
        [Column("APELLIDOS")]
        public string Apellidos { get; set; }
        [Column("NOMBRES")]
        public string Nombres { get; set; }
        [Column("SEXO")]
        public string Sexo { get; set; }
        [Column("NOMBRE_GRADO")]
        public string NombreGrado { get; set; }
        [Column("UNDE_FUERZA")]
        public decimal UndeFuerza { get; set; }
        [Column("UNDE_CONSECUTIVO", Order = 1)]
        [Key]
        public decimal UndeConsecutivo { get; set; }
        [Column("CONSECUTIVO", Order = 2)]
        public decimal Consecutivo { get; set; }
        [Column("NUMERO_CELULAR")]
        public Nullable<decimal> NumeroCelular { get; set; }
        [Column("CORREO_ELECTRONICO")]
        public string CorreoElectronico { get; set; }
        [Column("CARGO_ACTUAL")]
        public string CargoActual { get; set; }
        [Column("USUARIO_EMPRESARIAL")]
        public string UsuarioEmpresarial { get; set; }

        #endregion

    }
}
