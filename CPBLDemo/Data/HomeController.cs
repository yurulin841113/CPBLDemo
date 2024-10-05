using Microsoft.AspNetCore.Mvc;

namespace CPBLDemo.Data
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
