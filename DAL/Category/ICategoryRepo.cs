using System.Collections.Generic;
using Nettbutikk.Model;

namespace DAL.Category
{
    public interface ICategoryRepo
    {
        bool AddCategory(string name);
        bool AddOldCategory(string Name, int adminId);
        bool DeleteCategory(int CategoryId);

        List<Nettbutikk.Model.Category> GetAllCategories();
        Nettbutikk.Model.Category GetCategory(int CategoryId);

        List<CategoryModel> GetAllCategoryModels();
        CategoryModel GetCategoryModel(int CategoryId);

        string GetCategoryName(int CategoryId);
        bool UpdateCategory(int CategoryId, string Name);
        int FirstCategoryWithProducts();

    }
}