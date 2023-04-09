using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.MedViewModels;
using System.Data;
using System.Xml.Linq;

namespace PharmaKursWork.Controllers
{
    public class MedController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public MedController(DataContext db, UserServise userService)
        {
            _db = db;
            _userServise = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(MedViewModel? medModel)
        {
            var user = _userServise.getCurrentUser();

            if (user == null || user.Username == "Guest")
            {
                return RedirectToAction("Index", "Account");
            }

            var filter = medModel.FilterViewModel;

            var labs = await _db.Laboratories.ToListAsync();
            var comGroups = await _db.CommodityGroups.ToListAsync();
            var unitMeasures = await _db.UnitMeasures.ToListAsync();
            var meds = (
                from m in _db.Meds
                join c in _db.CommodityGroups on m.CommodityGroupId equals c.Id
                join l in _db.Laboratories on m.LabratoryId equals l.Id
                join u in _db.UnitMeasures on m.UnitMeasureId equals u.Id
                select new MedCustomViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    StartExploring = DateTime.Now,
                    Expenses = m.Expenses,
                    LabratoryName = l.Name,
                    CommodityName = c.Name,
                    UnitMeasureName = u.Name,
                }
            ).AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    meds = meds.Where(m => m.Name.Contains(filter.Name));
                }

                if (filter.MinExpenses > 0 && filter.MaxExpenses > 0)
                {
                    meds = meds.Where(m => m.Expenses > filter.MinExpenses && m.Expenses < filter.MaxExpenses);
                }
                else if (filter.MinExpenses > 0)
                {
                    meds = meds.Where(m => m.Expenses > filter.MinExpenses);
                }
                else if (filter.MaxExpenses > 0)
                {
                    meds = meds.Where(m => m.Expenses < filter.MaxExpenses);
                }
            }

            var model = new MedViewModel
            {
                LaboratoryList = new SelectList(labs, "Id", "Name"),
                CommodityGroupsList = new SelectList(comGroups, "Id", "Name"),
                UnitMeasuresList = new SelectList(unitMeasures, "Id", "Name"),
                List = await meds.ToListAsync(),
            };

            return View(model);
        }

        // Зміна інфи 
        [HttpGet]
        public async Task<IActionResult> EditMed(int id)
        {
            var rawMed = await _db.Meds.FirstOrDefaultAsync(m => m.Id == id);

            if (rawMed == default)
                return View("Index");

            var labs = await _db.Laboratories.ToListAsync();
            var comGroups = await _db.CommodityGroups.ToListAsync();
            var unitMeasures = await _db.UnitMeasures.ToListAsync();

            var med = new EditMedModel
            {
                Id = rawMed.Id,
                Name = rawMed.Name,
                StartExploring = rawMed.StartExploring,
                Expenses = rawMed.Expenses,
                LabratoryId = rawMed.LabratoryId,
                CommodityGroupId = rawMed.CommodityGroupId,
                UnitMeasureId = rawMed.UnitMeasureId,
            };

            var model = new EditMedModalViewModel
            {
                LaboratoryList = new SelectList(labs, "Id", "Name"),
                CommodityGroupsList = new SelectList(comGroups, "Id", "Name"),
                UnitMeasuresList = new SelectList(unitMeasures, "Id", "Name"),
                EditMedModel = med,
            };

            return PartialView(model);
        }
        // Редагування ліків
        [HttpPost]
        public async Task<IActionResult> EditMed(EditMedModalViewModel rawEditData)
        {
            var editData = rawEditData.EditMedModel;

            var med = (from m in _db.Meds where editData.Id == m.Id select m).FirstOrDefault();

            if (med == default)
                return View("Index");

            med.Name = editData.Name;
            med.StartExploring = editData.StartExploring;
            med.Expenses = editData.Expenses;
            med.LabratoryId = editData.LabratoryId;
            med.CommodityGroupId = editData.CommodityGroupId;
            med.UnitMeasureId = editData.UnitMeasureId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddMed()
        {
            var labs = await _db.Laboratories.ToListAsync();
            var comGroups = await _db.CommodityGroups.ToListAsync();
            var unitMeasures = await _db.UnitMeasures.ToListAsync();

            var model = new AddMedModalViewModel
            {
                LaboratoryList = new SelectList(labs, "Id", "Name"),
                CommodityGroupsList = new SelectList(comGroups, "Id", "Name"),
                UnitMeasuresList = new SelectList(unitMeasures, "Id", "Name"),
            };
            return PartialView(model);
        }

        // додавання ліків до бази
        [HttpPost]
        public async Task<IActionResult> AddMed(AddMedModalViewModel rawMedData)
        {
            var medData = rawMedData.AddMedModal;
            var med = new Med
            {
                Name = medData.Name,
                StartExploring = DateTime.Now.Date,
                Expenses = medData.Expenses,
                LabratoryId = medData.LabratoryId,
                CommodityGroupId = medData.CommodityGroupId,
                UnitMeasureId = medData.UnitMeasureId,
            };

            await _db.Meds.AddAsync(med);
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteMed(int id)
        {
            var med = (from c in _db.Meds where c.Id == id select c).FirstOrDefault();
            if (med == default)
                return View("Index");
            _db.Meds.Remove(med);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> HandleDonat(DonatProcedure donat)
        {
            if (donat.Expenses <= 0)
            {
                return RedirectToAction("Index");
            }

            object[] params1 = 
            {
                new SqlParameter("@medName", donat.Name),
                new SqlParameter("@donate", donat.Expenses),
            };

            _db.Database.ExecuteSqlRaw("addDonate {0}, {1}", params1);

            return RedirectToAction("Index");
        }
    }
}
