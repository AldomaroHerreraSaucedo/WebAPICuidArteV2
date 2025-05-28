using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Enfermedad
    {
        [Key]
        public int EnfermedadId { get; set; }

        [Required(ErrorMessage = "Campo Nombre obligatorio.")]
        [Display(Name = "Nombre de la enfermedad:")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Campo Sintomas obligatorio.")]
        [Display(Name = "Sintoma de la enfermedad:")]
        public string Descripcion { get; set; }
    }
}
