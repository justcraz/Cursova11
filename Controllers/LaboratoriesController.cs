using Microsoft.AspNetCore.Mvc;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.LaboratoriesViewModels;

namespace PharmaKursWork.Controllers
{
    public class LaboratoriesController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public LaboratoriesController(DataContext db, UserServise userService)
        {
            _db = db;
            _userServise = userService;
        }

        public IActionResult Index()
        {
            var user = _userServise.getCurrentUser();

            if (user == null || user.Username == "Guest")
            {
                return RedirectToAction("Index", "Account");
            }
            var labs = (from l in _db.Laboratories select l).ToList();

            var model = new LaboratoriesViewModel
            {
                List = labs,
            };
            return View(model);
        }

        // инфа для изменения лабы

        [HttpGet]
        public IActionResult EditLab(int id)
        {
            var lab = (from l in _db.Laboratories where id == l.Id select l).FirstOrDefault();
            if (lab == default)
                return View("Index");

            var model = new EditLaboratoriesModalViewModel
            {
                Id = lab.Id,
                Name = lab.Name,
                Adress = lab.Adress,
            };
            return PartialView(model); ;
        }

        // изменение лабы

        [HttpPost]
        public async Task<IActionResult> EditLab(EditLaboratoriesModalViewModel labData)
        {
            var lab = (from l in _db.Laboratories where labData.Id == l.Id select l).First();
            if (lab == default)
                return View("Index");

            lab.Name = labData.Name;
            lab.Adress = labData.Adress;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddLab()
        {
            return PartialView();
        }

        // Создание новой лабы
        [HttpPost]
        public async Task<IActionResult> AddLab(AddLaboratoriesModalViewModel labData)
        {
            var lab = new Laboratory
            {
                Name = labData.Name,
                Adress = labData.Adress,
            };

            await _db.Laboratories.AddAsync(lab);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lab = (from c in _db.Laboratories where c.Id == id select c).FirstOrDefault();
            if (lab == default)
                return View("Index");

            _db.Laboratories.Remove(lab);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
