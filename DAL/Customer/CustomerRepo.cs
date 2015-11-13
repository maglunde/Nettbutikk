using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Order;

namespace DAL.Customer
{
    public class CustomerRepo : ICustomerRepo
    {
        public bool ExisitingEmail(string email)
        {
            using (var db = new TankshopDbContext())
            {
                var dbPerson = db.People.Where(p => p.Email == email).FirstOrDefault();
                return dbPerson != null;
            }
        }

        public bool DeleteCustomer(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbPerson = db.People.Find(email);
                    var dbCustomer = db.Customers.FirstOrDefault(c => c.Email == email);
                    var dbAdmin = db.Admins.FirstOrDefault(a => a.Email == email);
                    var dbCredentials = db.Credentials.Find(email);

                    if (dbPerson != null)
                        db.People.Remove(dbPerson);
                    if (dbCustomer != null)
                        db.Customers.Remove(dbCustomer);
                    if (dbAdmin != null)
                        db.Admins.Remove(dbAdmin);
                    if (dbCredentials != null) db.Credentials.Remove(dbCredentials);


                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<CustomerModel> GetAllCustomers()
        {
            var customerList = new List<CustomerModel>();

            try
            {
                using (var db = new TankshopDbContext())
                {
                    var dbCustomers = db.Customers.ToList();


                    foreach (var c in dbCustomers)
                    {
                        var p = db.People.Find(c.Email);
                        var customer = new CustomerModel()
                        {
                            Email = p.Email,
                            Firstname = p.Firstname,
                            Lastname = p.Lastname,
                            Address = p.Address,
                            Zipcode = p.Zipcode,
                            City = p.Postal.City,
                            CustomerId = c.CustomerId,
                            Orders = c.Orders.Select(o => new OrderModel()
                            {
                                CustomerId = o.CustomerId,
                                Date = o.Date,
                                OrderId = o.OrderId,
                                Orderlines = o.Orderlines.Select(l => new OrderlineModel()
                                {
                                    Count = l.Count,
                                    OrderId = l.OrderId,
                                    OrderlineId = l.OrderlineId,
                                    ProductId = l.ProductId,
                                    ProductName = l.Product.Name,
                                    ProductPrice = l.Product.Price
                                }).ToList()
                            }).ToList()
                        };
                        customerList.Add(customer);
                    }
                    return customerList;
                }
            }
            catch (Exception)
            {
                return customerList;
            }
        }

        public CustomerModel GetCustomer(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbPerson = GetPerson(email);
                    var customerId = db.Customers.FirstOrDefault(c => c.Email == email).CustomerId;

                    var orderRepo = new OrderRepo();

                    var customer = new CustomerModel()
                    {
                        CustomerId = customerId,
                        Email = dbPerson.Email,
                        Firstname = dbPerson.Firstname,
                        Lastname = dbPerson.Lastname,
                        Address = dbPerson.Address,
                        Zipcode = dbPerson.Zipcode,
                        City = dbPerson.City,
                        Orders = orderRepo.GetOrders(customerId)
                    };

                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public CustomerModel GetCustomer(int customerId)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
                    var dbPerson = GetPerson(dbCustomer.Email);
                    var orderRepo = new OrderRepo();

                    var customer = new CustomerModel()
                    {
                        CustomerId = customerId,
                        Email = dbPerson.Email,
                        Firstname = dbPerson.Firstname,
                        Lastname = dbPerson.Lastname,
                        Address = dbPerson.Address,
                        Zipcode = dbPerson.Zipcode,
                        City = dbPerson.City,
                        Orders = orderRepo.GetOrders(customerId)
                    };

                    return customer;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        private PersonModel GetPerson(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {

                    var person = db.People.Where(p => p.Email == email).Select(p => new PersonModel()
                    {
                        Email = p.Email,
                        Firstname = p.Firstname,
                        Lastname = p.Lastname,
                        Address = p.Address,
                        Zipcode = p.Zipcode,
                        City = p.Postal.City
                    }).Single();

                    return person;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}
