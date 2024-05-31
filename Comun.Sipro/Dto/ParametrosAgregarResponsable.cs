using System;

namespace Comun.Sipro.Dto
{
    public class ParametrosAgregarResponsable
    {
        #region Propiedades
        public string IdProyecto { get; set; }
        public string IdTipoResponsable { get; set; }
        public long Identificacion { get; set; }
        public Nullable<decimal> IdUnidad { get; set; }
        #endregion
    }
}
