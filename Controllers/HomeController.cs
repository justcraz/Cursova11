using Microsoft.AspNetCore.Mvc;
using PharmaKursWork.Servises;

namespace PharmaKursWork.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public HomeController(DataContext db, UserServise userService)
        {
            _db = db;
            _userServise = userService;
        }
        public async Task<IActionResult> Index()
        {
            var user = _userServise.getCurrentUser();
            

            if (user == null || user.Username == "Guest")
            {
                return RedirectToAction("Index", "Account");
            }

            return View();
        }
    }
}
