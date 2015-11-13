using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.Customer
{
    public class CustomerRepoStub : ICustomerRepo
    {
        private List<OrderModel> orderModels;
        private OrderModel orderModel;
        private List<OrderlineModel> orderlineModels;
        private OrderlineModel orderlineModel;
        private CustomerModel customerModel;

        public CustomerRepoStub()
        {
            orderlineModel = new OrderlineModel()
            {
                Count = 1,
                OrderId = 1,
                OrderlineId = 1,
                ProductId = 1,
                ProductName = "Tank",
                ProductPrice = 500000
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

            customerModel =  new CustomerModel()
            {
                CustomerId = 1,
                Email = "ole@gmail.com",
                Firstname = "Ole",
                Lastname = "Olsen",
                Address = "Persveien 5",
                Zipcode = "0123",
                City = "Oslo",
                Orders= orderModels
            };
        }

        public bool DeleteCustomer(string email)
        {
            if (email == "")
                return false;
            return true;
        }

        public List<CustomerModel> GetAllCustomers()
        {
            var customerModels = new List<CustomerModel>();
            customerModels.Add(customerModel);
            customerModels.Add(customerModel);
            customerModels.Add(customerModel);

            return customerModels;

        }

        public CustomerModel GetCustomer(string email)
        {
            if (email == "")
                return new CustomerModel()
                {
                    Firstname = ""
                };
            return customerModel;
        }

        public CustomerModel GetCustomer(int customerId)
        {
            if (customerId == 0)
                return new CustomerModel()
                {
                    Firstname = ""
                };
            return customerModel;
        }

        public bool ExisitingEmail(string email)
        {
            return email == "";
        }
    }
}
