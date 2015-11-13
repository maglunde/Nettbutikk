using System.Collections.Generic;
using Nettbutikk.Model;

namespace BLL.Category
{
    public interface ICategoryLogic
    {
        bool AddCategory(string Name);
        bool DeleteCategory(int CategoryId, int AdminId);

        Nettbutikk.Model.Category GetCategory(int CategoryId);
        List<Nettbutikk.Model.Category> GetAllCategories();

        CategoryModel GetCategoryModel(int CategoryId);
        List<CategoryModel> GetAllCategoryModels();

        string GetCategoryName(int CategoryId);
        bool UpdateCategory(int CategoryId, string Name, int AdminId);
        int FirstCategoryWithProducts();
    }
}