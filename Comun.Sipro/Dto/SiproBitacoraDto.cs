namespace Comun.Sipro.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SiproBitacoraDto
    {
        #region Propiedades
        public string IdBitacora { get; set; }
        public string IdProyecto { get; set; }
        public string IdObservacion { get; set; }
        public string IdFase { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime FechaFin { get; set; }
        [Display(Name = "Fecha Incio")]
        public DateTime FechaInicio { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Usuario Creación")]
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        [Display(Name = "Observación")]
        public string Observacion { get; set; }
        public string Fase { get; set; }
        #endregion

        #region Evidencia Estado
        public bool EvidenciaValida { get; set; }
        #endregion
    }
}
