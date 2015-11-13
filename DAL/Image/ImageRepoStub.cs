using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.Image
{
    public class ImageRepoStub : IImageRepo
    {
        public bool AddImage(int productId, string imageUrl)
        {
            return productId != -1;
        }

        public bool AddOldImage(int productId, string imageUrl, int adminId)
        {
            return productId != -1;
        }

        public bool DeleteImage(int imageId)
        {
            return imageId != -1;
        }

        public List<Nettbutikk.Model.Image> GetAllImages()
        {
            var allImages = new List<Nettbutikk.Model.Image> {
                new Nettbutikk.Model.Image { ImageId = 1, ProductId = 1, ImageUrl = "test1"},
                new Nettbutikk.Model.Image { ImageId = 2, ProductId = 2, ImageUrl = "test2"},
                new Nettbutikk.Model.Image { ImageId = 3, ProductId = 3, ImageUrl = "test3"},
                new Nettbutikk.Model.Image { ImageId = 4, ProductId = 4, ImageUrl = "test4"}
            };

            return allImages;
        }


        public Nettbutikk.Model.Image GetImage(int imageId)
        {
            
            return imageId == -1 ? null : new Nettbutikk.Model.Image { ImageId = imageId, ProductId = 1, ImageUrl = "test"};
        }


        public bool UpdateImage(int imageId, int productId, string imageUrl)
        {

            return imageId != -1 && productId != -1; 
           
        }

    }
}
