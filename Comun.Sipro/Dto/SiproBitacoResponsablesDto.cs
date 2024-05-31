namespace Comun.Sipro.Dto
{
    using System;


    public class SiproBitacoResponsablesDto
    {
        #region Propiedades
        public string IdBitacoResponsable { get; set; }
        public string IdResponsable { get; set; }
        public string IdBitacora { get; set; }
        public string Firma { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public decimal Identificacion { get; set; }
        #endregion

        #region Propiedades Funcionario

        public string Grado { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Cargo { get; set; }

        public string TipoResponsabilidad { get; set; }

        public string NombreCompletoGrado
        {
            get
            {
                return $"{this.Grado} {this.Nombres} {this.Apellidos} - {TipoResponsabilidad}";
            }
        }

        #endregion
    }
}
