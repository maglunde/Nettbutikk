using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk
{
    public class CookieHandler
    {
        private const string SHOPPINGCART = "Shoppingcart";

        private HttpContext _context;
        private HttpCookie _cookie;

        public CookieHandler()
        {
            _context = HttpContext.Current;
            _cookie = _context.Request.Cookies[SHOPPINGCART] ?? new HttpCookie(SHOPPINGCART);
        }

        public int AddToCart(int ProductId)
        {
            int numProduct;
            try
            {
                numProduct = Convert.ToInt32(_cookie[ProductId.ToString()]);
                numProduct++;
            }
            catch (Exception)
            {
                numProduct = 1;
            }
            _cookie[ProductId.ToString()] = numProduct.ToString();
            _context.Response.Cookies.Add(_cookie);

            return NumItemsInCart();
        }
        public bool EmptyCart()
        {
            _cookie.Expires = DateTime.Now.AddDays(-1d);
            _cookie = new HttpCookie(SHOPPINGCART);
            _context.Response.Cookies.Add(_cookie);

            return NumItemsInCart() == 0;
        }
        public List<int> GetCartProductIds()
        {
            var productIdList = new List<int>();
            var cookieValueList = _cookie.Values;

            foreach (var pid in cookieValueList)
            {
                var productId = Convert.ToInt32(pid);
                try
                {
                    var count = Convert.ToInt32(_cookie[pid.ToString()]);
                    if (count > 0)
                        productIdList.Add(productId);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return productIdList;
        }
        public int GetCount(int ProductId)
        {
            int count;
            try
            {
                count = Convert.ToInt32(_cookie[ProductId.ToString()]);
            }
            catch (Exception)
            {
                count = 0;
            }
            return count;
        }
        public int NumItemsInCart()
        {
            if (_cookie == null)
                return 0;

            var list = _cookie.Values;
            var numItemsInCart = 0;
            foreach (var c in list)
            {
                try
                {
                    var count = Convert.ToInt32(_cookie[c.ToString()]);
                    numItemsInCart += count;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return numItemsInCart;
        }
        public int RemoveFromCart(int ProductId)
        {
            _cookie.Values[ProductId.ToString()] = null;
            _context.Response.AppendCookie(_cookie);

            return NumItemsInCart();
        }
        public int UpdateCartProductCount(int ProductId, int Count)
        {
            _cookie[ProductId.ToString()] = Count.ToString();
            _context.Response.AppendCookie(_cookie);

            return NumItemsInCart();

        }
    }
}