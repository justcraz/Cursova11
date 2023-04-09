using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PharmaKursWork.ViewModels.LaboratoriesViewModels
{
    public class EditLaboratoriesModalViewModel
    {
        public int Id { get; set; }

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву лабораторії")]
        public string Name { get; set; }

        [DisplayName("Адреса лабораторії")]
        [Required(ErrorMessage = "Вкажіть адресу лабораторії")]
        public string Adress { get; set; }
    }

}
