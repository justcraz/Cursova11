using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.ChallengeViewModels
{
    public class AddChallengeViewModel
    {
        public AddChallengeModal AddChallengeModal { get; set; }
        public SelectList MedSelectList { get; set; }
        public SelectList ScientistSelectList { get; set; }
        public SelectList TechStaffSelectList { get; set; }
        
    }
    
    public class AddChallengeModal
    {
        public int Id { get; set; }

        [DisplayName("Назва")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; }

        [DisplayName("Початок випробування")]
        [Required(ErrorMessage = "Вкажіть початок випробування")]
        public DateTime ChallegesStart { get; set; }
        
        [DisplayName("Кінець випробування")]
        [Required(ErrorMessage = "Вкажіть кінець випробування")]
        public DateTime ChallegesEnd { get; }

        [DisplayName("Випробування займається")]
        [Required(ErrorMessage = "Вкажіть Вченого")]
        public int ScientistId { get; set; }

        [DisplayName("Назва ліків")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public int MedId { get; set; }

        [DisplayName("Відповідальний за роботу приладів")]
        [Required(ErrorMessage = "Вкажіть тих персонал")]
        public int TechStaffId { get; set; }
    }
}
