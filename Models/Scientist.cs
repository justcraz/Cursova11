using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class Scientist 
    {
        [Key]
        public int LaboratoryEmployeeId { get; set; }

        [DisplayName("Працює за приладом")]
        [Required(ErrorMessage = "Вкажіть прилад")]
        public string ResponsibleForDevice { get; set; }

        [DisplayName("Напрямок досліджень")]
        [Required(ErrorMessage = "Вкажіть адресу лабораторії")]
        public string DirectionDevelopment { get; set; }
        [ForeignKey(nameof(LaboratoryEmployeeId))]
        public LaboratoryEmployee laboratoryEmployee { get; set; }
    }
}
