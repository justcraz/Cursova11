using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PharmaKursWork.Models;

namespace PharmaKursWork.ViewModels.UnitViewModels
{
    public class UnitViewModel
    {
        public int Id { get; set; }

        [DisplayName("Назва")]
        [Required(ErrorMessage = "Вкажіть назву одиниці виміру")]
        public string Name { get; set; }

        [DisplayName("Опис")]
        [Required(ErrorMessage = "Вкажіть опис одиниці вимірювання")]
        public string Desc { get; set; }

        public List<UnitMeasure> List { get; set; } = new List<UnitMeasure>();
    }
}
