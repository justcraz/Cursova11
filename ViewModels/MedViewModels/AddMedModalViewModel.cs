using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PharmaKursWork.ViewModels.MedViewModels
{
    public class AddMedModal
    {
        
        public int Id { get; set; }

        [DisplayName("Назва")]
        [Required(ErrorMessage = "Вкажіть назву")]
        public string Name { get; set; } = string.Empty;

        [DisplayName("Початок розробки")]
        [Required(ErrorMessage = "Вкажіть початок розробки")]
        public DateTime StartExploring { get; set; }

        [DisplayName("Кінець розробки")]
        [Required(ErrorMessage = "Вкажіть кінець розробки")]
        public DateTime EndExploring { get; set; }

        [DisplayName("Внесок у розробку")]
        [Required(ErrorMessage = "Вкажіть внесок у розробку")]
        public decimal Expenses { get; set; }

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву назву лабораторії")]
        public int LabratoryId { get; set; }

        [DisplayName("Назва товарної групи")]
        [Required(ErrorMessage = "Вкажіть назву товарної групи")]
        public int CommodityGroupId { get; set; }

        [DisplayName("Одиниця виміру")]
        [Required(ErrorMessage = "Вкажіть одиницю виміру")]
        public int UnitMeasureId { get; set; }
    }

    public class AddMedModalViewModel
    {
        public AddMedModal AddMedModal { get; set; }
        public SelectList LaboratoryList { get; set; }

        public SelectList CommodityGroupsList { get; set; }

        public SelectList UnitMeasuresList { get; set; }
    }
}
