using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Order;
using BLL.Account;

namespace Nettbutikk.Controllers
{
    public class OrderController : Controller
    {

        private IOrderLogic _orderBLL;

        public OrderController()
        {
            _orderBLL = new OrderBLL();
        }

        public OrderController(IOrderLogic stub)
        {
            _orderBLL = stub;

        }

        [ChildActionOnly]
        public PartialViewResult OrdersPartial(int CustomerId)
        {
            List<OrderModel> orderModels;
            if (CustomerId > 0)
                orderModels = _orderBLL.GetOrders(CustomerId);
            else
                orderModels = _orderBLL.GetAllOrders();

            var orderViews = new List<OrderView>();

            foreach (var o in orderModels)
            {
                var order = new OrderView();
                order.Date = o.Date;
                order.OrderId = o.OrderId;
                order.CustomerId = o.CustomerId;
                order.Orderlines = new List<OrderlineView>();

                foreach (var l in o.Orderlines)
                {
                    var orderline = new OrderlineView();
                    orderline.Count = l.Count;
                    orderline.OrderlineId = l.OrderlineId;
                    orderline.Product = new ProductView()
                    {
                        Price = l.ProductPrice,
                        ProductId = l.ProductId,
                        ProductName = l.ProductName
                    };

                    order.Orderlines.Add(orderline);
                }
                orderViews.Add(order);
            }

            var productModels = _orderBLL.GetAllProducts();
            var productViews = new List<ProductView>();

            foreach (var productModel in productModels)
            {
                var productview = new ProductView()
                {
                    ProductId = productModel.ProductId,
                    ProductName = productModel.ProductName,
                    Description = productModel.Description,
                    Price = productModel.Price,
                    Stock = productModel.Stock,
                    //ImageUrl = productModel.ImageUrl,
                    //CategoryName = productModel.CategoryName
                    CategoryId = productModel.CategoryId
                };
                productViews.Add(productview);
            }

            string Title = CustomerId == 0 ? "Ordreadministrasjon - Alle ordre" : (CustomerId > 0 ? "Ordreadministrasjon - Kunde": "Feil kunde id");

            ViewBag.Orders = orderViews;
            ViewBag.Products = productViews;
            ViewBag.Title = Title;

            return PartialView();
        }

        [HttpPost]
        public bool UpdateOrderline(int OrderlineId, int ProductId, int Count)
        {
            if ((Session["Admin"] == null ? false : (bool)Session["Admin"]))
            {
                var orderlineModel = new OrderlineModel()
                {
                    Count = Count,
                    OrderlineId = OrderlineId,
                    ProductId = ProductId
                };

                var adminEmail = (string)Session["Email"];
                var adminModel = _orderBLL.GetAdmin(adminEmail);


                return _orderBLL.UpdateOrderline(orderlineModel, adminModel.AdminId);

            }
            return false;
        }

        public double GetOrderSumTotal(int OrderId)
        {
            return _orderBLL.GetOrderSumTotal(OrderId);

        }

        [HttpPost]
        public bool DeleteOrder(int OrderId)
        {
            if ((Session["Admin"] == null ? false : (bool)Session["Admin"]))
            {
                return _orderBLL.DeleteOrder(OrderId);
            }
            return false;
        }
    }
}