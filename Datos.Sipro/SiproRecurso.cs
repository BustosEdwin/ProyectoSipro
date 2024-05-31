namespace Datos.Sipro
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SIPRO_RECURSO", Schema = "USR_SATDE")]
    public class SiproRecurso
    {
        #region Propiedades
        [Column("ID_RECURSO")]
        [Key]
        public string IdRecurso { get; set; }
        [Column("ID_PROYECTO")]
        public string IdProyecto { get; set; }
        [Column("ID_TIPO_RECURSO")]
        public string IdTipoRecurso { get; set; }
        [Column("NOMBRE")]
        public string Nombre { get; set; }
        [Column("DIRECCION_IP")]
        public string DireccionIp { get; set; }
        [Column("BASE_DATOS")]
        public string BaseDatos { get; set; }
        [Column("ADICIONALES")]
        public string Adicionales { get; set; }
        [Column("FECHA_CREACION")]
        public DateTime FechaCreacion { get; set; }
        [Column("USUARIO_CREACION")]
        public string UsuarioCreacion { get; set; }
        [Column("MAQUINA_CREACION")]
        public string MaquinaCreacion { get; set; }
        [Column("VIGENTE")]
        public decimal Vigente { get; set; }

        #region PropiedadesReferencia
        [ForeignKey("IdProyecto")]
        public virtual SiproProyecto ProyectoRecurso { get; set; }
        [ForeignKey("IdTipoRecurso")]
        public virtual SiproTipoRecurso TipoRecursoRecurso { get; set; }
        #endregion

        #endregion
    }
}
