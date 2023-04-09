using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.UnitViewModels
{
    public class AddUnitModalViewModel
    {
        public int Id { get; set; }

        [DisplayName("Назва")]
        [Required(ErrorMessage = "Вкажіть назву одиниці виміру")]
        public string Name { get; set; }

        [DisplayName("Опис")]
        [Required(ErrorMessage = "Вкажіть опис одиниці вимірювання")]
        public string Desc { get; set; }
    }
}
