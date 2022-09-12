using Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;

namespace Book_Shop.Areas
{
    public class ShoppingCart
    {
        public Product productById { get; set; }

        [Range(1, 50, ErrorMessage = "Please enter a value between 1 and 50")]
        public int Count { get; set; }

    }
}
