using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Ejercicio
    {
        [Key]
        public int EjercicioId { get; set; }
        public int AdultoMayorId { get; set; }
        [Required]
        [Display(Name = "Nombre del ejercicio")]
        [StringLength(50)]
        public string Nombre { get; set; }
        [Required]
        [Display(Name = "Detalles del ejercicio")]
        public string Detalles { get; set; }
        [Required]
        [Display(Name = "Es favorito")]
        public bool IsFavorito { get; set; }
        public string FechaCreacion { get; set; }
        public string Categoria { get; set; }
        public string Intensidad { get; set; }
    }
}
