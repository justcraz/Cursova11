using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.ChallengerViewModels;
using PharmaKursWork.ViewModels.ChallengeViewModels;

namespace PharmaKursWork.Controllers
{
    public class ChallengerController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public ChallengerController(DataContext db, UserServise userService)
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

            var challengers = (from c in _db.Challengers
                               join ch in _db.Challenges on c.ChallengeId equals ch.Id into ps_add
                               from sub in ps_add.DefaultIfEmpty()
                               select new ChallengerModel
                               {
                                   Id = c.Id,
                                   FirstName = c.FirstName,
                                   LastName = c.LastName,
                                   Contraindications = c.Contraindications,
                                   ChallengeName = sub.Name,
                               }).ToList();

            var challenges = (from c in _db.Challenges
                              select c);

            var model = new ChallengerViewModel
            {
                List = challengers,
                ChallengeSelectList = new SelectList(challenges, "Id", "Name"),
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult AddChallenger()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddChallenger(ChallengerViewModel model)
        {
            var data = model.ChallengerModel;

            var challenger = new Challenger
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Contraindications = data.Contraindications,
            };

            await _db.Challengers.AddAsync(challenger);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> EditChallenger(int id)
        {
            var challenges = (from c in _db.Challenges
                              select c);

            var data = (from c in _db.Challengers
                        where c.Id == id
                        select c).FirstOrDefault();

            var cmodel = new ChallengerModel
            {
                Id = data.Id,
                FirstName = data.FirstName,
                LastName = data.LastName,
                Contraindications = data.Contraindications,
                ChallengeId = data.ChallengeId,
            };

            var model = new ChallengerViewModel
            {
                ChallengerModel = cmodel,
                ChallengeSelectList = new SelectList(challenges, "Id", "Name"),
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> EditChallenger(ChallengerViewModel model)
        {
            var challenger = (from c in _db.Challengers
                              where model.ChallengerModel.Id == c.Id
                              select c).FirstOrDefault();

            var data = model.ChallengerModel;

            challenger.Id = data.Id;
            challenger.FirstName = data.FirstName;
            challenger.LastName = data.LastName;
            challenger.Contraindications = data.Contraindications;
            challenger.ChallengeId = data.ChallengeId;

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var challenge = (from c in _db.Challengers where c.Id == id select c).FirstOrDefault();
            if (challenge == default)
                return View("Index");

            _db.Challengers.Remove(challenge);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
