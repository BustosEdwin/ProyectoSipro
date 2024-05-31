namespace Comun.Sipro.Dto
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public class SiproEvidenciaDto
    {
        #region Propiedades
        public string IdEvidencia { get; set; }
        public string IdBitacora { get; set; }
        public string IdObservaciones { get; set; }
        public string UrlRuta { get; set; }
        public string NombreArchivo
        {
            get
            {
                var rutaDividiad = this.UrlRuta.Split('/');
                return rutaDividiad[1];
            }
        }
        public HttpPostedFileBase Archivo { get; set; }
        public string IdResponsable { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
        public string Observacion { get; set; }
        public string NombreGradoResponsable { get; set; }
        public string NombreProyecto { get; set; }
        public string DescripcionProyecto { get; set; }

        #region Para la firma
        public string Usuario { get; set; }
        public string Contrasena { get; set; }

        public string Nombre_proyecto { get; set; }
        public string sigla_proyecto { get; set; }
        public string fase_proyecto { get; set; }

        public string descripcion_documento { get; set; }

        public string GradoEnvia { get; set; }
        public string NombresEnvia { get; set; }

        public string ApellidoEnvia { get; set; }

        public string IdResponsableEnvia { get; set; }
        public string Estado { get; set; }
        public string IdProyecto { get; set; }

        public string IdActividad { get; set; }


        #endregion

        #region Comentario
        public List<SiproComentarioDto> Comentario { get; set; }
        public string IdComentario { get; set; }
        #endregion

        #endregion
    }
}
