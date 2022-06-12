using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities;

public class LoginEntity
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [Display(Name = "Usuário")]
    public string Username { get; set; }

    [Required(ErrorMessage = "Password é obrigatório")]
    [Display(Name = "Senha")]
    public string Password { get; set; }
}
