using Microsoft.AspNetCore.Mvc;
using PharmaKursWork.Models;
using PharmaKursWork.ViewModels.AccountViewModels;

namespace PharmaKursWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _db;
        public AccountController(DataContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        public IActionResult Index(LoginViewModel loginData) 
        {
            var user = _db.Users.FirstOrDefault(user => user.Username == loginData.Username);
            if (user == default)
            {
                ModelState.AddModelError("Username", "Користувача немає");
            }

            if (loginData.Password != user?.Password)
            { 
                ModelState.AddModelError("Password", "Паролі не співпадають");
            }

            if (!ModelState.IsValid)
            {
                return View(loginData);
            }
            Response.Cookies.Append("authenticationKey", user.authenticationKey.ToString());
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerData) 
        {
            var username = _db.Users.FirstOrDefault(user => user.Username == registerData.Username);

            if (username != default)
            {
                ModelState.AddModelError("Username", "Таке ім'я вже існує");
            }

            if (!ModelState.IsValid)
            {
                return View(registerData);
            }

            var authenticationKey = Guid.NewGuid();
            Response.Cookies.Append("authenticationKey", authenticationKey.ToString());

            var user = new User
            {
                Username = registerData.Username,
                Password = registerData.Password,
                authenticationKey = authenticationKey,
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("authenticationKey");
            return RedirectToAction("Index", "Home");
        }
    }
}
