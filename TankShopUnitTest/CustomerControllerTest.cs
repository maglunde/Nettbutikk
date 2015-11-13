using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcContrib.TestHelper;
using Nettbutikk.Controllers;
using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using BLL.Customer;
using DAL.Customer;


namespace TankShopUnitTest
{
    [TestClass]
    public class CustomerControllerTest
    {
        private List<OrderModel> orderModels;
        private OrderModel orderModel;
        private List<OrderlineModel> orderlineModels;
        private OrderlineModel orderlineModel;
        private CustomerModel customerModel;

        private string userEmail, anotherUserEmail, adminEmail;

        public CustomerControllerTest()
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

            customerModel = new CustomerModel()
            {
                CustomerId = 1,
                Email = "ole@gmail.com",
                Firstname = "Ole",
                Lastname = "Olsen",
                Address = "Persveien 5",
                Zipcode = "0123",
                City = "Oslo",
                Orders = orderModels
            };

            userEmail = "ole@gmail.com";
            anotherUserEmail = "notOle@gmail.com";
            adminEmail = "admin";
        }
    

        [TestMethod]
        public void Customer_Index_ok()
        {
            // Arrange 
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = true;

            var expViewName = "";

            // Act
            var result = (ViewResult)Controller.Index();

            // Assert
            Assert.AreEqual(expViewName,result.ViewName);
        }

        [TestMethod]
        public void Customer_AdminIndex_NotAdmin()
        {
            // Arrange 
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = false;

            var expRouteName = "";
            var expAction = "Index";
            var expController = "Home";

            // Act
            var result = (RedirectToRouteResult)Controller.Index();

            // Assert
            Assert.AreEqual(expRouteName, result.RouteName);
            Assert.AreEqual(expAction, result.RouteValues["action"]);
            Assert.AreEqual(expController, result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Customer_AdminIndex_NoAdminSession()
        {
            // Arrange 
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = null;

            var expRouteName = "";
            var expAction = "Index";
            var expController = "Home";

            // Act
            var result = (RedirectToRouteResult)Controller.Index();

            // Assert
            Assert.AreEqual(expRouteName, result.RouteName);
            Assert.AreEqual(expAction, result.RouteValues["action"]);
            Assert.AreEqual(expController, result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Customer_Show_CustomerOk()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = true;
            var customerId = 1;
            var returnUrl = "returnUrl";

            var expViewName = "Administration_Customer";
            var expViewBagCustomer = new CustomerView()
            {
                CustomerId = customerModel.CustomerId,
                Email = customerModel.Email,
                Firstname = customerModel.Firstname,
                Lastname = customerModel.Lastname,
                Address = customerModel.Address,
                Zipcode = customerModel.Zipcode,
                City = customerModel.City
            };
            var expViewBagReturnUrl = returnUrl;

            // Act
            var result = (ViewResult)Controller.ShowCustomer(customerId,returnUrl);

            var viewBagCustomer = result.ViewBag.Customer;
            var viewBagReturnUrl = result.ViewBag.ReturnUrl;

            // Assert
            Assert.AreEqual(expViewName, result.ViewName);

            Assert.AreEqual(expViewBagCustomer.CustomerId, viewBagCustomer.CustomerId);
            Assert.AreEqual(expViewBagCustomer.Firstname, viewBagCustomer.Firstname);
            Assert.AreEqual(expViewBagCustomer.Lastname, viewBagCustomer.Lastname);
            Assert.AreEqual(expViewBagCustomer.Address, viewBagCustomer.Address);
            Assert.AreEqual(expViewBagCustomer.Zipcode, viewBagCustomer.Zipcode);
            Assert.AreEqual(expViewBagCustomer.City, viewBagCustomer.City);

            Assert.AreEqual(expViewBagReturnUrl, viewBagReturnUrl);

        }

        [TestMethod]
        public void Customer_Show_Customer_NoAdminSession()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = null;
            var customerId = 1;
            var returnUrl = "returnUrl";
            
            var expRouteName = "";
            var expAction = "Index";
            var expController = "Home";

            // Act
            var result = (RedirectToRouteResult)Controller.ShowCustomer(customerId, returnUrl);
            

            // Assert
            Assert.AreEqual(expRouteName, result.RouteName);
            Assert.AreEqual(expAction, result.RouteValues["action"]);
            Assert.AreEqual(expController, result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Customer_Show_Customer_NotAdmin()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Admin"] = false;
            var customerId = 1;
            var returnUrl = "returnUrl";

            var expRouteName = "";
            var expAction = "Index";
            var expController = "Home";

            // Act
            var result = (RedirectToRouteResult)Controller.ShowCustomer(customerId, returnUrl);


            // Assert
            Assert.AreEqual(expRouteName, result.RouteName);
            Assert.AreEqual(expAction, result.RouteValues["action"]);
            Assert.AreEqual(expController, result.RouteValues["controller"]);
        }

        [TestMethod]
        public void Customer_CustomerlistPartial_List()
        {
            // Arrange
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));

            var expModel= new CustomerView()
            {
                CustomerId = customerModel.CustomerId,
                Email = customerModel.Email,
                Firstname = customerModel.Firstname,
                Lastname = customerModel.Lastname,
                Address = customerModel.Address,
                Zipcode = customerModel.Zipcode,
                City = customerModel.City
            };
            var expResult = new List<CustomerView>();
            expResult.Add(expModel);
            expResult.Add(expModel);
            expResult.Add(expModel);

            // Act
            var result = (PartialViewResult)Controller.CustomerlistPartial();
            var modelresult = (List<CustomerView>)result.Model;

            // Assert
            Assert.AreEqual("", result.ViewName);
            for (var i = 0; i < modelresult.Count; i++)
            {
                Assert.AreEqual(expResult[i].CustomerId, modelresult[i].CustomerId);
                Assert.AreEqual(expResult[i].Email, modelresult[i].Email);
                Assert.AreEqual(expResult[i].Firstname, modelresult[i].Firstname);
                Assert.AreEqual(expResult[i].Lastname, modelresult[i].Lastname);
                Assert.AreEqual(expResult[i].Address, modelresult[i].Address);
                Assert.AreEqual(expResult[i].Zipcode, modelresult[i].Zipcode);
                Assert.AreEqual(expResult[i].City, modelresult[i].City);
            }
        }

        [TestMethod]
        public void Customer_UpdateCustomer_UpdateSelfOk()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);

