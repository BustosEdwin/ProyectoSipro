namespace Datos.Sipro
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_TRAZA_RESPONSABLE", Schema = "USR_SATDE")]
    public class SiproTrazaResponsable
    {
        #region Propiedades
        [Column("ID_TRAZA_RESPONSABLE")]
        public string IdTrazaResponsble { get; set; }
        [Column("ID_COMENTARIO")]
        public string IdComentario { get; set; }
        [Column("ID_ESTADO_COMENTARIO")]
        public string IdEstadoComentario { get; set; }
        [Column("ID_RESPONSABLE")]
        public string IdResponsable { get; set; }
        #endregion
    }
}
