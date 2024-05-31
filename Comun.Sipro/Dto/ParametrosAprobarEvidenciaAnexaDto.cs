namespace Comun.Sipro.Dto
{
    using System.Collections.Generic;

    public class ParametrosAprobarEvidenciaAnexaDto
    {

        #region Propiedades
        public List<string> LstIdentificadorResponsables { get; set; }
        public string IdEvidencia { get; set; }
        public string Comentario { get; set; }
        public string IdProyecto { get; set; }
        #endregion
    }
}
