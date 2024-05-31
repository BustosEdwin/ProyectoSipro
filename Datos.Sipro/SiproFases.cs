namespace Datos.Sipro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("SIPRO_FASES", Schema = "USR_SATDE")]
    public class SiproFases
    {
        #region Propiedades
        [Column("ID_FASE")]
        [Key]
        public string IdFase { get; set; }
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

        #region Propiedades de Rerefencia
        //public virtual ICollection<SiproBitacora> FaseBitacora { get; set; }

        #endregion
        #endregion
    }
}
