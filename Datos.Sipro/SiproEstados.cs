namespace Datos.Sipro
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_ESTADOS", Schema = "USR_SATDE")]
    public class SiproEstados
    {
        #region Propiedades
        [Column("ID_ESTADO")]
        [Key]
        public string IdEstado { get; set; }
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

        #region Propiedades de Referencias
        public virtual ICollection<SiproProyecto> EstadosProyecto { get; set; }

        #endregion
        #endregion
    }
}
