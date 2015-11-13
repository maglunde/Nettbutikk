using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Product
{
    public interface IProductRepo
    {

        Nettbutikk.Model.Product GetProduct(int productId);
        List<Nettbutikk.Model.Product> GetAllProducts();

        ProductModel GetProductModel(int productId);
        List<ProductModel> GetAllProductModels();

        List<ProductModel> GetProducts(List<int> productIdList);
        List<ProductModel> GetProducts(string searchstr);
        List<ProductModel> GetProductsByCategory(int categoryId);

        bool AddProduct(string Name, double Price, int Stock, string Description, int CategoryId);
        bool DeleteProduct(int ProductId);
        bool UpdateProduct(int ProductId, string Name, double Price, int Stock, string Description, int CategoryId);
        bool AddOldProduct(string Name, double Price, int Stock, string Description, int CategoryId, int AdminId);

    }
}
