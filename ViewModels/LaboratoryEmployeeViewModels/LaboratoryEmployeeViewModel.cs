using Microsoft.AspNetCore.Mvc.Rendering;
using PharmaKursWork.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PharmaKursWork.ViewModels.LaboratoryEmployeeViewModels
{
    public class LaboratoryEmployeeViewModel
    {
        public List<ScientistViewModel> ScientistList { get; set; }

        public ScientistViewModel ScientistViewModel{ get; set; }

        public List<TechStaffViewModel> TechStaffList { get; set; }

        public TechStaffViewModel TechStaffViewModel { get; set; }

        public SelectList LaboratorySelectList { get; set; }
    }

    public class ScientistViewModel
    {
        
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Вкажіть прізвище")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("День рождения")]
        [Required(ErrorMessage = "Вкажіть день народження")]
        public DateTime BirthDate { get; set; } 

        [DisplayName("Номер телефон")]
        [Required(ErrorMessage = "Вкажіть номер телефону")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Вкажіть email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Адреса")]
        [Required(ErrorMessage = "Вкажіть адреса")]
        public string Adress { get; set; } = string.Empty;

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву лабораторії")]
        public string LaboratoryName { get; set; } = string.Empty;

        [DisplayName("Працює за приладом")]
        [Required(ErrorMessage = "Вкажіть прилад")]
        public string ResponsibleForDevice { get; set; } = string.Empty;

        [DisplayName("Напрямок досліджень")]
        [Required(ErrorMessage = "Вкажіть адресу лабораторії")]
        public string DirectionDevelopment { get; set; } = string.Empty;
    }
    public class TechStaffViewModel
    {
        public int Id { get; set; }

        [DisplayName("Ім'я")]
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string FirstName { get; set; } = string.Empty;

        [DisplayName("Прізвище")]
        [Required(ErrorMessage = "Вкажіть прізвище")]
        public string LastName { get; set; } = string.Empty;

        [DisplayName("День рождения")]
        [Required(ErrorMessage = "Вкажіть день народження")]
        public DateTime BirthDate { get; set; } 

        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Вкажіть номер телефона")]
        public string PhoneNumber { get; set; } = string.Empty;

        [DisplayName("Email")]
        [Required(ErrorMessage = "Вкажіть email")]
        public string Email { get; set; } = string.Empty;

        [DisplayName("Адрес")]
        [Required(ErrorMessage = "Вкажіть адрес")]
        public string Adress { get; set; } = string.Empty;

        [DisplayName("Назва лабораторії")]
        [Required(ErrorMessage = "Вкажіть назву лабораторії")]
        public string LaboratoryName { get; set; } = string.Empty;

        [DisplayName("Обслуговує прилад")]
        [Required(ErrorMessage = "Вкажіть який прилад обслуговується")]
        public string MaintainsDevice { get; set; } = string.Empty;

        [DisplayName("Чи є свої інструменти")]
        [Required(ErrorMessage = "Вкажіть, чи є інструменти")]
        public bool HasYourInstruments { get; set; } 
    }

}
