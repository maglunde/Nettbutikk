using System;
using System.Collections.Generic;
using System.Linq;
using Nettbutikk.Model;

namespace DAL.Account
{
    public class AccountRepo : IAccountRepo
    {

        public bool AddPerson(PersonModel person, Role role, string password)
        {
            var email = person.Email;
            var newPerson = new Person()
            {
                Email = email,
                Firstname = person.Firstname,
                Lastname = person.Lastname,
                Address = person.Address,
                Zipcode = person.Zipcode,

            };
            using (var db = new TankshopDbContext())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        var personPostal = db.Postals.Find(person.Zipcode);
                        if (personPostal == null)
                        {
                            personPostal = new Postal()
                            {
                                Zipcode = person.Zipcode,
                                City = person.City
                            };
                        }
                        personPostal.People.Add(newPerson);
                        newPerson.Postal = personPostal;


                        // Create email / password - combination
                        var existingCredentials = db.Credentials.Find(email);
                        if (existingCredentials != null)
                            return false;

                        var passwordHash = CreateHash(password);
                        var newCredentials = new Credential()
                        {
                            Email = email,
                            Password = passwordHash
                        };
                        db.Credentials.Add(newCredentials);

                        // Set Customer / AdminId
                        int AdminId = 0, CustomerId = 0;
                        if (role == Role.Admin)
                        {
                            var dbAdmin = db.Admins.FirstOrDefault(a => a.Email == email);
                            if (dbAdmin == null)
                            {
                                dbAdmin = new Admin()
                                {
                                    Email = email
                                };
                                db.Admins.Add(dbAdmin);
                            }
                            AdminId = dbAdmin.AdminId;
                        }
                        if (role == Role.Customer)
                        {
                            var dbCustomer = db.Customers.FirstOrDefault(c => c.Email == email);
                            if (dbCustomer == null)
                            {
                                dbCustomer = new Nettbutikk.Model.Customer()
                                {
                                    Email = email
                                };
                                db.Customers.Add(dbCustomer);
                            }
                            CustomerId = dbCustomer.CustomerId;

                        }

                        db.People.Add(newPerson);

                        db.SaveChanges();
                        transaction.Commit();

                        return true;

                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
            }
        }
        public bool AttemptLogin(string email, string password)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var passwordHash = CreateHash(password);
                    var existingUser = db.Credentials.FirstOrDefault(c => c.Email == email && c.Password == passwordHash);

                    if (existingUser == null)
                        return false;

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool ChangePassword(string email, string newPassword)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newPasswordHash = CreateHash(newPassword);
                    var existingUser = db.Credentials.Find(email);
                    if (existingUser == null)
                        return false;

                    existingUser.Password = newPasswordHash;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool CreateCredentials(string email, string password)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var existingCredentials = db.Credentials.Find(email);
                    if (existingCredentials != null)
                        return false;

                    var passwordHash = CreateHash(password);
                    var newCredentials = new Credential()
                    {
                        Email = email,
                        Password = passwordHash
                    };
                    db.Credentials.Add(newCredentials);

