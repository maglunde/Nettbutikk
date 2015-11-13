using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nettbutikk.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using Nettbutikk.Controllers;
using MvcContrib.TestHelper;
using BLL.Category;
using DAL.Category;
using BLL.Account;
using DAL.Account;


namespace TankShopEnhetstest
{
    [TestClass]
    public class CategoryControllerTest
    {

        //GoodInput = input of correct type and value
        //BadInput = input of wrong type. For example "abc123" being sent instead of an int
        //Invalid = input of correct type, but wrong value. For example an id of -1

        [TestMethod]
        public void Category_Index()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            
            
            var expectedCategories = new List<CategoryModel>() {
                new CategoryModel { CategoryId = 1, CategoryName = "test name 1"},
                new CategoryModel { CategoryId = 2, CategoryName = "test name 2"},
                new CategoryModel { CategoryId = 3, CategoryName = "test name 3"},
                new CategoryModel { CategoryId = 4, CategoryName = "test name 4"}
            };
            


            //Act
            var viewResult = controller.Index() as ViewResult;
            var actualCategories = viewResult.ViewBag.Categories;


            //Assert
            Assert.AreEqual(expectedCategories.Count, actualCategories.Count);
            for (int i = 0; i < actualCategories.Count; i++)
            {
                Assert.AreEqual(expectedCategories[i].CategoryId, actualCategories[i].CategoryId);
                Assert.AreEqual(expectedCategories[i].CategoryName, actualCategories[i].CategoryName);
            }

            Assert.AreEqual("ListCategory", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_CreateCategory()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));

            //Act
            var viewResult = controller.CreateCategory() as ViewResult;


            //Assert
            Assert.AreEqual("", viewResult.ViewName);
        }


        [TestMethod]
        public void Category_EditCategory_GoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "2";
            var expectedCategory = new CategoryModel { CategoryId = 2, CategoryName = "test" };

            //Act
            var viewResult = controller.EditCategory(categoryId) as ViewResult;
            var actualCategory = controller.ViewBag.Category;


            //Assert
            Assert.AreEqual(expectedCategory.CategoryId, actualCategory.CategoryId);
            Assert.AreEqual(expectedCategory.CategoryName, actualCategory.CategoryName);
            Assert.AreEqual("", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_EditCategory_BadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "123abc";

            //Act
            var viewResult = controller.EditCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_EditCategory_InvalidInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "-1";

            //Act
            var viewResult = controller.EditCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Couldnt find a category with id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_DeleteCategory_GoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "2";
            var expectedCategory = new CategoryModel { CategoryId = 2, CategoryName = "test name" };

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;
            var actualCategory = controller.ViewBag.Category;


            //Assert
            Assert.AreEqual(expectedCategory.CategoryId, actualCategory.CategoryId);
            Assert.AreEqual(expectedCategory.CategoryName, actualCategory.CategoryName);

            Assert.AreEqual("", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_DeleteCategory_BadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "123abc";

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_DeleteCategory_InvalidInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));
            string categoryId = "-1";

            //Act
            var viewResult = controller.DeleteCategory(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Couldnt find a category with the id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Create_GoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryName = "test name";

            //Act
            var viewResult = controller.Create(categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was added to the database", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        
        [TestMethod]
        public void Category_Create_NotAdmin()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = false;
            controller.Session["Email"] = "ole";

            string categoryName = "test name";

            //Act
            var viewResult = controller.Create(categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Only administrators can create categories", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void Category_Edit_GoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryName = "test name";
            string categoryId = "2";

            //Act
            var viewResult = controller.Edit(categoryId, categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was updated", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Edit_NoIdentifierFound()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;

            string categoryName = "test name";
            string categoryId = "2";

            //Act
            var viewResult = controller.Edit(categoryId, categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Cannot perform admin tasks without a valid email", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Edit_NotAdmin()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = false;
            controller.Session["Email"] = "ole";

            string categoryName = "test name";
            string categoryId = "2";

            //Act
            var viewResult = controller.Edit(categoryId, categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Only administrators can edit categories", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Edit_BadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryName = "test name";
            string categoryId = "2asb";

            //Act
            var viewResult = controller.Edit(categoryId, categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Edit_InvalidInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryName = "test name";
            string categoryId = "-1";

            //Act
            var viewResult = controller.Edit(categoryId, categoryName) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Could not update the category", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }


        [TestMethod]
        public void Category_Delete_GoodInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryId = "2";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Success", controller.ViewBag.Title);
            Assert.AreEqual("Category was deleted", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Delete_NoIdentifierFound()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;

            string categoryId = "2";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Cannot perform admin tasks without a valid email", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }



        [TestMethod]
        public void Category_Delete_NotAdmin()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = false;
            controller.Session["Email"] = "ole";

            string categoryId = "2";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Only administrators can delete categories", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Delete_BadInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryId = "2asb";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Invalid category id: " + categoryId, controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }

        [TestMethod]
        public void Category_Delete_InvalidInput()
        {

            //Arrange
            var controller = new CategoryController(new CategoryBLL(new CategoryRepoStub())
                , new AccountBLL(new AccountRepoStub()));

            var sessionMock = new TestControllerBuilder();
            sessionMock.InitializeController(controller);
            controller.Session["Admin"] = true;
            controller.Session["Email"] = "admin";

            string categoryId = "-1";

            //Act
            var viewResult = controller.Delete(categoryId) as ViewResult;

            //Assert
            Assert.AreEqual("Error", controller.ViewBag.Title);
            Assert.AreEqual("Could not delete the category", controller.ViewBag.Message);
            Assert.AreEqual("~/Views/Shared/Result.cshtml", viewResult.ViewName);

        }
        
    }
}
