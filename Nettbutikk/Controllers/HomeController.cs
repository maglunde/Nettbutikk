
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Product;
using BLL.Category;

namespace Nettbutikk.Controllers
{
    public class HomeController : Controller
    {
        private IProductLogic _productBLL;
        private ICategoryLogic _categoryBLL;

        public HomeController()
        {
            _productBLL = new ProductBLL();
            _categoryBLL = new CategoryBLL();
        }

        public HomeController(IProductLogic productStub, ICategoryLogic categoryStub)
        {
            _productBLL = productStub;
            _categoryBLL = categoryStub;
        }


        public ActionResult Index()
        {
            var categories = _categoryBLL.GetAllCategoryModels().Select(c => new CategoryView()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }
            ).ToList();

            int firstCategoryWithProducts = _categoryBLL.FirstCategoryWithProducts();

            var productModels = _productBLL.GetProductsByCategory(firstCategoryWithProducts).ToList();

            var products = new List<ProductView>();
            foreach (var product in productModels)
            {
                var imageViews = new List<ImageView>();
                foreach(var image in product.Images)
                {
                    var imageView = new ImageView()
                    {
                        ImageId = image.ImageId,
                        ProductId = image.ProductId,
                        ImageUrl = image.ImageUrl
                    };
                    imageViews.Add(imageView);
                }
                var productView = new ProductView()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName,
                    Images = imageViews
                };
                products.Add(productView);
            }

            ViewBag.Categories = categories ?? new List<CategoryView>();
            ViewBag.Products = products ?? new List<ProductView>();
            ViewBag.LoggedIn = LoginStatus();
            ViewBag.CategoryName = _categoryBLL.GetCategoryName(firstCategoryWithProducts);
            ViewBag.Message = TempData["Message"] != null ? TempData["Message"] : "";
            return View();
        }

        public ActionResult Category(int CategoryId)
        {
            var categories = _categoryBLL.GetAllCategoryModels().Select(c => new CategoryView()
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName
            }
            ).ToList();

            var productModels = _productBLL.GetProductsByCategory(CategoryId).ToList();

            var products = new List<ProductView>();
            foreach (var product in productModels)
            {
                var imageViews = new List<ImageView>();
                foreach (var image in product.Images)
                {
                    var imageView = new ImageView()
                    {
                        ImageId = image.ImageId,
                        ProductId = image.ProductId,
                        ImageUrl = image.ImageUrl
                    };
                    imageViews.Add(imageView);
                }
                var productView = new ProductView()
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Stock = product.Stock,
                    CategoryId = product.CategoryId,
                    CategoryName = product.CategoryName,
                    Images = imageViews
                };
                products.Add(productView);
            }

            ViewBag.Categories = categories;
            ViewBag.Products = products;
            ViewBag.LoggedIn = LoginStatus();
            ViewBag.CategoryName = _categoryBLL.GetCategoryName(CategoryId) ?? "Epler?";

            return View("Index");
        }


        public bool LoginStatus()
        {
            bool LoggedIn = false;
            if (Session["LoggedIn"] != null)
            {
                LoggedIn = (bool)Session["LoggedIn"];
            }
            return LoggedIn;
        }

    }
}