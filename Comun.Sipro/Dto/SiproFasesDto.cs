﻿namespace Comun.Sipro.Dto
{
    using System;

    public class SiproFasesDto
    {
        #region Propiedades
        public string IdFase { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        #endregion

    }
}
