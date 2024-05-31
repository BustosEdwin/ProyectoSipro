namespace Comun.Sipro.Dto
{
    using System;
    public class SiproRecursoDto
    {
        #region Propiedades
        public string IdRecurso { get; set; }
        public string IdProyecto { get; set; }
        public string IdTipoRecurso { get; set; }
        public string TipoRecurso { get; set; }
        public string Nombre { get; set; }
        public string DireccionIp { get; set; }
        public string BaseDatos { get; set; }
        public string Adicionales { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
       
        #endregion
    }
}
