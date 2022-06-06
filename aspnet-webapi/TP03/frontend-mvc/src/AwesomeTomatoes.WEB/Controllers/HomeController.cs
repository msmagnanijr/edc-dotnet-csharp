namespace AwesomeTomatoes.WEB.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApiHttpClient _apiHttpClient;

    public HomeController(ILogger<HomeController> logger, ApiHttpClient apiHttpClient)
    {
        _logger = logger;
        _apiHttpClient = apiHttpClient;
    }

    public IActionResult Index()
    {
        if (_apiHttpClient.IsTokenInSession(HttpContext))
        {
            return RedirectToAction("Index", "Movies");
        }
        else
        {
            return RedirectToAction("Login", "Account");
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}