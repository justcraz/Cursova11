using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaKursWork.Models;
using PharmaKursWork.Servises;
using PharmaKursWork.ViewModels.LaboratoryEmployeeViewModels;

namespace PharmaKursWork.Controllers
{
    public class LaboratoryEmployeeController : Controller
    {
        private readonly DataContext _db;
        private readonly UserServise _userServise;
        public LaboratoryEmployeeController(DataContext db, UserServise userService)
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

            var scientists = await (from s in _db.Scientists 
                                    join e in _db.LaboratoryEmployees on s.LaboratoryEmployeeId equals e.Id
                                    join l in _db.Laboratories on e.LabratoryId equals l.Id
                                    select new ScientistViewModel
                                    {
                                        Id = e.Id,
                                        FirstName= e.FirstName,
                                        LastName= e.LastName,
                                        PhoneNumber= e.PhoneNumber,
                                        Email= e.Email,
                                        ResponsibleForDevice = s.ResponsibleForDevice,
                                        Adress= e.Adress,
                                        DirectionDevelopment = s.DirectionDevelopment,
                                        LaboratoryName = l.Name
                                    }).ToListAsync();

            var techStaffs = await (from t in _db.TechStaffs
                                    join e in _db.LaboratoryEmployees on t.LaboratoryEmployeeId equals e.Id
                                    join l in _db.Laboratories on e.LabratoryId equals l.Id
                                    select new TechStaffViewModel
                                    {
                                        Id = e.Id,
                                        FirstName = e.FirstName,
                                        LastName = e.LastName,
                                        PhoneNumber = e.PhoneNumber,
                                        Email = e.Email,
                                        Adress = e.Adress,
                                        LaboratoryName = l.Name,
                                        HasYourInstruments = t.HasYourInstruments,
                                        MaintainsDevice = t.MaintainsDevice,
                                    }).ToListAsync();

            var labs = await (from l in _db.Laboratories select l).ToListAsync();

