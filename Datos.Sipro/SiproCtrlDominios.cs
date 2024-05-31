namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;

    [Table("SIPRO_CTRL_DOMINIOS", Schema = "USR_SATDE")]
    public class SiproCtrlDominios
    {
        [Column("ID_DOMINIO")]
        [Key]
        public decimal IdDominio { get; set; }
        [Column("DESCRIPCION")]
        public string Descripcion { get; set; }
        [Column("PADRE_ID")]
        public decimal PadreId { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }
        [Column("ABREVIATURA")]
        public string Abreviatura { get; set; }
        [Column("OBSERVACION")]
        public string Observacion { get; set; }
       

        //public class CarDBCtxt : ContextoSipro
        //{
        //    public DbSet<SiproComentario> siproComentario { get; set; }
        //}
    }
}
