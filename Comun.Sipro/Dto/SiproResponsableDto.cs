namespace Comun.Sipro.Dto
{
    using System;
    public class SiproResponsableDto
    {
        #region Propiedades

        public string IdResponsable { get; set; }
        public string IdProyecto { get; set; }
        public string Grado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }
        public Nullable<decimal> IdUnidad { get; set; }
        public string IdTipoResponsable { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public decimal Identificacion { get; set; }
        public Nullable<decimal> UndeConsecutivo { get; set; }
        public Nullable<decimal> UndeFuerza { get; set; }
        public Nullable<decimal> Consecutivo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public string TipoResponsabilidad { get; set; }
        public string IdTipoResponsabilidad { get; set; }
        public DateTime? FechaFin { get; set; }
        public bool? Activo { get; set; }
        public string Observaciones { get; set; }
        public string NombreCompletoGrado
        {
            get
            {
                return $"{this.Grado} {this.Nombres} {this.Apellidos} - {TipoResponsabilidad}";
            }
        }
        public string DescripcionActivo
        {
            get
            {
                if (this.Activo.HasValue)
                    if (this.Activo.Value)
                        return "SI";
                    else
                        return "NO";
                else
                    return "SIN VALOR";
            }
        }
        #endregion
    }
}
