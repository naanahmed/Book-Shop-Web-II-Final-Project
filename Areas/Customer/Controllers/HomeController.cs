using Book_Shop.Areas.Admin.Models;
using Book_Shop.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

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

        public async Task<IActionResult> SearchResults()
        {
            return View();
        }
        // POST: Searchresults
        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            return View("Index", await _db.Products.Where(j => j.Title.Contains(SearchPhrase)).ToListAsync());
        }

        public IActionResult Details(int productId)
        {
            ShoppingCart cartObject = new()
            {
                Count = 1,
                ProductId = productId,
                Product = _db.Products.FirstOrDefault(p => p.Id == productId),
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



            ShoppingCart cartFromDb = _db.ShoppingCarts.FirstOrDefault(
                u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId
                );
            if (cartFromDb == null)
            {
                _db.Add(shoppingCart);
            }
            else
            {
                int IncrementCount(ShoppingCart shoppingCart, int count)
                {
                    shoppingCart.Count += count;
                    return shoppingCart.Count;
                }
                int DecrementCount(ShoppingCart shoppingCart, int count)
                {
                    shoppingCart.Count -= count;
                    return shoppingCart.Count;
                }
                IncrementCount(cartFromDb, shoppingCart.Count);
            }
            _db.SaveChanges();
            TempData["Success"] = "Added to cart";
            return RedirectToAction("Index");

        }

    }
}

