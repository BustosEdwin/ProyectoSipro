namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_BITACO_RESPONSABLES", Schema ="USR_SATDE")]
    public class SiproBitacoResponsables
    {
        #region Propiedades
        [Column("ID_BITACO_RESPONSABLE")]
        [Key]
        public string IdBitacoResponsable { get; set; }
        [Column("ID_RESPONSABLE")]
        public string IdResponsable { get; set; }
        [Column("ID_BITACORA")]
        public string IdBitacora { get; set; }
        [Column("FIRMA")]
        public string Firma { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        #region Propiedades de Referencia

        [ForeignKey("IdResponsable")]
        public virtual SiproResponsable ResponsableBitacoResponsable { get; set; }
        [ForeignKey("IdBitacora")]
        public virtual SiproBitacora BitacoraBitacoResponsable { get; set; }
        #endregion
        #endregion
    }
}
