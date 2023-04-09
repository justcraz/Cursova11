using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class Med
    {
        public int Id { get; set; }
        
        public string Name { get; set; }

        public DateTime StartExploring { get; set; }
        
        public decimal Expenses { get; set; }
        public string? Contraindications { get; set; }

        public int LabratoryId { get; set; }
        [ForeignKey(nameof(LabratoryId))]
        public Laboratory Laboratory { get; set; }

        [ForeignKey(nameof(CommodityGroupId))]
        public int CommodityGroupId { get; set; }
        public CommodityGroup CommodityGroup { get; set; }

        [ForeignKey(nameof(UnitMeasureId))]
        public int UnitMeasureId { get; set; }
        public UnitMeasure UnitMeasure { get; set; }
    }
}
