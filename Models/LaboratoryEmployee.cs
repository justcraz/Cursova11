using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class LaboratoryEmployee
    {
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Вкажіть прізвище")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Вкажіть номер телефону")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Вкажіть email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Адреса")]
        [Required(ErrorMessage = "Вкажіть адрес")]
        public string Adress { get; set; } = string.Empty;

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву лабораторії")]
        public int LabratoryId { get; set; }

        [ForeignKey(nameof(LabratoryId))]
        public Laboratory Laboratory { get; set; }
    }
}
