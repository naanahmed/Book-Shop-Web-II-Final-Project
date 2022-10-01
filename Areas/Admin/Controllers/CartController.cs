using Microsoft.AspNetCore.Mvc;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
