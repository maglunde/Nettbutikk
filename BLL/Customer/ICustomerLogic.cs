using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Customer
{
    public interface ICustomerLogic
    {
        bool DeleteCustomer(string email);
        bool ExisitingEmail(string email);
        List<CustomerModel> GetAllCustomers();
        List<ProductModel> GetAllProducts();
        CustomerModel GetCustomer(int customerId);
        bool UpdatePerson(PersonModel personUpdate, string email);
    }
}
