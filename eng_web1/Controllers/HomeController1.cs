using Microsoft.AspNetCore.Mvc;

namespace eng_web1.Controllers
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
