using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Customer;


namespace Nettbutikk.Controllers
{
    public class CustomerController : Controller
    {
        private ICustomerLogic _customerBLL;

        public CustomerController()
        {
            _customerBLL = new CustomerBLL();
        }

        public CustomerController(ICustomerLogic stub)
        {
            _customerBLL = stub;
        }


        // GET: Customer / Order administration
        public ActionResult Index()
        {
            if ((Session["Admin"] == null ? false : (bool)Session["Admin"]))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ShowCustomer(int CustomerId, string ReturnUrl)
        {
            if ((Session["Admin"] == null ? false : (bool)Session["Admin"]))
            {
                var customerModel = _customerBLL.GetCustomer(CustomerId);

                var customerView = new CustomerView()
                {
                    CustomerId = customerModel.CustomerId,
                    Email = customerModel.Email,
                    Firstname = customerModel.Firstname,
                    Lastname = customerModel.Lastname,
                    Address = customerModel.Address,
                    Zipcode = customerModel.Zipcode,
                    City = customerModel.City
                };


                ViewBag.Customer = customerView;
                ViewBag.ReturnUrl = ReturnUrl;

                return View("Administration_Customer");

            }
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public PartialViewResult CustomerlistPartial()
        {
            var customerViewList = new List<CustomerView>();
            var customerModels = _customerBLL.GetAllCustomers();

            foreach (var Customer in customerModels)
            {
                var customerView = new CustomerView()
                {
                    CustomerId = Customer.CustomerId,
                    Email = Customer.Email,
                    Firstname = Customer.Firstname,
                    Lastname = Customer.Lastname,
                    Address = Customer.Address,
                    Zipcode = Customer.Zipcode,
                    City = Customer.City
                };

                customerViewList.Add(customerView);
            }

            return PartialView(customerViewList);
        }

        public bool ExisitingEmail(string Email)
        {
            return _customerBLL.ExisitingEmail(Email);
        }

        [HttpPost]
        public bool DeleteCustomer(string email)
        {
            if ((Session["Admin"] == null ? false : (bool)Session["Admin"]))
            {
                if ((string)Session["Email"] != email)
                {
                    return _customerBLL.DeleteCustomer(email);
                }
            }
            return false;
        }

        [HttpPost]
        public bool UpdateCustomerInfo(CustomerView customerEdit)
        {
            if (Session["Email"] != null)
            {
                var email = customerEdit.Email;
                var personUpdate = new PersonModel()
                {
                    Firstname = customerEdit.Firstname,
                    Lastname = customerEdit.Lastname,
                    Address = customerEdit.Address,
                    Zipcode = customerEdit.Zipcode,
                    City = customerEdit.City
                };
                if ((string)Session["Email"] == email || Session["Admin"] != null)
                {
                    return _customerBLL.UpdatePerson(personUpdate, email);
                }
            }
            return false;

        }


    }

}