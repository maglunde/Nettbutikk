using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.Category
{
    public class CategoryRepoStub : ICategoryRepo
    {
        public bool AddCategory(string Name)
        {
            return Name != "invalid";
        }

        public bool AddOldCategory(string Name, int adminId)
        {
            return Name != "invalid";
        }

        public bool DeleteCategory(int CategoryId)
        {
            return CategoryId != -1;
        }

        public List<Nettbutikk.Model.Category> GetAllCategories()
        {
            var allCategories = new List<Nettbutikk.Model.Category> {
                new Nettbutikk.Model.Category { CategoryId = 1, Name = "test1"},
                new Nettbutikk.Model.Category { CategoryId = 2, Name = "test2"},
                new Nettbutikk.Model.Category { CategoryId = 3, Name = "test3"},
                new Nettbutikk.Model.Category { CategoryId = 4, Name = "test4"}
            };

            return allCategories;
        }

        public Nettbutikk.Model.Category GetCategory(int categoryId)
        {
            return categoryId == -1 ? null : new Nettbutikk.Model.Category { CategoryId = categoryId, Name = "test" };
        }

        public string GetCategoryName(int CategoryId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateCategory(int CategoryId, string Name)
        {

            return CategoryId != -1;

        }

        public List<CategoryModel> GetAllCategoryModels()
        {
            var allCategories = new List<CategoryModel> {
                new CategoryModel { CategoryId = 1, CategoryName = "test name 1"},
                new CategoryModel { CategoryId = 2, CategoryName = "test name 2"},
                new CategoryModel{ CategoryId = 3, CategoryName = "test name 3"},
                new CategoryModel { CategoryId = 4, CategoryName = "test name 4"}
            };

            return allCategories;
        }

        public CategoryModel GetCategoryModel(int CategoryId)
        {
            return CategoryId == -1 ? null : new CategoryModel { CategoryId = CategoryId, CategoryName = "test name" };
        }

        public int FirstCategoryWithProducts()
        {
            throw new NotImplementedException();
        }
    }
}
