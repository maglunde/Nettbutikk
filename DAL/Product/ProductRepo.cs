
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;
using Logging;

namespace DAL.Product
{
    public class ProductRepo : IProductRepo
    {

        public bool AddProduct(string Name, double Price, int Stock, string Description, int CategoryId)
        {
            var db = new TankshopDbContext();

            var newProduct = new Nettbutikk.Model.Product()
            {
                Name = Name,
                Price = Price,
                Stock = Stock,
                Description = Description,
                CategoryId = CategoryId
            };

            try {
                db.Products.Add(newProduct);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public bool AddOldProduct(string Name, double Price, int Stock, string Description, int CategoryId, int AdminId)
        {
            var db = new TankshopDbContext();
            OldProduct oldProduct = new OldProduct();

            oldProduct.Name = Name;
            oldProduct.Price = Price;
            oldProduct.Stock = Stock;
            oldProduct.Description = Description;
            oldProduct.CategoryId = CategoryId;

            oldProduct.AdminId = AdminId;
            oldProduct.Changed = DateTime.Now;

            db.OldProducts.Add(oldProduct);

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

        public bool DeleteProduct(int ProductId)
        {
            var db = new TankshopDbContext();

            Nettbutikk.Model.Product product = (from p in db.Products where p.ProductId == ProductId select p).FirstOrDefault();

            if (product == null)
                return false;


            try
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public List<ProductModel> GetAllProductModels()
        {
            var productModels = new List<ProductModel>();
            try
            {
                using (var db = new TankshopDbContext())
                {
                    var dbProducts = db.Products.ToList();

                    foreach (var product in dbProducts)
                    {
                        var dbProductImages = db.Images.Where(i => i.ProductId == product.ProductId).ToList();
                        var imageModels = new List<ImageModel>();

                        foreach (var image in dbProductImages)
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
                    return productModels;
                }
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                return productModels;
            }
        }

        public Nettbutikk.Model.Product GetProduct(int ProductId)
        {

            try
            {
                return new TankshopDbContext().Products.Find(ProductId);
            }
            catch (Exception e)
            {
                LogHandler.WriteToLog(e);
                return null;
            }

        }

        public ProductModel GetProductModel(int ProductId)
        {
            using (var db = new TankshopDbContext())
            {
                var product = db.Products.Find(ProductId);
                if (product == null)
                    return null;
                var dbProductImages = db.Images.Where(i => i.ProductId == product.ProductId).ToList();
                var imageModels = new List<ImageModel>();

                foreach (var image in dbProductImages)
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
                return productModel;
            }
        }

        public List<ProductModel> GetProductModels(string searchstr)
        {
            using (var db = new TankshopDbContext())
            {
                var productList = new List<ProductModel>();
                try
                {
                    var dbProducts = db.Products.Where(p => p.Name.Contains(searchstr)
                                                        || p.Category.Name.Contains(searchstr)
                                                        //|| p.Description.Contains(searchstr)
                                                        ).ToList();
                    foreach (var product in dbProducts)
                    {
                        var dbProductImages = db.Images.Where(i => i.ProductId == product.ProductId).ToList();
                        var imageModels = new List<ImageModel>();

                        foreach (var image in dbProductImages)
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

                        productList.Add(productModel);
                    }

                    return productList;
                }
                catch (Exception)
                {
                    return productList;
                }
            }
        }

        public List<ProductModel> GetProducts(List<int> productIdList)
        {
            var productList = new List<ProductModel>();
            try
            {
                using (var db = new TankshopDbContext())
                {

                    foreach (var productId in productIdList)
                    {
                        var product = db.Products.Find(productId);
                        if (product != null)
                        {
                            var dbProductImages = db.Images.Where(i => i.ProductId == product.ProductId).ToList();
                            var imageModels = new List<ImageModel>();

                            foreach (var image in dbProductImages)
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

                            productList.Add(productModel);
                        }
                    }

                    return productList;
                }
            }
            catch (Exception)
            {
                return productList;
            }
        }

        public List<ProductModel> GetProductsByCategory(int categoryId)
        {
            using (var db = new TankshopDbContext())
            {
                var dbProducts = db.Products.Where(p => p.CategoryId == categoryId).ToList();
                var productModels = new List<ProductModel>();
                foreach (var product in dbProducts)
                {
                    var dbProductImages = db.Images.Where(i => i.ProductId == product.ProductId).ToList();
                    var imageModels = new List<ImageModel>();

                    foreach (var image in dbProductImages)
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
                return productModels;

            }
        }

        public bool UpdateProduct(int ProductId, string Name, double Price, int Stock, string Description, int CategoryId)
        {
            var db = new TankshopDbContext();

            Nettbutikk.Model.Product product = (from p in db.Products where p.ProductId == ProductId select p).FirstOrDefault();
            
            if (product == null)
                return false;

            product.Name = Name;
            product.Price = Price;
            product.Stock = Stock;
            product.Description = Description;
            product.CategoryId = CategoryId;

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

        public List<Nettbutikk.Model.Product> GetAllProducts()
        {
            return new TankshopDbContext().Products.ToList();
        }

        public List<ProductModel> GetProducts(string searchstr)
        {
            return GetProductModels(searchstr);
        }
    }
}
