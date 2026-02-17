using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICuidArte.Models
{
    public class InformacionEmergencia
    {
        [Key]
        public int IdInfoEmergencia { get; set; }

        [Required(ErrorMessage = "El ID del Adulto Mayor es obligatorio.")]
        public int IdAdultoMayor { get; set; }

        [Required(ErrorMessage = "El Número de Seguro Social es obligatorio.")]
        [StringLength(18, MinimumLength = 8, ErrorMessage = "El Número de Seguro debe tener entre 8 y 18 caracteres.")]
        [Display(Name = "Número de Seguro Social")]
        public string NumeroSeguroSocial { get; set; }

        [Required(ErrorMessage = "El Tipo de Sangre es obligatorio.")]
        [StringLength(5, ErrorMessage = "El Tipo de Sangre no debe exceder 5 caracteres.")]
        [Display(Name = "Tipo de Sangre")]
        public string TipoSangre { get; set; }

        [StringLength(500, ErrorMessage = "Las Alergias no deben exceder 500 caracteres.")]
        [Display(Name = "Alergias")]
        public string? Alergias { get; set; }

        [StringLength(500, ErrorMessage = "Los Medicamentos Actuales no deben exceder 500 caracteres.")]
        [Display(Name = "Medicamentos Actuales")]
        public string? MedicamentosActuales { get; set; }

        [StringLength(500, ErrorMessage = "Las Enfermedades Crónicas no deben exceder 500 caracteres.")]
        [Display(Name = "Enfermedades Crónicas")]
        public string? EnfermedadesCronicas { get; set; }

        // Navegación
        [ForeignKey("IdAdultoMayor")]
        public virtual AdultoMayor? AdultoMayor { get; set; }
    }
}
