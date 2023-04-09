using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PharmaKursWork.Models;

namespace PharmaKursWork.ViewModels.LaboratoriesViewModels
{
    public class LaboratoriesViewModel
    {
        public int Id { get; set; }

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву лабораторії")]
        public string Name { get; set; }

        [DisplayName("Адреса лабораторії")]
        [Required(ErrorMessage = "Вкажіть адресу лабораторії")]
        public string Adress { get; set; }

        public List<Laboratory> List { get; set; } = new List<Laboratory>();
    }
}
