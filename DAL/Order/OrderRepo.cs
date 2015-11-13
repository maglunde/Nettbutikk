using Logging;
using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Order
{
    public class OrderRepo : IOrderRepo
    {
        public List<OrderModel> GetOrders(int customerId)
        {
            using (var db = new TankshopDbContext())
            {
                var customerOrders = new List<OrderModel>();
                var dbOrders = db.Orders.ToList(); ;

                foreach (var dbOrder in dbOrders)
                {
                    if (dbOrder.CustomerId == customerId)
                    {
                        try
                        {
                            var order = GetOrder(dbOrder.OrderId);
                            var count = order.Orderlines.Count;
                            if (count > 0)
                                customerOrders.Add(GetOrder(dbOrder.OrderId));
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

                return customerOrders;

            }
        }

        public OrderModel GetOrder(int orderId)
        {
            using (var db = new TankshopDbContext())
            {
                var dbOrder = db.Orders.Find(orderId);
                if (dbOrder == null)
                    return null;

                var order = new OrderModel()
                {
                    CustomerId = dbOrder.CustomerId,
                    OrderId = dbOrder.OrderId,
                    Orderlines = db.Orderlines.Where(l => l.OrderId == dbOrder.OrderId).Select(l => new OrderlineModel()
                    {
                        OrderlineId = l.OrderlineId,
                        OrderId = l.OrderId,
                        ProductId = l.ProductId,
                        Count = l.Count,
                        ProductName = l.Product.Name,
                        ProductPrice = l.Product.Price

                    }).ToList(),
                    Date = dbOrder.Date
                };

                return order;
            }
        }

        public int PlaceOrder(OrderModel order)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newOrder = new Nettbutikk.Model.Order()
                    {
                        CustomerId = order.CustomerId,
                        Date = order.Date
                    };

                    foreach (var item in order.Orderlines)
                    {
                        var product = db.Products.Find(item.ProductId);
                        var orderline = new Orderline()
                        {
                            Product = product,
                            Count = item.Count,
                            ProductId = item.ProductId
                        };

                        newOrder.Orderlines.Add(orderline);
                    }

                    db.Orders.Add(newOrder);
                    db.SaveChanges();
                    return newOrder.OrderId;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
        }

        public OrderModel GetReciept(int orderId)
        {
            using (var db = new TankshopDbContext())
            {
                var dbOrder = db.Orders.Find(orderId);
                var orderModel = new OrderModel()
                {
                    CustomerId = dbOrder.CustomerId,
                    Date = dbOrder.Date,
                    OrderId = dbOrder.OrderId,
                    Orderlines = dbOrder.Orderlines.Select(l => new OrderlineModel()
                    {
                        Count = l.Count,
                        OrderlineId = l.OrderlineId,
                        ProductId = l.ProductId,
                        ProductName = l.Product.Name,
                        ProductPrice = l.Product.Price
                    }).ToList()
                };

                return orderModel;
            }
        }

        public List<OrderModel> GetAllOrders()
        {
            using(var db = new TankshopDbContext())
            {
                var dbOrders = db.Orders.ToList();
                var orderModels = new List<OrderModel>();

                foreach(var dbOrder in dbOrders)
                {
                    try
                    {
                        var order = GetOrder(dbOrder.OrderId);
                        var count = order.Orderlines.Count;
                        if (count > 0)
                            orderModels.Add(order);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                return orderModels;
            }
        }

        public bool UpdateOrderline(OrderlineModel orderline)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbOrderline = db.Orderlines.Find(orderline.OrderlineId);

                    if (orderline.Count == 0)
                    {
                        db.Orderlines.Remove(dbOrderline);
                    }
                    else
                    {
                        dbOrderline.ProductId = orderline.ProductId;
                        dbOrderline.Count = orderline.Count;
                    }


                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public double GetOrderSumTotal(int orderId)
        {
            using (var db = new TankshopDbContext())
            {
                var sumTotal = 0.0;
                var dbOrder = db.Orders.Find(orderId);

                foreach (var l in dbOrder.Orderlines)
                {
                    var price = l.Product.Price;
                    var count = l.Count;
                    sumTotal += price * count;
                }

                return sumTotal;

            }
        }

        public bool DeleteOrder(int orderId)
        {
            using(var db = new TankshopDbContext())
            {
                try
                {
                    var dbOrder = db.Orders.Find(orderId);
                    db.Orders.Remove(dbOrder);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }

        public bool UpdateOrderline(OrderlineModel orderline, int adminId)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbOrderline = db.Orderlines.Find(orderline.OrderlineId);
                    var oldOrderline = new OldOrderline()
                    {
                        OrderlineId = dbOrderline.OrderlineId,
                        OrderId = dbOrderline.OrderId, 
                        ProductId_From = dbOrderline.ProductId,
                        ProductId_To= orderline.ProductId,
                        Count_From = dbOrderline.Count,
                        Count_To = orderline.Count,
                        AdminId = adminId,
                        Changed = DateTime.Now,
                    };
                     
                    if (orderline.Count == 0)
                    {
                        db.Orderlines.Remove(dbOrderline);
                    }
                    else
                    {
                        dbOrderline.ProductId = orderline.ProductId;
                        dbOrderline.Count = orderline.Count;
                    }

                    db.OldOrderLines.Add(oldOrderline);

                    db.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    LogHandler.WriteToLog(e);
                    return false;
                }
            }
        }
    }
}
