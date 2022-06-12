using Domain.Model.Entities;
using Domain.Model.Entities.Infra;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace WebMvc;

[AllowAnonymous]
public class AccountController : Controller
{
    private readonly ApiHttpClient _apiHttpClient;
    public AccountController(ApiHttpClient apiHttpClient)
    {
        _apiHttpClient = apiHttpClient;
    }
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost, ActionName("Login")]
    public async Task<IActionResult> Login(LoginEntity login)
    {
        if (ModelState.IsValid)
        {
            var loginJson = JsonConvert.SerializeObject(login);
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Authenticate/login", data);

            var loginReponse = await response.Content.ReadFromJsonAsync<LoginReponse>();

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("token", loginReponse.Token);
                HttpContext.Session.SetString("username", login.Username);
                return Redirect("/Home/Index");
            }
            else 
            {
                ViewBag.Message = "Login inválido!";
            }
        }
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }


    [HttpPost, ActionName("Register")]
    public async Task<IActionResult> Register(RegisterEntity register)
    {
        if (ModelState.IsValid)
        {
            var loginJson = JsonConvert.SerializeObject(register);
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Authenticate/register", data);

            var loginReponse = await response.Content.ReadFromJsonAsync<LoginReponse>();

            if (response.IsSuccessStatusCode)
            {

                ViewBag.Message = "Usuário registrado com sucesso!";
                return Redirect("/Account/Login");
            }
            else
            {
                ViewBag.Message = "Algum erro ocorreu durante a criação do usuário!";
                return View();
            }
        }
        return View();
    }
    public async Task<IActionResult> Logout() 
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login", "Account");
    }
}
