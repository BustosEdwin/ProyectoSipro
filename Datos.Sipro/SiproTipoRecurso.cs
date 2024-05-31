namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_TIPO_RECURSO", Schema = "USR_SATDE")]
    public class SiproTipoRecurso
    {
        #region Propiedades
        [Column("ID_TIPO_RECURSO")]
        [Key]
        public string IdTipoRecurso { get; set; }
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
