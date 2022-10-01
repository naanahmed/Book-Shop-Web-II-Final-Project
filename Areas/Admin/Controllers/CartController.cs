using Book_Shop.Data;
using Book_Shop.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Book_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ShoppingCartVM ShoppingCartVM { get; set; }

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

            return View(ShoppingCartVM);
        }

    }




}



