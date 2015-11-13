using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Order;
using BLL.Product;
using BLL.Account;

namespace Nettbutikk.Controllers
{
    public class CheckoutController : Controller
    {
        private IOrderLogic _orderBLL;

        public CheckoutController()
        {
            _orderBLL = new OrderBLL();
        }

        public CheckoutController(IOrderLogic stub)
        {
            _orderBLL = stub;
        }

        public ActionResult Checkout()
        {
            if (Session["LoggedIn"] != null)
            {
                if ((bool)Session["LoggedIn"])
                {
                    var productBLL = new ProductBLL();
                    var ch = new CookieHandler();

                    var Email = (string)Session["Email"];
                    var pidList = ch.GetCartProductIds();
                    var productModelList = productBLL.GetProducts(pidList);

                    var cart = productModelList.Select(p => new CartItem()
                    {
                        ProductId = p.ProductId,
                        Name = p.ProductName,
                        Count = ch.GetCount(p.ProductId),
                        Price = p.Price
                    }).ToList();

                    var customerModel = new AccountBLL().GetCustomer(Email);
                    var customer = new CustomerView()
                    {
                        Firstname = customerModel.Firstname,
                        Lastname = customerModel.Lastname,
                        Address = customerModel.Address,
                        Zipcode = customerModel.Zipcode,
                        City = customerModel.City,
                        CustomerId = customerModel.CustomerId,
                        Email = customerModel.Email

                    };

                    ViewBag.Cart = cart;
                    ViewBag.Customer = customer;
                    ViewBag.LoggedIn = LoginStatus();
                    TempData["Message"] = "";
                    return View();
                }
            }
            TempData["Message"] = "Logg inn for å betale";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult PlaceOrder(string returnUrl)
        {
            if (Session["LoggedIn"] != null)
            {
                if ((bool)Session["LoggedIn"])

                {
                    var productBLL = new ProductBLL();
                    var ch = new CookieHandler();

                    var productIdList = ch.GetCartProductIds();
                    var productModelList = productBLL.GetProducts(productIdList);

                    var cart = productModelList.Select(p => new CartItem()
                    {
                        ProductId = p.ProductId,
                        Name = p.ProductName,
                        Count = ch.GetCount(p.ProductId),
                        Price = p.Price
                    }).ToList();
                    
                    var order = new OrderModel();
                    var orderlines = new List<OrderlineModel>();

                    foreach (var item in cart)
                    {
                        orderlines.Add(new OrderlineModel()
                        {
                            Count = item.Count,
                            ProductId = item.ProductId
                        });
                    }
                    order.Orderlines = orderlines;
                    order.CustomerId = new AccountBLL().GetCustomer((String)Session["Email"]).CustomerId;
                    order.Date = DateTime.Now;
                    var OrderId = _orderBLL.PlaceOrder(order);

                    if (OrderId > 0)
                    {
                        ch.EmptyCart();
                                               
                        ViewBag.LoggedIn = (bool)Session["LoggedIn"];
                        ViewBag.Reciept = GetReciept(OrderId);

                        return View("GetReciept");
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }
           

        public OrderView GetReciept(int OrderId)
        {
           
            OrderModel reciept = _orderBLL.GetReciept(OrderId);

            var orderlines = new List<OrderlineView>();
            foreach(var item in reciept.Orderlines)
            {
                var product = new ProductView()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.ProductPrice,

                };

                orderlines.Add(new OrderlineView()
                {
                    OrderlineId = item.OrderlineId,
                    Count = item.Count,
                    Product = product

                });
            }

            OrderView orderView = new OrderView()
            {
                OrderId = reciept.OrderId,
                Date = reciept.Date,
                Orderlines = orderlines,
            };

            return orderView ;
        }

        public bool LoginStatus()
        {
            bool LoggedIn = false;
            if (Session["LoggedIn"] != null)
            {
                LoggedIn = (bool)Session["LoggedIn"];
            }
            return LoggedIn;
        }

    }
}