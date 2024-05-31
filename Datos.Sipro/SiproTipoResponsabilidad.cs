namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_TIPO_RESPONSABILIDAD", Schema = "USR_SATDE")]
    public class SiproTipoResponsabilidad
    {
        #region Propiedades
        [Column("ID_TIPO_RESPONSABILIDAD")]
        [Key]
        public string IdTipoResponsabilidad { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        #endregion
    }
}
