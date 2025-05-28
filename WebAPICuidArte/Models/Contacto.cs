using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Contacto
    {
        [Key]
        public int ContactoId { get; set; }
        public int AdultoMayorId { get; set; }

        [Required(ErrorMessage = "Campo Apellidos obligatorio.")]
        [Display(Name = "Apellidos del Contacto:")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Campo Nombres obligatorio.")]
        [Display(Name = "Nombres del Contacto:")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Campo Teléfono obligatorio.")]
        [Display(Name = "Teléfono de Contacto:")]
        public string Telefono { get; set; }
     
        [Required(ErrorMessage = "Campo Parentesco obligatorio.")]
        [Display(Name = "Parentesco con el Adulto Mayor:")]
        public string Parentesco { get; set; }
    }
}
