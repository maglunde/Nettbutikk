using Nettbutikk.Model;
using System;
using System.Collections.Generic;


namespace DAL.Order
{
    public interface IOrderRepo
    {
        bool DeleteOrder(int orderId);
        List<OrderModel> GetAllOrders();
        OrderModel GetOrder(int orderId);
        List<OrderModel> GetOrders(int customerId);
        double GetOrderSumTotal(int orderId);
        OrderModel GetReciept(int orderId);
        int PlaceOrder(OrderModel order);
        bool UpdateOrderline(OrderlineModel orderline);
        bool UpdateOrderline(OrderlineModel orderlineModel, int adminId);
    }
}
