namespace Datos.Sipro
{
    using System;
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_USUARIO_ROL", Schema = "USR_SATDE")]
    public class SiproUsuarioRol
    {
        [Column("ID_USUARIO_ROL")]
        [Key]
        public string IdUsuarioRol { get; set; }
        [Column("ID_USUARIO")]
        public string IdUsuario { get; set; }
        [Column("ID_ROL")]
        public string IdRol { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }

        [Column("FECHA_INICIO")]
        public DateTime FechaInicio { get; set; }

        [Column("FECHA_FIN")]
        public DateTime FechaFin { get; set; }

        [ForeignKey("IdUsuario")]
        public virtual SiproUsuarios Usuarios { get; set; }
        [ForeignKey("IdRol")]
        public virtual SiproRoles Roles { get; set; }

    }
}
