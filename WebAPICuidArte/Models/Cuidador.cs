using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Cuidador
    {
        [Key]
        public int CuidadorId { get; set; }

        [Required(ErrorMessage = "Campo Apellidos obligatorio.")]
        [Display(Name = "Apellidos del Cuidador:")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Campo Nombres obligatorio.")]
        [Display(Name = "Nombres del Cuidador:")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Campo Correo obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [Display(Name = "Correo Electrónico:")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Campo Contraseña obligatorio.")]
        [StringLength(15, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 15 caracteres.")]
        [Display(Name = "Contraseña:")]
        public string Contrasenia { get; set; }
    }
}