            var model = new LaboratoryEmployeeViewModel()
            {
                ScientistList = scientists,
                TechStaffList = techStaffs,
                LaboratorySelectList = new SelectList(labs, "Id", "Name"),
            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> AddScientist()
        {

            var labs = await (from l in _db.Laboratories select l).ToListAsync();

            var model = new AddScientistViewModel()
            {
                LaboratorySelectList = new SelectList(labs, "Id", "Name"),
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddScientist(AddScientistViewModel model)
        {
            var scientist = model.ScientistModel;
            var employee = model.LaboratoryEmployeeModel;

            await _db.LaboratoryEmployees.AddAsync(employee);
            await _db.SaveChangesAsync();
            scientist.LaboratoryEmployeeId = employee.Id;
            await _db.Scientists.AddAsync(scientist);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
            
        [HttpGet]
        public async Task<IActionResult> EditScientist(int id)
        {
            var scientist = (from s in _db.Scientists where id == s.LaboratoryEmployeeId select s).FirstOrDefault();
            var employee = (from e in _db.LaboratoryEmployees where id == e.Id select e).FirstOrDefault();
            if (scientist == default || employee == default)
                return View("Index");

            var labs = await (from l in _db.Laboratories select l).ToListAsync();

            var model = new EditScientistViewModel
            {
                LaboratorySelectList = new SelectList(labs, "Id", "Name"),
                ScientistModel = scientist,
                LaboratoryEmployeeModel = employee,
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditScientist(EditScientistViewModel model)
        {
            var scientistData = model.ScientistModel;
            var employeeData = model.LaboratoryEmployeeModel;

            var scientist = (from s in _db.Scientists where employeeData.Id == s.LaboratoryEmployeeId select s).FirstOrDefault();
            var employee = (from e in _db.LaboratoryEmployees where employeeData.Id == e.Id select e).FirstOrDefault();
                
            if (scientist == default || employee == default)
                return View("Index");

            scientist.DirectionDevelopment = scientistData.DirectionDevelopment;
            scientist.ResponsibleForDevice = scientistData.ResponsibleForDevice;

            employee.FirstName = employeeData.FirstName;
            employee.LastName = employeeData.LastName;
            employee.PhoneNumber = employeeData.PhoneNumber;
            employee.Adress = employeeData.Adress;
            employee.Email = employeeData.Email;
            employee.LabratoryId = employeeData.LabratoryId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> AddTechStaff()
        {

            var labs = await (from l in _db.Laboratories select l).ToListAsync();

            var model = new AddTechStaffViewModel()
            {
                LaboratorySelectList = new SelectList(labs, "Id", "Name"),
            };

            return PartialView(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddTechStaff(AddTechStaffViewModel model)
        {
            var techStaff = model.TechStaffModel;
            var emp = model.LaboratoryEmployeeModel;

            await _db.LaboratoryEmployees.AddAsync(emp);
            await _db.SaveChangesAsync();
            techStaff.LaboratoryEmployeeId = emp.Id;
            await _db.TechStaffs.AddAsync(techStaff);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> EditTechStaff(int id)
        {
            var techStaff = (from t in _db.TechStaffs where id == t.LaboratoryEmployeeId select t).FirstOrDefault();
            var employee = (from e in _db.LaboratoryEmployees where id == e.Id select e).FirstOrDefault();

            if (employee == default || techStaff == default)
                return View("Index");

            var labs = await (from l in _db.Laboratories select l).ToListAsync();

            var model = new EditTechStaffViewModel
            {
                LaboratorySelectList = new SelectList(labs, "Id", "Name"),
                TechStaffModel = techStaff,
                LaboratoryEmployeeModel = employee,
            };

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditTechStaff(EditTechStaffViewModel model)
        {
            var techStaffData = model.TechStaffModel;
            var employeeData = model.LaboratoryEmployeeModel;

            var techStaff = (from t in _db.TechStaffs where employeeData.Id == t.LaboratoryEmployeeId select t).FirstOrDefault();
            var employee = (from e in _db.LaboratoryEmployees where employeeData.Id == e.Id select e).FirstOrDefault();

            if (techStaff == default || employee == default)
                return View("Index");

            techStaff.MaintainsDevice = techStaffData.MaintainsDevice;
            techStaff.HasYourInstruments = techStaffData.HasYourInstruments;

            employee.FirstName = employeeData.FirstName;
            employee.LastName = employeeData.LastName;
            employee.PhoneNumber = employeeData.PhoneNumber;
            employee.Adress = employeeData.Adress;
            employee.Email = employeeData.Email;
            employee.LabratoryId = employeeData.LabratoryId;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteScientist(int id)
        {
            var scientist = (from c in _db.Scientists where c.LaboratoryEmployeeId == id select c).FirstOrDefault();
            var employee = (from e in _db.LaboratoryEmployees where e.Id == id select e).FirstOrDefault();

            if (scientist == default || employee == default)
                return View("Index");
            var challenge = _db.Challenges.FirstOrDefault(u => u.ScientistId == scientist.LaboratoryEmployeeId);
            if (challenge != default)
                return RedirectToAction("Index");

            _db.Scientists.Remove(scientist);
            _db.LaboratoryEmployees.Remove(employee);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteTechStaff(int id)
        {
            var techStaff = (from c in _db.TechStaffs where c.LaboratoryEmployeeId == id select c).First();
            var employee = (from e in _db.LaboratoryEmployees where e.Id == id select e).First();

            if (techStaff == default || employee == default)
                return View("Index");

            var challenge = _db.Challenges.FirstOrDefault(u => u.TechStaffId == techStaff.LaboratoryEmployeeId);
            if (challenge != default)
                return RedirectToAction("Index");

            _db.TechStaffs.Remove(techStaff);
            _db.LaboratoryEmployees.Remove(employee);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
