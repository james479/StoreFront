using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoreFront.DATA.EF;

namespace StoreFront.UI.MVC.Models
{
    public class CartItemViewModel
    {
        public int Qty { get; set; }
        public Product CartItem { get; set; }

        //contstructor
        public CartItemViewModel(int qty, Product cartItem)
        {
            Qty = qty;
            CartItem = cartItem;
        }
    }
}