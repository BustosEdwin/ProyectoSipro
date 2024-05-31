using System;

namespace Comun.Sipro.Dto
{
    public class TrazabilidadDto
    {
        #region Propiedades
        public string NombreGradoFuncionario { get; set; }
        public string Unidad { get; set; }
        public string DescripcionComentario { get; set; }
        public string EstadoComentario { get; set; }
        public DateTime FechaOrganizacion { get; set; }
        #endregion
    }
}
