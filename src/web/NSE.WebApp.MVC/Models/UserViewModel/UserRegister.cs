using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using NSE.WebApp.MVC.Extensions;

namespace NSE.WebApp.MVC.Models.UserViewModel
{
    public class UserRegister
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Name Completo")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("CPF")]
        [Cpf]
        public string Cpf { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido")]
        public string Email { get; set; }

        [DisplayName("Senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 6)]
        public string Password { get; set; }

        [DisplayName("Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem")]
        public string PasswordConfirmation { get; set; }
    }
}
