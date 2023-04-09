using PharmaKursWork.Models;

namespace PharmaKursWork.Servises
{
    public class UserServise
    {
        private readonly DataContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserServise(DataContext db, IHttpContextAccessor httpContextAccessor)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public User getCurrentUser()
        {
            var cookie = _httpContextAccessor.HttpContext.Request.Cookies["authenticationKey"];

            var user = _db.Users.FirstOrDefault(u => u.authenticationKey.ToString() == cookie);
            if (user == default)
            {
                return new User()
                {
                    Username = "Guest",
                    isAdmin = false,
                };
            }
            return user;
        }

        public bool isCurrentUserAdmin()
        {
            var user = getCurrentUser();
            return user.isAdmin;
        }

        public bool isCurrentUserChallenger()
        {
            var user = getCurrentUser();
            var challenger = _db.Challengers.FirstOrDefault(u => u.Id == user.Id);

            if (challenger == default)
            {
                return false;
            }

            return true;
        }
    }
}
