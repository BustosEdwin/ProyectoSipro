namespace Comun.Sipro.Dto
{
    using System;
    using System.Collections.Generic;

    public class SiproCtrlDominioDTO
    {
        #region Propiedades    
            public decimal IdDominio { get; set; }
            public string Descripcion { get; set; }
            public decimal PadreId { get; set; }
            public decimal Vigente { get; set; }
            public string Abreviatura { get; set; }
            public string Observacion { get; set; }

        
            #endregion
        }
}
