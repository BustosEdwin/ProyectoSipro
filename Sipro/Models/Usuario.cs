namespace Sipro.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Usuario
    {
        [Required]
        [Display(Name = "Usuario")]
        public string usuario { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string clave { get; set; }
        //[Required]
        public string sistema { get; set; }
    }
}