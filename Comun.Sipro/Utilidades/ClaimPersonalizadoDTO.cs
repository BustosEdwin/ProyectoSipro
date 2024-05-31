using Newtonsoft.Json;

namespace Comun.Sipro.Utilidades
{
    public static class ClaimPersonalizadoDTO
    {
        public const string GradAlfabetico = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/grado";
        public const string Nombres = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nombres";
        public const string Apellidos = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/apellidos";
        public const string Identificacion = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/identificacion";
        public const string Sexo = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/sexo";
        public const string CargoActual = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/cargo";
        public const string UsuarioEmpresarial = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/usuarioempresarial";
        public const string DescripcionDependencia = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/descripciondependencia";
        public const string CorreoElectronico = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/correo";
        public const string Fisica =  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/fisica";
        public const string NumeroCelular = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/celular";
        public const string SiglaPapa = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/siglapapa";
        public const string UndeConsecutivoLaborando = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/undeconsecutivolaborando";
        public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
        public const string Consecutivo = "http://schemas.microsoft.com/ws/2005/05/identity/claims/Consecutivo";
        public const string UndeConsecutivo = "http://schemas.microsoft.com/ws/2005/05/identity/claims/UndeConsecutivo";
        public const string UndeFuerza = "http://schemas.microsoft.com/ws/2005/05/identity/claims/UndeFuerza";

        public class ClaimPersonalizados
        {
            #region Propiedades
            public string GradAlfabetico { get; set; }
            public string Nombres { get; set; }
            public string Apellidos { get; set; }
            public string Identificacion { get; set; }
            public string Sexo { get; set; }
            public string CargoActual { get; set; }
            public string NumeroCelular { get; set; }
            public string UsuarioEmpresarial { get; set; }
            public string Fisica { get; set; }
            public string DescripcionDependencia { get; set; }
            public string CorreoElectronico { get; set; }
            public string SiglaPapa { get; set; }
            public string UndeConsecutivoLaborando { get; set; }
            public string[] Roles { get; set; }

            public string Consecutivo { get; set; }
            public string UndeConsecutivo { get; set; }
            public string UndeFuerza { get; set; }
            #endregion

            #region Metodos
            public string ToJson()
            {
                return JsonConvert.SerializeObject(this);
            }

            #endregion

        }
    }
}
