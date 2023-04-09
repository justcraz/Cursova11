using Microsoft.AspNetCore.Mvc;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.UnitViewModels;

namespace PharmaKursWork.Controllers
{
    public class UnitController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public UnitController(DataContext db, UserServise userService)
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

            var model = new UnitViewModel
            {
                List = _db.UnitMeasures.ToList(),
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult AddUnit()
        {
            return PartialView();
        }

        public async Task<IActionResult> AddUnit(AddUnitModalViewModel unitData)
        {
            var unit = new UnitMeasure
            {
                Name = unitData.Name,
                Desc = unitData.Desc,
            };

            await _db.UnitMeasures.AddAsync(unit);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditUnit(int id)
        {
            var unit = _db.UnitMeasures.FirstOrDefault(u => u.Id == id);
            if (unit == default)
                return View("Index");
            var model = new EditUnitModalViewModel
            {
                Id = unit.Id,
                Name = unit.Name,
                Desc = unit.Desc,
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditUnit(EditUnitModalViewModel unitData)
        {
            var unit = _db.UnitMeasures.First(u => u.Id == unitData.Id);
            if (unit == default)
                return View("Index");
            unit.Name = unitData.Name;
            unit.Desc = unitData.Desc;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var unit = _db.UnitMeasures.FirstOrDefault(u => u.Id == id);
            if (unit == default)
                return View("Index");

            _db.UnitMeasures.Remove(unit);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


    }
}
