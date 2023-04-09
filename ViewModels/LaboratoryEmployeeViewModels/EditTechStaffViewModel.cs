using Microsoft.AspNetCore.Mvc.Rendering;
using PharmaKursWork.Models;

namespace PharmaKursWork.ViewModels.LaboratoryEmployeeViewModels
{
    public class EditTechStaffViewModel
    {
        public LaboratoryEmployee LaboratoryEmployeeModel { get; set; }
        public TechStaff TechStaffModel { get; set; }
        public SelectList LaboratorySelectList { get; set; }
    }
}
