using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class CitaMedica
    {
        [Key]
        public int CitaId { get; set; }
        public int AdultoMayorId { get; set; }

        [Required(ErrorMessage = "Campo Fecha obligatorio.")]
        [Display(Name = "Fecha de la cita:")]
        public string Fecha { get; set; }

        [Required(ErrorMessage = "Campo Hora obligatorio.")]
        [Display(Name = "Hora de la cita:")]
        public string Hora { get; set; }

        [Required(ErrorMessage = "Campo Noombre del Doctor obligatorio.")]
        [Display(Name = "Nombre del Doctor:")]
        public string NombreDoctor { get; set; }

        [Required(ErrorMessage = "Campo Direccion obligatorio.")]
        [Display(Name = "Direccion:")]
        public string Direccion { get; set; }
        [Required(ErrorMessage = "Campo Es Atendido obligatorio.")]
        [Display(Name = "Es Atendido:")]
        public bool EsAtendido { get; set; }
    }
}
