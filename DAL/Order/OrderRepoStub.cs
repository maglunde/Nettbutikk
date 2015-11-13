using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.Order
{
    public class OrderRepoStub : IOrderRepo
    {
        private OrderlineModel orderlineModel;
        private List<OrderlineModel> orderlineModels;
        private OrderModel orderModel;
        private List<OrderModel> orderModels;

        public OrderRepoStub()
        {
            // DomainModels
            orderlineModel = new OrderlineModel()
            {
                Count = 1,
                OrderId = 1,
                OrderlineId = 1,
                ProductId = 1
            };

            orderlineModels = new List<OrderlineModel>();
            orderlineModels.Add(orderlineModel);
            orderlineModels.Add(orderlineModel);
            orderlineModels.Add(orderlineModel);

            orderModel = new OrderModel()
            {
                CustomerId = 1,
                Date = new DateTime(2015, 1, 1),
                OrderId = 1,
                Orderlines = orderlineModels
            };

            orderModels = new List<OrderModel>();
            orderModels.Add(orderModel);
            orderModels.Add(orderModel);
            orderModels.Add(orderModel);

            
        }

        public bool DeleteOrder(int orderId)
        {
            if (orderId == 0)
                return false;
            return true;
        }

        public List<OrderModel> GetAllOrders()
        {
            return orderModels;
        }

        public OrderModel GetOrder(int orderId)
        {
            if (orderId == 0)
                return new OrderModel()
                {
                    OrderId = 0
                };
            return orderModel;
        }

        public List<OrderModel> GetOrders(int customerId)
        {
            if (customerId == 0)
                return new List<OrderModel>();
            return orderModels;

        }

        public double GetOrderSumTotal(int orderId)
        {
            if (orderId == 0)
                return 0.0;
            return 1.0;
        }

        public OrderModel GetReciept(int orderId)
        {
            return orderModel;
        }

        public int PlaceOrder(OrderModel order)
        {
            if (order.CustomerId == 0)
                return 0;
            return 1;
        }

        public bool UpdateOrderline(OrderlineModel orderline)
        {
            if (orderline.OrderlineId == 0)
                return false;
            return true;
        }

        public bool UpdateOrderline(OrderlineModel orderlineModel, int adminId)
        {
            return (adminId != 0) && (orderlineModel.OrderlineId != 0);
        }
    }
}
