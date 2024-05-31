namespace Comun.Sipro.Dto
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class SiproProyectoDto
    {
        #region Agregando Campos del Proyecto
        public string IdProyecto { get; set; }

        [Display(Name = "Nombre Proyecto")]
        public string Nombre { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Estado")]
        public string IdEstado { get; set; }
        public Nullable<decimal> IdUnidadResponsable { get; set; }

        [Display(Name = "Unidad Responsable")]
        public string SiglaUnidadResponsable { get; set; }

        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Usuario Creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Acrónimo")]
        public string Acronimo { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public Nullable<decimal> IdUnidadSolicitante { get; set; }
        public string SiglaUnidadSolicitante { get; set; }

        [DisplayFormat(DataFormatString = "{0:#.####}", ApplyFormatInEditMode = true)]
        public decimal Identificacion { get; set; }

        #region Descripcion de Identificadores de Referencia
        public string DescripcionEstado { get; set; }
        #endregion
        #endregion

        #region Agregando Campos para una observacion
        [Display(Name = "Descripción Proyecto")]
        public string Descripcion { get; set; }
  
        public string Grado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        [Display(Name = "Funcionario creación")]
        public string FuncionarioCreacion {
            get => $"{this.Grado}. {this.Nombres} {this.Apellidos}";
        }

        #endregion
    }
}
