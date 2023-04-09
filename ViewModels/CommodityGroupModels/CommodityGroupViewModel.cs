using PharmaKursWork.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PharmaKursWork.ViewModels.CommodityGroupModels
{
    public class CommodityGroupViewModel
    {
        public CommodityGroupCustomModel CommodityGroupCustomModel { get; set; }
        public FilterViewModel FilterViewModel { get; set; }
        public List<CommodityGroup> List { get; set; }
    }

    public class CommodityGroupCustomModel
    {
        public int Id { get; set; }
        
        [DisplayName("Назва товарної групи")]
        [Required(ErrorMessage = "Назва товарної групи")]
        public string Name { get; set; }
        
        [DisplayName("Опис")]
        [Required(ErrorMessage = "Вкажіть опис")]
        public string Desc { get; set; }
    }

    public class FilterViewModel
    {
        [DisplayName("Назва")]
        public string Name { get; set; }
    }
}
