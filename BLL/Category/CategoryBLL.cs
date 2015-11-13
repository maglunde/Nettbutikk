using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;
using DAL.Category;

namespace BLL.Category
{
    public class CategoryBLL :  ICategoryLogic
    {
        
        private ICategoryRepo repo;

        public CategoryBLL()
        {
            repo = new CategoryRepo();
        }

        public CategoryBLL(ICategoryRepo repo)
        {
            this.repo = repo;
        }


        public bool AddCategory(string Name)
        {
            return repo.AddCategory(Name);
        }

        public bool DeleteCategory(int CategoryId, int AdminId)
        {
            Nettbutikk.Model.Category category = repo.GetCategory(CategoryId);

            if (category == null)
                return false;

            if (!repo.AddOldCategory(category.Name, AdminId))
                return false;

            return repo.DeleteCategory(CategoryId);
        }

        public bool UpdateCategory(int CategoryId, string Name, int AdminId)
        {

            Nettbutikk.Model.Category category = repo.GetCategory(CategoryId);

            if (category == null)
                return false;

            if (!repo.AddOldCategory(category.Name, AdminId))
                return false;

            return repo.UpdateCategory(CategoryId, Name);
        }

        public string GetCategoryName(int CategoryId)
        {
            return repo.GetCategoryName(CategoryId);
        }


        public int FirstCategoryWithProducts()
        {
            return repo.FirstCategoryWithProducts();
        }

        public Nettbutikk.Model.Category GetCategory(int CategoryId)
        {
            return repo.GetCategory(CategoryId);
        }

        public List<Nettbutikk.Model.Category> GetAllCategories()
        {
            return repo.GetAllCategories();
        }


        public CategoryModel GetCategoryModel(int CategoryId)
        {
            return repo.GetCategoryModel(CategoryId);
        }

        public List<CategoryModel> GetAllCategoryModels()
        {
            return repo.GetAllCategoryModels();
        }

    }
}