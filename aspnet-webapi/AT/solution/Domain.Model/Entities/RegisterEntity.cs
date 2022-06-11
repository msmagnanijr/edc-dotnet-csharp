using System.ComponentModel.DataAnnotations;

namespace Domain.Model.Entities;

public class RegisterEntity
{
    [Required(ErrorMessage = "Username é obrigatório")]
    [Display(Name = "Usuário")]
    public string Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email é obrigatório")]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Password é obrigatório")]
    [Display(Name = "Senha")]
    public string Password { get; set; }
}
