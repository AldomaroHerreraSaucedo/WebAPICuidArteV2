using System.ComponentModel.DataAnnotations;

namespace WebAPICuidArte.Models
{
    public class Medicamento
    {
        [Key]
        public int MedicamentoId { get; set; }

        [Required(ErrorMessage = "Campo Nombre del Medicamento obligatorio.")]
        [Display(Name = "Nombre del Medicamento:")]
        public string NombreMedicamento { get; set; }

        [Required(ErrorMessage = "Campo Dosis obligatorio.")]
        [Display(Name = "Dosis del Medicamento:")]
        public string Dosis { get; set; }

        [Required(ErrorMessage = "Campo Fecha Inicio obligatorio.")]
        [Display(Name = "Fecha de Inicio:")]
        public string FechaInicio { get; set; }

        [Required(ErrorMessage = "Campo Fecha Fin obligatorio.")]
        [Display(Name = "Fecha de Fin:")]
        public string FechaFin { get; set; }

        [Required(ErrorMessage = "Campo Descripción obligatorio.")]
        [Display(Name = "Descripción del Medicamento:")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Campo Días obligatorio.")]
        [Display(Name = "Días a la semana para el Medicamento:")]
        public string DiasSemana { get; set; }

        [Required(ErrorMessage = "Campo Estado obligatorio.")]
        [Display(Name = "Estado:")]
        public int Estado { get; set; }

        public ICollection<MedicamentoHorario> Horarios { get; set; } = new List<MedicamentoHorario>();

    }
}
