using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class TechStaff
    {
        [Key]
        public int LaboratoryEmployeeId { get; set; }

        [DisplayName("Обслуговує прилад")]
        [Required(ErrorMessage = "Вкажіть який прилад обслуговується")]
        public string MaintainsDevice { get; set; }

        [DisplayName("Чи є свої інструменти")]
        [Required(ErrorMessage = "Вкажіть, чи є інструменти")]

        public bool HasYourInstruments { get; set; }
        [ForeignKey(nameof(LaboratoryEmployeeId))]
        public LaboratoryEmployee laboratoryEmployee { get; set; }
    }
}
