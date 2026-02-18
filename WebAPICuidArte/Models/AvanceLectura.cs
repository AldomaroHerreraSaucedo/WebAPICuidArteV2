using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class AvanceLectura
    {
        [Key]
        public int AvanceId { get; set; }
        public int LecturaId { get; set; }

        [Required(ErrorMessage = "Campo hora inicio obligatorio.")]
        public string HoraInicio { get; set; }

        [Required(ErrorMessage = "Campo hora fin obligatorio.")]
        public string HoraFin { get; set; }

        [Required(ErrorMessage = "Campo páginas leídas obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe registrar al menos 1 página.")]
        public int PaginasLeidas { get; set; }

        [Required(ErrorMessage = "Campo minutos leídos obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe registrar al menos 1 minuto.")]
        public int MinutosLeidos { get; set; }

        public string FechaRegistro { get; set; }
        public string Apreciacion { get; set; }
    }
}
