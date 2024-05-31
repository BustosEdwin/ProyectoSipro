namespace Datos.Sipro
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PORTAL_UNIDADES_SIGLAS", Schema = "USR_SATDE")]
    public class PortalUnidadesSiglas
    {
        [Column("SIGLA")]
        public string Sigla { get; set; }
        [Column("CONSECUTIVO")]
        [Key]
        public decimal Consecutivo { get; set; }
    }
}
