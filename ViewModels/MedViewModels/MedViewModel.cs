using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;

namespace PharmaKursWork.ViewModels.MedViewModels
{
    public class MedCustomViewModel
    {
        [DisplayName("Один")]
        public int Id { get; set; }

        [DisplayName("Назва")]
        public string Name { get; set; }

        [DisplayName("Початок розробки")]
        public DateTime StartExploring { get; set; }

        [DisplayName("Кінець розробки")]
        public DateTime EndExploring { get; set; }

        [DisplayName("Внесок")]
        public decimal Expenses { get; set; }

        [DisplayName("Лабораторія")]
        public string LabratoryName { get; set; }

        [DisplayName("Товарна група")]
        public string CommodityName { get; set; }

        [DisplayName("Одиниця виміру")]
        public string UnitMeasureName { get; set; }
    }

    public class MedViewModel
    {
        public List<MedCustomViewModel> List { get; set; } = new List<MedCustomViewModel>();

        public MedCustomViewModel MedCustomViewModel { get; set; }

        public SelectList LaboratoryList { get; set; }
        
        public SelectList CommodityGroupsList { get; set; }
        
        public SelectList UnitMeasuresList { get; set; }

        public FilterViewModel FilterViewModel { get; set; }

        public DonatProcedure Donat { get; set; }  
    }

    public class FilterViewModel
    {

        [DisplayName("Мінімальний внесок")]
        public decimal MinExpenses { get; set; }

        [DisplayName("Максимальний внесок")]
        public decimal MaxExpenses { get; set; }

        [DisplayName("Назва")]
        public string Name { get; set; }        
    }

    public class DonatProcedure
    {
        [DisplayName("Максимальний внесок")]
        public decimal Expenses { get; set; }

        public string Name { get; set; }
    }
}
