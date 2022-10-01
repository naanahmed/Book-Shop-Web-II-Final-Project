using Book_Shop.Areas.Admin.Models;
using System.Collections.Generic;

namespace Book_Shop.Models
{
    public class ShoppingCartVM
    {
        public IEnumerable<ShoppingCart> ListCart { get; set; }

    }
}
