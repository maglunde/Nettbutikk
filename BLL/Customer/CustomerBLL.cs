using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using DAL.Customer;
using DAL.Account;
using DAL.Order;
using DAL.Product;

namespace BLL.Customer
{
    public class CustomerBLL :ICustomerLogic
    {
        private ICustomerRepo _repo;
        private IAccountRepo _accountrepo;
        private IOrderRepo _orderrepo;
        private IProductRepo _productrepo;

        public CustomerBLL()
        {
            _repo = new CustomerRepo();
            _accountrepo = new AccountRepo();
            _orderrepo = new OrderRepo();
            _productrepo = new ProductRepo();
        }

        public CustomerBLL(ICustomerRepo stub)
        {
            _repo = stub;
            _accountrepo = new AccountRepoStub();
            _orderrepo = new OrderRepoStub();
            _productrepo = new ProductRepoStub();
        }

        public bool DeleteCustomer(string email)
        {
            return _repo.DeleteCustomer(email);
        }

        public List<CustomerModel> GetAllCustomers()
        {
            return _repo.GetAllCustomers();
        }

        public bool UpdatePerson(PersonModel personUpdate, string email)
        {
            return _accountrepo.UpdatePerson(personUpdate, email);
        }

        //public List<OrderModel> GetAllOrders()
        //{
        //    return _orderrepo.GetAllOrders();
        //}

        //public bool UpdateOrderline(OrderlineModel orderline)
        //{
        //    return _orderrepo.UpdateOrderline(orderline);
        //}

        //public double GetOrderSumTotal(int orderId)
        //{
        //    return _orderrepo.GetOrderSumTotal(orderId);
        //}

        //public bool DeleteOrder(int orderId)
        //{
        //    return _orderrepo.DeleteOrder(orderId);
        //}

        public CustomerModel GetCustomer(int customerId)
        {
            return _repo.GetCustomer(customerId);
        }

        public List<ProductModel> GetAllProducts()
        {
            return _productrepo.GetAllProductModels();
        }

        public bool ExisitingEmail(string email)
        {
            return _repo.ExisitingEmail(email);
        }
    }
}
