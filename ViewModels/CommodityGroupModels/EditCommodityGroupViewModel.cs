using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PharmaKursWork.ViewModels.CommodityGroupModels
{
    public class EditCommodityGroupViewModel
    {
        public int Id { get; set; }

        [DisplayName("Назва товарної групи")]
        [Required(ErrorMessage = "Вкажіть назву товарної групи")]
        public string Name { get; set; }

        [DisplayName("Опис")]
        [Required(ErrorMessage = "Вкажіть опис")]
        public string Desc { get; set; }
    }
}
