using System.Collections.Generic;
using Nettbutikk.Model;

namespace BLL.Order
{
    public interface IOrderLogic
    {
        bool DeleteOrder(int orderId);
        List<OrderModel> GetAllOrders();
        List<ProductModel> GetAllProducts();
        OrderModel GetOrder(int OrderId);
        List<OrderModel> GetOrders(int CustomerId);
        double GetOrderSumTotal(int orderId);
        OrderModel GetReciept(int orderId);
        int PlaceOrder(OrderModel order);
        bool UpdateOrderline(OrderlineModel orderline);
        bool UpdateOrderline(OrderlineModel orderlineModel, int adminId);
        AdminModel GetAdmin(string adminEmail);
    }
}