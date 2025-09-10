using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Lectura2
    {
        [Key]
        public int LecturaId { get; set; }
        public int AdultoId { get; set; }

        [Required(ErrorMessage = "Campo título obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede superar los 100 caracteres")]
        public string TituloLibro { get; set; }

        [Required(ErrorMessage = "Campo autor obligatorio.")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Campo páginas totales obligatorio.")]
        public int PaginasTotales { get; set; }

        public string Editorial { get; set; }
        public string Categoria { get; set; }
        public string AnioPublicacion { get; set; }

        public string FechaRegistro { get; set; }
    }
}
