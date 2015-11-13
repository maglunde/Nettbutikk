using System;
using System.Collections.Generic;
using System.Linq;
using Nettbutikk.Model;
using Logging;

namespace DAL.Category
{
    public class CategoryRepo : ICategoryRepo
    {


        public Nettbutikk.Model.Category GetCategory(int CategoryId)
        {
            try
            {
                return new TankshopDbContext().Categories.FirstOrDefault(c => c.CategoryId == CategoryId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                return null;
            }

        }

        public CategoryModel GetCategoryModel(int CategoryId)
        {

            using (var db = new TankshopDbContext())
            {
                var c = db.Categories.Find(CategoryId);

                var productModels = new List<ProductModel>();

                foreach (var product in c.Products)
                {
                    var imageModels = new List<ImageModel>();

                    foreach (var image in product.Images)
                    {
                        var imageModel = new ImageModel()
                        {
                            ImageId = image.ImageId,
                            ImageUrl = image.ImageUrl,
                            ProductId = image.ProductId
                        };
                        imageModels.Add(imageModel);
                    }

                    var productModel = new ProductModel()
                    {
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        Stock = product.Stock,
                        Images = imageModels
                    };

                    productModels.Add(productModel);
                }

                var categoryModel = new CategoryModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name,
                    Products = productModels
                };

                return categoryModel;
            }
        }

        public List<Nettbutikk.Model.Category> GetAllCategories() {

            return new TankshopDbContext().Categories.ToList();

        }

        public List<CategoryModel> GetAllCategoryModels()
        {

            var db = new TankshopDbContext();

            var dbCategories = db.Categories.ToList();
            var categoryModels = new List<CategoryModel>();

            foreach (var c in dbCategories)
            {
                var productModels = new List<ProductModel>();
                
                foreach(var product in c.Products)
                {
                    var imageModels = new List<ImageModel>();

                    foreach(var image in product.Images)
                    {
                        var imageModel = new ImageModel()
                        {
                            ImageId = image.ImageId,
                            ImageUrl = image.ImageUrl,
                            ProductId = image.ProductId
                        };
                        imageModels.Add(imageModel);
                    }

                    var productModel = new ProductModel()
                    {
                        CategoryId = product.CategoryId,
                        CategoryName = product.Category.Name,
                        Description = product.Description,
                        Price = product.Price,
                        ProductId = product.ProductId,
                        ProductName = product.Name,
                        Stock = product.Stock,
                        Images = imageModels
                    };

                    productModels.Add(productModel);
                }

                var categoryModel = new CategoryModel()
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.Name,
                    Products = productModels
                };

                categoryModels.Add(categoryModel);

            }
            return categoryModels;
        }

        public bool AddCategory(string name)
        {

            try
            {
                var db = new TankshopDbContext();
                db.Categories.Add(new Nettbutikk.Model.Category() { Name = name });
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;

        }

        public bool DeleteCategory(int CategoryId)
        {

            var db = new TankshopDbContext();

            Nettbutikk.Model.Category category = (from c in db.Categories where c.CategoryId == CategoryId select c).FirstOrDefault();

            if (category == null)
                return false;


            try
            {
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public bool UpdateCategory(int CategoryId, string Name)
        {

            var db = new TankshopDbContext();

            Nettbutikk.Model.Category category = (from c in db.Categories where c.CategoryId == CategoryId select c).FirstOrDefault();

            if (category == null)
                return false;


            category.CategoryId = CategoryId;
            category.Name = Name;


            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }


        public bool AddOldCategory(string Name, int adminId)
        {

            var db = new TankshopDbContext();
            OldCategory oldCategory = new OldCategory();

            oldCategory.Name = Name;
            oldCategory.AdminId = adminId;
            oldCategory.Changed = DateTime.Now;

            db.OldCategories.Add(oldCategory);

            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public string GetCategoryName(int CategoryId)
        {

            Nettbutikk.Model.Category c = new TankshopDbContext().Categories.Find(CategoryId);

            return c == null ? null : c.Name;

        }

        public int FirstCategoryWithProducts()
        {
            using (var db = new TankshopDbContext())
            {
                var FirstCategoryWithProducts = db.Categories.Where(c => c.Products.Count > 0).FirstOrDefault();
                return FirstCategoryWithProducts == null ? 0 : FirstCategoryWithProducts.CategoryId;
            }

        }

    }

}