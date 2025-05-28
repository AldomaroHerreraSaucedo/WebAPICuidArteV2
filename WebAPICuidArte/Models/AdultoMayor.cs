using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class AdultoMayor
    {
        [Key]
        public int AdultoMayorId { get; set; }

        [Required(ErrorMessage = "Campo Apellidos obligatorio.")]
        [Display(Name = "Apellidos del Adulto Mayor:")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Campo Nombres obligatorio.")]
        [Display(Name = "Nombres del Adulto Mayor:")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Campo Fecha de Nacimiento obligatorio.")]
        [Display(Name = "Fecha de Nacimiento:")]
        public string FechaNacimiento { get; set; }

        [Required(ErrorMessage = "Campo Sexo obligatorio.")]
        [Display(Name = "Sexo:")]
        public string Sexo { get; set; }

        [Required(ErrorMessage = "Campo Peso obligatorio.")]
        [Range(0, 100, ErrorMessage = "El peso debe estar entre 0 y 100 kg.")]
        [Display(Name = "Peso (kg):")]
        public double Peso { get; set; }

        [Required(ErrorMessage = "Campo Talla obligatorio.")]
        [Range(0, 200, ErrorMessage = "La talla debe estar entre 0 y 200 cm.")]
        [Display(Name = "Talla (cm):")]
        public double Talla { get; set; }

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
