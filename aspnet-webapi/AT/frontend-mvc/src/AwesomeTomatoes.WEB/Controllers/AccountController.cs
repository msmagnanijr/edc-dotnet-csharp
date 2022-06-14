using AwesomeTomatoes.WEB.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;


namespace AwesomeTomatoes.WEB.Controllers;

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
    public async Task<IActionResult> Login(Login loginModel)
    {
        if (ModelState.IsValid)
        {
            var loginJson = JsonConvert.SerializeObject(loginModel);
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Authenticate/login", data);

            var loginReponse = await response.Content.ReadFromJsonAsync<LoginReponse>();

            if (response.IsSuccessStatusCode)
            {
                HttpContext.Session.SetString("token", loginReponse.Token);

                return Redirect("/Home/Index");
            }
            else 
            {
                //ViewBag.Message = response.StatusCode;
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
    public async Task<IActionResult> Register(Register register)
    {
        if (ModelState.IsValid)
        {
            var loginJson = JsonConvert.SerializeObject(register);
            var data = new StringContent(loginJson, Encoding.UTF8, "application/json");
            var response = await _apiHttpClient.PostAsync(HttpContext, "/api/Authenticate/register", data);

            var loginReponse = await response.Content.ReadFromJsonAsync<LoginReponse>();

            if (response.IsSuccessStatusCode)
            {
                //HttpContext.Session.SetString("token", loginReponse.Token);

                return Redirect("/Account/Login");
            }
            else
            {
                //ViewBag.Message = response.StatusCode;
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
