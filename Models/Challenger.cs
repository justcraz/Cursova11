using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.Models
{
    public class Challenger
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Contraindications { get; set; }
        
        [ForeignKey(nameof(Id))]
        public User User { get; set; }
        public int? ChallengeId { get; set; }
        
        [ForeignKey(nameof(ChallengeId))]
        public Challenge? Challenge { get; set; }
    }
}
