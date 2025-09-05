using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Lectura
    {
        [Key]
        public int LecturaId { get; set; }
        public int AdultoId { get; set; }

        [Required(ErrorMessage = "Campo título obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres")]
        [Display(Name = "Título del libro")]
        public string TituloLibro { get; set; }

        [Required(ErrorMessage = "Campo autor obligatorio.")]
        [Display(Name = "Autor del libro")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Campo páginas totales obligatorio.")]
        [Display(Name = "Páginas totales")]
        public int PaginasTotales { get; set; }

        [Required(ErrorMessage = "Campo páginas leídas obligatorio.")]
        [Display(Name = "Páginas leídas")]
        public int PaginasLeidas { get; set; }
        [StringLength(100)]
        public string Editorial { get; set; }
        [StringLength(30)]
        public string Categoria { get; set; }
        [StringLength(10)]
        public string AnioPublicacion { get; set; }

        [Required(ErrorMessage = "Campo hora inicio obligatorio.")]
        [Display(Name = "Hora inicio")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "Campo hora fin obligatorio.")]
        [Display(Name = "Hora fin")]
        public string HoraFin { get; set; }

        [Required(ErrorMessage = "Campo minutos leídos obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe registrar al menos 1 minuto.")]
        [Display(Name = "Minutos leídos")]
        public int MinutosLeidos { get; set; }
        public string FechaRegistro { get; set; }
    }
}
