using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.DATA.EF;
using StoreFront.UI.MVC.Models;


namespace StoreFront.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //local version of the shopping cart from the global version
            var shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //if the value is null or count is zero
            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, CartItemViewModel>();
                ViewBag.Message = "There are no items in your cart.";
            }
            //if items in cart set message to null
            else
            {
                ViewBag.Message = null;
            }

            //return shopping cart to view
            return View(shoppingCart);
        }

        public ActionResult UpdateCart(int productId, int qty, string size)
        {
            if (qty == 0)
            {
                RemoveFromCart(productId);
                return RedirectToAction("Index");
            }

            //retrive the cart from session and assign to local dictionary
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //update the qty in the local storage
            shoppingCart[productId].Qty = qty;

            //return the local cart to session
            Session["cart"] = shoppingCart;

            //display no items inm cart message if they update the last item in the cart to 0
            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no items in your cart";
            }

            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //cart out of sesion into local storage
            Dictionary<int, CartItemViewModel> shoppingCart = (Dictionary<int, CartItemViewModel>)Session["cart"];

            //remove the item
            shoppingCart.Remove(id);

            return RedirectToAction("Index");
        }
    }
}