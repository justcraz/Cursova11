using Microsoft.AspNetCore.Mvc;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.CommodityGroupModels;
using PharmaKursWork.ViewModels.UnitViewModels;

namespace PharmaKursWork.Controllers
{
    public class CommodityGroupController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public CommodityGroupController(DataContext db, UserServise userService)
        {
            _db = db;
            _userServise = userService;
        }
        [HttpGet]
        public IActionResult Index(CommodityGroupViewModel viewModel)
        {
            var user = _userServise.getCurrentUser();

            var filter = viewModel.FilterViewModel;

            if (user == null || user.Username == "Guest")
            {
                return RedirectToAction("Index", "Account");
            }

            var cmg = _db.CommodityGroups.AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    cmg = cmg.Where(c => c.Name.Contains(filter.Name));
                }
            }

            var model = new CommodityGroupViewModel
            {
                List = cmg.ToList(),
            };

            return View(model);
        }
        [HttpGet]
        public IActionResult AddCommodityGroup()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> AddCommodityGroup(AddCommodityGroupViewModel commData)
        {
            var comm = new CommodityGroup
            {
                Name = commData.Name,
                Desc = commData.Desc,
            };

            await _db.CommodityGroups.AddAsync(comm);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCommodityGroup(int id)
        {
            var comm = _db.CommodityGroups.FirstOrDefault(u => u.Id == id);

            if (comm == default)
                return View("Index");
            var model = new EditCommodityGroupViewModel
            {
                Id = comm.Id,
                Name = comm.Name,
                Desc = comm.Desc,
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditCommodityGroup(EditCommodityGroupViewModel commData)
        {
            var comm = _db.CommodityGroups.First(u => u.Id == commData.Id);
            comm.Name = commData.Name;
            comm.Desc = commData.Desc;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var comm = _db.CommodityGroups.FirstOrDefault(u => u.Id == id);

            if (comm == default)
                return View();

            _db.CommodityGroups.Remove(comm);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
