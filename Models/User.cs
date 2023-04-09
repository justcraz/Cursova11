using Microsoft.AspNetCore.Identity;

namespace PharmaKursWork.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; } = false;
        public Guid authenticationKey { get; set; }
    }
}
