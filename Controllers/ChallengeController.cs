using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.ChallengeViewModels;
using PharmaKursWork.ViewModels.MedViewModels;

namespace PharmaKursWork.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public ChallengeController(DataContext db, UserServise userService)
        {
            _db = db;
            _userServise = userService;
        }
        [HttpGet]
        public IActionResult Index(ChallengeViewModel? challengeModel)
        {
            var user = _userServise.getCurrentUser();


            if (user == null || user.Username == "Guest")
            {
                return RedirectToAction("Index", "Account");
            }

            var filter = challengeModel.FilterViewModel;

            var meds = (from m in _db.Meds select m);
            var scientist = (from s in _db.Scientists
                             join e in _db.LaboratoryEmployees on s.LaboratoryEmployeeId equals e.Id
                             select new { Name = e.FirstName, Id = e.Id });

            var techStaff = (from t in _db.TechStaffs
                             join e in _db.LaboratoryEmployees on t.LaboratoryEmployeeId equals e.Id
                             select new { Name = e.FirstName, Id = e.Id });

            var challenges = (from c in _db.Challenges
                              join m in _db.Meds on c.MedsId equals m.Id
                              join s in scientist on c.ScientistId equals s.Id
                              join t in techStaff on c.TechStaffId equals t.Id
                              select new ChallengeView()
                              {
                                  Id = c.Id,
                                  Name = c.Name,
                                  ChallegesStart = c.ChallegesStart,
                                  MedName = m.Name,
                                  ScientistName = s.Name,
                                  TechStaffName = t.Name,
                              });

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    challenges = challenges.Where(m => m.Name.Contains(filter.Name));
                }

                if (filter.MinChallegesStart != DateTime.MinValue)
                {
                    challenges = challenges.Where(m => m.ChallegesStart > filter.MinChallegesStart);
                }
                if (filter.MaxChallegesStart != DateTime.MinValue)
                {
                    challenges = challenges.Where(m => m.ChallegesStart < filter.MaxChallegesStart);
                }
            }

            var model = new ChallengeViewModel
            {
                List = challenges.ToList(),
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddChallenge()
        {
            var meds = await (from m in _db.Meds
                              select m).ToListAsync();
            var scientists = await (from s in _db.Scientists
                                    join e in _db.LaboratoryEmployees on s.LaboratoryEmployeeId equals e.Id
                                    select new { Id = s.LaboratoryEmployeeId, Name = e.FirstName }).ToListAsync();

            var techStaffs = await (from c in _db.TechStaffs
                                    join e in _db.LaboratoryEmployees on c.LaboratoryEmployeeId equals e.Id
                                    select new { Id = c.LaboratoryEmployeeId, Name = e.FirstName }).ToListAsync();

            var model = new AddChallengeViewModel
            {
                MedSelectList = new SelectList(meds, "Id", "Name"),
                ScientistSelectList = new SelectList(scientists, "Id", "Name"),
                TechStaffSelectList = new SelectList(techStaffs, "Id", "Name"),
            };

            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddChallenge(AddChallengeViewModel model)
        {
            var addData = model.AddChallengeModal;
            var challenge = new Challenge
            {
                Name = addData.Name,
                ChallegesStart = DateTime.Now,
                MedsId = addData.MedId,
                ScientistId = addData.ScientistId,
                TechStaffId = addData.TechStaffId,
            };

            await _db.Challenges.AddAsync(challenge);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditChallenge(int id)
        {
            var meds = await (from m in _db.Meds
                              select m).ToListAsync();
            var scientists = await (from s in _db.Scientists
                                    join e in _db.LaboratoryEmployees on s.LaboratoryEmployeeId equals e.Id
                                    select new { Id = s.LaboratoryEmployeeId, Name = e.FirstName }).ToListAsync();

            var techStaffs = await (from c in _db.TechStaffs
                                    join e in _db.LaboratoryEmployees on c.LaboratoryEmployeeId equals e.Id
                                    select new { Id = c.LaboratoryEmployeeId, Name = e.FirstName }).ToListAsync();


            var rawChallenge = (from c in _db.Challenges
                                where c.Id == id
                                select c).FirstOrDefault();
            if (rawChallenge == default)
                return View("Index");

            var challenge = new EditChallengeModal
            {
                Id = rawChallenge.Id,
                Name = rawChallenge.Name,
                MedId = rawChallenge.MedsId,
                ScientistId = rawChallenge.ScientistId,
                TechStaffId = rawChallenge.TechStaffId,
            };
            var model = new EditChallengeViewModel
            {
                EditChallengeModal = challenge,
                MedSelectList = new SelectList(meds, "Id", "Name"),
                ScientistSelectList = new SelectList(scientists, "Id", "Name"),
                TechStaffSelectList = new SelectList(techStaffs, "Id", "Name"),
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditChallenge(EditChallengeViewModel model)
        {
            var editData = model.EditChallengeModal;
            var challenge = (from c in _db.Challenges
                             where c.Id == editData.Id
                             select c).FirstOrDefault();
            if (challenge == default) 
            {
                return View("Index");
            }

            challenge.Name = editData.Name;
            challenge.ChallegesStart = editData.ChallegesStart;
            challenge.MedsId = editData.MedId;
            challenge.ScientistId = editData.ScientistId;
            challenge.TechStaffId = editData.TechStaffId;
            
            await _db.SaveChangesAsync();


            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var challenge = (from c in _db.Challenges where c.Id == id select c).FirstOrDefault();
            if (challenge == default)
                return View("Index");

            _db.Challenges.Remove(challenge);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
