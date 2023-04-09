using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class Challenge
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime ChallegesStart { get; set; }

        public int ScientistId { get; set; }
        [ForeignKey(nameof(ScientistId))]
        public Scientist Scientist { get; set; }
        
        public int MedsId { get; set; }
        [ForeignKey(nameof(MedsId))]
        public Med Med { get; set; }

        public int TechStaffId { get; set; }

        [ForeignKey(nameof(TechStaffId))]
        public TechStaff TechStaff{ get; set; }

    }
}
