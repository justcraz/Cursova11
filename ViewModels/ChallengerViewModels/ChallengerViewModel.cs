using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.ChallengerViewModels
{
    public class ChallengerViewModel
    {
        public SelectList ChallengeSelectList { get; set; }
        public List<ChallengerModel> List { get; set; }
        public ChallengerModel ChallengerModel { get; set; }
    }

    public class ChallengerModel
    {
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string FirstName { get; set; }

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Вкажіть прізвище")]
        public string LastName { get; set; }

        [DisplayName("Протипоказання")]
        [Required(ErrorMessage = "Вкажіть протипоказання")]
        public string Contraindications { get; set; }


        [DisplayName("Найменування випробування")]
        [Required(ErrorMessage = "Вкажіть випробування")]
        public string? ChallengeName { get; set; }

        [DisplayName("Випробування")]
        [Required(ErrorMessage = "Вкажіть випробування")]
        public int? ChallengeId { get; set; }

    }
}
