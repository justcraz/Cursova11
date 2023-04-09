using System.ComponentModel.DataAnnotations;

namespace PharmaKursWork.ViewModels.AccountViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Вкажіть ім'я")]
        public string Username { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Вкажіть пароль")]
        public string Password { get; set; } = string.Empty;

    }
}
