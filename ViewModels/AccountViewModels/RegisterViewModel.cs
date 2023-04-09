using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Вкажіть ім'я")]
        [MinLength(3, ErrorMessage = "Ім'я повинно мати довжину понад 3 символи")]
        [MaxLength(20, ErrorMessage = "Ім'я повинно мати довжину менше 20 символів")]
        public string Username { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        [MinLength(6, ErrorMessage = "Пароль повинен мати довжину понад 6 символів")]
        public string Password { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Підтвердіть пароль")]
        [Compare("Password", ErrorMessage = "Паролі не співпадають")]
        public string ConfirmPassword { get; set; } = string.Empty;

    }
}
