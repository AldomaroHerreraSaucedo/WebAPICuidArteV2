using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPICuidArte.Models
{
    public class Consejo
    {
        [Key]
        public int ConsejoId { get; set; }
        public int AdultoMayorId { get; set; }

        [Required]
        [Display(Name = "Título del consejo")]
        [StringLength(100)]
        public string Titulo { get; set; }

        [Required]
        [Display(Name = "Descripción del consejo")]
        [StringLength(1000)]
        public string Descripcion { get; set; }

        [Display(Name = "Categoría")]
        [StringLength(50)]
        public string Categoria { get; set; }

        [Display(Name = "Fecha de generación")]
        public string FechaGenerado { get; set; }

        [Display(Name = "¿Es favorito?")]
        public bool EsFavorito { get; set; } = false;
        [Display(Name = "¿Tiene recordatorio?")]
        public bool TieneRecordatorio { get; set; } = false;
        public string FechaRecordatorio { get; set; }

        [StringLength(5)]
        public string HoraRecordatorio { get; set; }
        public string Dias { get; set; }

    }
}
