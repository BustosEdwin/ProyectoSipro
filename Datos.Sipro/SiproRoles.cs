namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_ROLES", Schema = "USR_SATDE")]
    public class SiproRoles
    {
        [Column("ID_ROL")]
        [Key]        
        public string IdRol { get; set; }
        [Column("DESCRIPCION_ROL")]
        public string DescripcionRol { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }

    }
}
