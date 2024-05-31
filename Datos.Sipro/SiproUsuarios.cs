namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_USUARIOS", Schema = "USR_SATDE")]
    public class SiproUsuarios
    {
        [Column("ID_USUARIO")]
        [Key]
        public string IdUsuario { get; set; }
        [Column("USUARIO_EMPRESARIAL")]
        public string UsuarioEmpresarial { get; set; }
        [Column("CONSECUTIVO")]
        public decimal Consecutivo { get; set; }
        [Column("UNDE_CONSECUTIVO")]
        public decimal UndeConsecutivo { get; set; }
        [Column("UNDE_FUERZA")]
        public decimal UndeFuerza { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal  Vigente{ get; set; }


    }
}
