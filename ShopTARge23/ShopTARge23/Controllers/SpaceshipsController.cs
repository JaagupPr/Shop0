using Microsoft.AspNetCore.Mvc;

namespace ShopTARge23.Controllers
{
    public class SpaceshipsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
