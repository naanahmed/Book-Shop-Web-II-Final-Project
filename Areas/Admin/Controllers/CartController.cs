using Book_Shop.Areas.Admin.Models;
using Book_Shop.Data;
using Book_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public int OrderTotal { get; set; }

        public CartController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);


            ShoppingCartVM ShoppingCartVM = new ShoppingCartVM()
            {
                ListCart = _db.ShoppingCarts.Include(u => u.Product)
            };
            foreach (var cart in ShoppingCartVM.ListCart)
            {
                ShoppingCartVM.CartTotal += (cart.Product.ListPrice * cart.Count);

            }

            return View(ShoppingCartVM);
        }
        public IActionResult Summary()
        {

            return View();
        }

        public IActionResult OrderConfirmation(int id)
        {

            return View();
        }
        public IActionResult Plus(int cartId)
        {
            int IncrementCount(ShoppingCart shoppingCart, int count)
            {
                shoppingCart.Count += count;
                return shoppingCart.Count;
            }
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            IncrementCount(cart, 1);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Minus(int cartId)
        {
            int DecrementCount(ShoppingCart shoppingCart, int count)
            {
                shoppingCart.Count -= count;
                return shoppingCart.Count;
            }
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            if (cart.Count <= 1)
            {
                _db.ShoppingCarts.Remove(cart);
            }
            else
            {
                DecrementCount(cart, 1);
            }
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _db.ShoppingCarts.FirstOrDefault(u => u.Id == cartId);
            _db.ShoppingCarts.Remove(cart);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }




}



