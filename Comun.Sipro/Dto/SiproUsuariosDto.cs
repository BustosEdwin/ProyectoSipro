namespace Comun.Sipro.Dto
{
    using System;
    public class SiproUsuariosDto
    {
        public string IdUsuario { get; set; }
        public string UsuarioEmpresarial { get; set; }
        public decimal Consecutivo { get; set; }
        public decimal UndeConsecutivo { get; set; }
        public decimal UndeFuerza { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string MaquinaCreacion { get; set; }
        public decimal Vigente { get; set; }
    }
}
