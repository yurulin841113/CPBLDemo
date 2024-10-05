using Microsoft.AspNetCore.Mvc;

namespace CPBLDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
