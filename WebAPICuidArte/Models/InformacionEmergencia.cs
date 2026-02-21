using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class InformacionEmergencia
    {
        [Key]
        public int InfoEmergenciaId { get; set; }

        [Required(ErrorMessage = "El ID del Adulto Mayor es obligatorio.")]
        public int AdultoMayorId { get; set; }

        [Required(ErrorMessage = "El Número de Seguro Social es obligatorio.")]
        [Display(Name = "Número de Seguro Social")]
        public string NumeroSeguroSocial { get; set; }

        [Required(ErrorMessage = "El Tipo de Sangre es obligatorio.")]
        [Display(Name = "Tipo de Sangre")]
        public string TipoSangre { get; set; }

        [Display(Name = "Alergias")]
        public string? Alergias { get; set; }

        [Display(Name = "Enfermedades")]
        public string? Enfermedades { get; set; }

    }
}
