namespace BlueBasher.WeatherStation.Web.Controllers
{
    using Microsoft.AspNet.Mvc;
    using Microsoft.Extensions.Configuration;
    public class HomeController : Controller
    {
        private readonly IConfigurationRoot _configuration;

        public HomeController(IConfigurationRoot configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ViewData["bingCredentials"] = _configuration["BingMaps:Credentials"];
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
