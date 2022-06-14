﻿using System.ComponentModel.DataAnnotations;

namespace AwesomeTomatoes.WEB.Models.Account;

public class Login
{
    [Display(Name = "Usuário")]
    [Required(ErrorMessage = "User Name is required")]
    public string Username { get; set; }

    [Display(Name = "Senha")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }
}