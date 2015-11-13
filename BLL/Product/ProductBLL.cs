using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;
using DAL.Product;
using DAL.Category;

namespace BLL.Product
{
    public class ProductBLL : IProductLogic
    {
        private IProductRepo _productRepo;
        private ICategoryRepo _categoryRepo;

        public ProductBLL()
        {
            _productRepo = new ProductRepo();
            _categoryRepo = new CategoryRepo();
        }

        public ProductBLL(IProductRepo stub)
        {
            _productRepo = stub;
            _categoryRepo = new CategoryRepoStub();
        }

        public List<CategoryModel> AllCategories()
        {
            return _categoryRepo.GetAllCategoryModels();
        }

        public List<ProductModel> GetAllProductModels()
        {
            return _productRepo.GetAllProductModels();
        }

        public string GetCategoryName(int categoryId)
        {
            return _categoryRepo.GetCategoryName(categoryId);
        }

        public Nettbutikk.Model.Product GetProduct(int productId)
        {
            return _productRepo.GetProduct(productId);
        }

        public List<ProductModel> GetProducts(string searchstr)
        {
            return _productRepo.GetProducts(searchstr);
        }

        public List<ProductModel> GetProducts(List<int> productIdList)
        {
            return _productRepo.GetProducts(productIdList);
        }

        public List<ProductModel> GetProductsByCategory(int categoryId)
        {
            return _productRepo.GetProductsByCategory(categoryId);
        }

        public bool AddProduct(string Name, double Price, int Stock, string Description, int CategoryId)
        {
            return _productRepo.AddProduct(Name, Price, Stock, Description, CategoryId);
        }

        public bool DeleteProduct(int ProductId, int AdminId)
        {

            Nettbutikk.Model.Product product = _productRepo.GetProduct(ProductId);

            if (product == null)
                return false;

            if (!_productRepo.AddOldProduct(product.Name, product.Price, product.Stock, product.Description, product.CategoryId, AdminId))
                return false;


            return _productRepo.DeleteProduct(ProductId);
        }

        public bool UpdateProduct(int ProductId, string Name, double Price, int Stock, string Description, int CategoryId, int AdminId)
        {
            Nettbutikk.Model.Product product = _productRepo.GetProduct(ProductId);

            if (product == null)
                return false;

            if (!_productRepo.AddOldProduct(product.Name, product.Price, product.Stock, product.Description, product.CategoryId, AdminId))
                return false;


            return _productRepo.UpdateProduct(ProductId, Name, Price, Stock, Description, CategoryId);
        }

        public ProductModel GetProductModel(int productId)
        {
            return _productRepo.GetProductModel(productId);
        }

        public List<Nettbutikk.Model.Product> GetAllProducts()
        {
            return _productRepo.GetAllProducts();
        }
    }
}