            Controller.Session["Email"] = userEmail;
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

            // Act
            var result = Controller.UpdateCustomerInfo(customerView);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Customer_UpdateCustomer_AdminUpdateOk()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = true;
            Controller.Session["Email"] = adminEmail;

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

            // Act
            var result = (bool)Controller.UpdateCustomerInfo(customerView);

            // Assert
            Assert.IsTrue(result);

        }

        [TestMethod]
        public void Customer_UpdateCustomer_NotLoggedIn()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Email"] = null;

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

            // Act
            var result = Controller.UpdateCustomerInfo(customerView);

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Customer_UpdateCustomer_UpdateAnotherCustomer()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Email"] = anotherUserEmail;

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

            // Act
            var result = Controller.UpdateCustomerInfo(customerView);

            // Assert
            Assert.IsFalse(result);

        }

        [TestMethod]
        public void Customer_UpdateCustomer_ModelError()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Email"] = userEmail;

            var customerView = new CustomerView()
            {
                CustomerId = customerModel.CustomerId,
                Email = "",
                Firstname = customerModel.Firstname,
                Lastname = customerModel.Lastname,
                Address = customerModel.Address,
                Zipcode = customerModel.Zipcode,
                City = customerModel.City
            };

            // Act
            var result = Controller.UpdateCustomerInfo(customerView);

            // Assert
            Assert.IsFalse(result);

        }



        [TestMethod]
        public void Customer_DeleteCustomer_Ok()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = true;
            var email = userEmail;

            // Act
            var result = Controller.DeleteCustomer(email);

            // Assert
            Assert.IsTrue(result);


        }

        [TestMethod]
        public void Customer_DeleteCustomer_NoEmailSession()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Email"] = null;
            var email = userEmail;

            // Act
            var result = (bool)Controller.DeleteCustomer(email);

            // Assert
            Assert.IsFalse(result);


        }

        [TestMethod]
        public void Customer_DeleteCustomer_NotAdmin()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = false;
            var email = userEmail;

            // Act
            var result = Controller.DeleteCustomer(email);

            // Assert
            Assert.IsFalse(result);


        }

        [TestMethod]
        public void Customer_DeleteCustomer_DeleteSelfNotAllowed()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var Controller = new CustomerController(new CustomerBLL(new CustomerRepoStub()));
            SessionMock.InitializeController(Controller);
            Controller.Session["Admin"] = true;
            Controller.Session["Email"] = adminEmail;
            var email = adminEmail;

            // Act
            var result = Controller.DeleteCustomer(email);

            // Assert
            Assert.IsFalse(result);

        }

    }
}
