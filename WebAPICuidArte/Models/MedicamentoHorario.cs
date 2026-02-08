using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebAPICuidArte.Models
{
    public class MedicamentoHorario
    {
        [Key]
        public int MedicamentoHorarioId { get; set; }

        [Required]
        public int MedicamentoId { get; set; }

        [Required]
        public string Hora { get; set; } // ideal: TimeSpan

        [JsonIgnore]
        public Medicament? Medicamento { get; set; }
    }
}
