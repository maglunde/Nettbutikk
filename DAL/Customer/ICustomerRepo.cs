using System.Collections.Generic;
using Nettbutikk.Model;

namespace DAL.Customer
{
    public interface ICustomerRepo
    {
        bool DeleteCustomer(string email);
        List<CustomerModel> GetAllCustomers();
        CustomerModel GetCustomer(int customerId);
        CustomerModel GetCustomer(string email);
        bool ExisitingEmail(string email);
    }
}