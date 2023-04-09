using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.ChallengeViewModels
{
    public class ChallengeView
    {
        public int Id { get; set; }

        [DisplayName("Назва")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Початок випробування")]
        [Required(ErrorMessage = "Вкажіть початок випробування")]
        public DateTime ChallegesStart { get; set; }

        [DisplayName("Випробування займається")]
        [Required(ErrorMessage = "Вкажіть Вченого")]
        public string ScientistName { get; set; } = string.Empty;

        [DisplayName("Назва ліків")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string MedName { get; set; } = string.Empty;

        [DisplayName("Відповідальний за роботу приладів")]
        [Required(ErrorMessage = "Вкажіть тих персонал")]
        public string TechStaffName { get; set; } = string.Empty;
    }
    public class ChallengeViewModel
    {
        public SelectList MedSelectList { get; set; }
        public AddChallengeViewModel AddView { get; set; }
        public ChallengeView View { get; set; }
        public List<ChallengeView> List { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
    }

    public class FilterViewModel
    {

        [DisplayName("Мінімальна дата")]
        public DateTime MinChallegesStart { get; set; }

        [DisplayName("Максимальна дата")]
        public DateTime MaxChallegesStart { get; set; }

        [DisplayName("Назва")]
        public string Name { get; set; }
    }
}
