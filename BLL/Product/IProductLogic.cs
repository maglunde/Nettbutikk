using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Product
{ 

    public interface IProductLogic
    {
        List<ProductModel> GetProducts(List<int> productIdList);
        List<ProductModel> GetProducts(string searchstr);
        List<ProductModel> GetProductsByCategory(int categoryId);

        //Nettbutikk.Model.Product GetProduct(int productId);
        ProductModel GetProductModel(int productId);

        List<Nettbutikk.Model.Product> GetAllProducts();
        List<ProductModel> GetAllProductModels();

        bool AddProduct(string Name, double Price, int Stock, string Description, int CategoryId);
        bool DeleteProduct(int ProductId, int AdminId);
        bool UpdateProduct(int ProductId, string Name, double Price, int Stock, string Description, int CategoryId, int AdminId);
        

    }
}
