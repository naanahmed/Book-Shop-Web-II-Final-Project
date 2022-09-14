using Book_Shop.Areas;
using Book_Shop.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace Areas.Customer.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(ApplicationDbContext db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            var objProductList = _db.Products.ToList();
            return View(objProductList);
        }
        public IActionResult Details(int Id)
        {
            ShoppingCart cartObject = new()
            {
                Count = 1,
                productById = _db.Products.SingleOrDefault(p => p.Id == Id),
            };
            return View(cartObject);

        }

    }
}

