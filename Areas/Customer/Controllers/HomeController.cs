using Book_Shop.Areas.Admin.Models;
using Book_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;

namespace Areas.Customer.Controllers
{
    [Area("Customer")]
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
        public IActionResult Details(int productId)
        {
            ShoppingCart cartObject = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _db.Products.SingleOrDefault(p => p.Id == productId),
            };
            return View(cartObject);

        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCart.ApplicationUserId = claim.Value;

            _db.ShoppingCarts.Add(shoppingCart);
            _db.SaveChangesAsync();
            TempData["Success"] = "Added to cart";
            return RedirectToAction("Index");

        }

    }
}