                    //db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }


            }
        }
        public bool DeletePerson(string email)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var deletePerson = db.People.Find(email);
                    var deleteCustomer = db.Customers.FirstOrDefault(c => c.Email == email);
                    var deleteAdmin = db.Admins.FirstOrDefault(a => a.Email == email);

                    if (deletePerson != null)
                        db.People.Remove(deletePerson);
                    if (deleteCustomer != null)
                        db.Customers.Remove(deleteCustomer);
                    if (deleteAdmin != null)
                        db.Admins.Remove(deleteAdmin);

                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public AdminModel GetAdmin(int adminId)
        {
            using (var db = new TankshopDbContext())
            {
                var dbAdmin = db.Admins.FirstOrDefault(a => a.AdminId == adminId);
                var dbPerson = GetPerson(dbAdmin.Email);
                var admin = new AdminModel()
                {
                    AdminId = adminId,
                    Email = dbPerson.Email,
                    Firstname = dbPerson.Firstname,
                    Lastname = dbPerson.Lastname,
                    Address = dbPerson.Address,
                    Zipcode = dbPerson.Zipcode,
                    City = dbPerson.City
                };

                return admin;
            }
        }
        public AdminModel GetAdmin(string email)
        {
            using (var db = new TankshopDbContext())
            {
                var adminId = db.Admins.FirstOrDefault(a => a.Email == email).AdminId;
                var dbPerson = GetPerson(email);
                var admin = new AdminModel()
                {
                    AdminId = adminId,
                    Email = dbPerson.Email,
                    Firstname = dbPerson.Firstname,
                    Lastname = dbPerson.Lastname,
                    Address = dbPerson.Address,
                    Zipcode = dbPerson.Zipcode,
                    City = dbPerson.City
                };

                return admin;
            }

        }
        public List<PersonModel> GetAllPeople()
        {
            using (var db = new TankshopDbContext())
            {
                var people = db.People.Select(p => new PersonModel()
                {
                    Email = p.Email,
                    Firstname = p.Firstname,
                    Lastname = p.Lastname,
                    Address = p.Address,
                    Zipcode = p.Zipcode,
                    City = p.Postal.City
                }).ToList();

                return people;
            }
        }
        //public CustomerModel GetCustomer(int customerId)
        ////{
        ////    using (var db = new TankshopDbContext())
        ////    {
        ////        try
        ////        {
        ////            var dbCustomer = db.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        ////            var dbPerson = GetPerson(dbCustomer.Email);
        ////            var orderRepo = new OrderRepo();

        ////            var customer = new CustomerModel()
        ////            {
        ////                CustomerId = customerId,
        ////                Email = dbPerson.Email,
        ////                Firstname = dbPerson.Firstname,
        ////                Lastname = dbPerson.Lastname,
        ////                Address = dbPerson.Address,
        ////                Zipcode = dbPerson.Zipcode,
        ////                City = dbPerson.City,
        ////                Orders = orderRepo.GetOrders(customerId)
        ////            };

        ////            return customer;
        ////        }
        ////        catch (Exception)
        ////        {
        ////            return null;
        ////        }
        ////    }
        ////}
        //public CustomerModel GetCustomer(string email)
        //{
        //    using (var db = new TankshopDbContext())
        //    {
        //        try
        //        {
        //            var dbPerson = GetPerson(email);
        //            var customerId = db.Customers.FirstOrDefault(c => c.Email == email).CustomerId;

        //            var orderRepo = new OrderRepo();

        //            var customer = new CustomerModel()
        //            {
        //                CustomerId = customerId,
        //                Email = dbPerson.Email,
        //                Firstname = dbPerson.Firstname,
        //                Lastname = dbPerson.Lastname,
        //                Address = dbPerson.Address,
        //                Zipcode = dbPerson.Zipcode,
        //                City = dbPerson.City,
        //                Orders = orderRepo.GetOrders(customerId)
        //            };

        //            return customer;
        //        }
        //        catch (Exception)
        //        {
        //            return null;
        //        }
        //    }
        //}
        public PersonModel GetPerson(string email)
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
        public bool isAdmin(string email)
        {
            using (var db = new TankshopDbContext())
            {
                var dbAdmin = db.Admins.FirstOrDefault(a => a.Email == email);

                return dbAdmin != null;
            }
        }
        public bool SetRole(string email, Role role, bool isRole)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    if (role == Role.Admin)
                    {
                        var dbAdmin = db.Admins.Find(email);
                        if (isRole)
                        {
                            if (dbAdmin == null)
                            {
                                var newAdmin = new Admin()
                                {
                                    Email = email
                                };
                                db.Admins.Add(newAdmin);
                            }
                        }
                        else
                        {
                            db.Admins.Remove(dbAdmin);
                        }
                    }
                    if (role == Role.Customer)
                    {
                        var dbCustomer = db.Customers.Find(email);
                        if (isRole)
                        {
                            if (dbCustomer == null)
                            {
                                var newCustomer = new Nettbutikk.Model.Customer()
                                {
                                    Email = email
                                };
                                db.Customers.Add(newCustomer);
                            }
                        }
                        else
                        {
                            db.Customers.Remove(dbCustomer);
                        }
                    }
                    //db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        public bool UpdatePerson(PersonModel personUpdate, string email)
        {
            // TODO: update admin/customer -id
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var editPerson = db.People.Find(email);
                    var editPersonModel = GetPerson(email);

                    editPerson.Firstname = personUpdate.Firstname;
                    editPerson.Lastname = personUpdate.Lastname;
                    editPerson.Address = personUpdate.Address;

                    var personPostal = db.Postals.Find(personUpdate.Zipcode);
                    if (personPostal == null)
                    {
                        var oldPostal = db.Postals.Find(editPerson.Zipcode);
                        if (oldPostal != null)
                            oldPostal.People.Remove(editPerson);
                        db.SaveChanges();

                        personPostal = new Postal()
                        {
                            Zipcode = personUpdate.Zipcode,
                            City = personUpdate.City
                        };
                        personPostal.People.Add(editPerson);
                        db.SaveChanges();
                    }

                    editPerson.Zipcode = personUpdate.Zipcode;
                    editPerson.Postal = personPostal;

                    db.SaveChanges();

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }
        private byte[] CreateHash(string password)
        {
            byte[] inData, outData;
            var alg = System.Security.Cryptography.SHA256.Create();
            inData = System.Text.Encoding.Default.GetBytes(password);
            outData = alg.ComputeHash(inData);
            return outData;
        }
        

    }
}
