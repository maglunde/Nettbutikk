using Newtonsoft.Json;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Product;

namespace Nettbutikk.Controllers
{
    public class CartController : Controller
    {
        private const string SHOPPINGCART = "Shoppingcart";

        private IProductLogic _productBLL;

        public CartController()
        {
            _productBLL = new ProductBLL();
        }

        public CartController(IProductLogic stub)
        {
            _productBLL = stub;
        }

        public ActionResult Cart(string ReturnUrl)
        {
            ViewBag.ReturnUrl = ReturnUrl;
            ViewBag.ShoppingCart = GetCartList();
            ViewBag.LoggedIn = Session["LoggedIn"] ?? false;
            return View("Shoppingcart");
        }

        [HttpPost]
        public int AddToCart(int ProductId)
        {
            var ch = new CookieHandler();
            return ch.AddToCart(ProductId);
        }
        public ActionResult EmptyCart(string returnUrl)
        {
            var ch = new CookieHandler();
            ch.EmptyCart();

            return RedirectToAction("Cart",new { returnUrl = returnUrl });
        }
        public string GetCart()
        {
            var cart = GetCartList();

            var jsonCart = JsonConvert.SerializeObject(cart);
            return jsonCart;
        }
        public List<CartItem> GetCartList()
        {
            var ch = new CookieHandler();
            var productIdList = ch.GetCartProductIds();
            var productModelList = _productBLL.GetProducts(productIdList);

            var cartItemList = productModelList.Select(p => new CartItem()
            {
                ProductId = p.ProductId,
                Name = p.ProductName,
                Count = ch.GetCount(p.ProductId),
                Price = p.Price
            }).ToList();

            return cartItemList;
        }
        public double GetSumTotalCart()
        {
            var sumTotal = 0.0;
            var cart = GetCartList();

            foreach (var item in cart)
            {
                sumTotal += item.Price * item.Count;
            }

            return sumTotal;
        }
        public int NumItemsInCart()
        {
            var ch = new CookieHandler();
            return ch.NumItemsInCart();
        }
        [HttpPost]
        public int RemoveFromCart(int ProductId)
        {
            var ch = new CookieHandler();
            return ch.RemoveFromCart(ProductId);
        }
        public int UpdateCartProductCount(int ProductId, int Count)
        {
            var ch = new CookieHandler();
            return ch.UpdateCartProductCount(ProductId, Count);
        }
    }
}