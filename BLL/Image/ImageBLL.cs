using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace BLL.Image
{
    public class ImageBLL : IImageLogic
    {

        private DAL.Image.IImageRepo repo;

        public ImageBLL() {
            repo = new DAL.Image.ImageRepo();
        }

        public ImageBLL(DAL.Image.IImageRepo repo) {
            this.repo = repo;
        }

        public bool AddImage(int productId, string imageUrl)
        {
            return repo.AddImage(productId,imageUrl);
        }

        public bool DeleteImage(int imageId, int adminId)
        {
            Nettbutikk.Model.Image img = repo.GetImage(imageId);

            if (img == null)
                return false;

            if (!repo.AddOldImage(img.ProductId, img.ImageUrl, adminId))
                return false; 

            return repo.DeleteImage(imageId);
        }

        public List<Nettbutikk.Model.Image> GetAllImages()
        {
            return repo.GetAllImages();
        }

        public Nettbutikk.Model.Image GetImage(int imageId)
        {
            return repo.GetImage(imageId);
        }

        public bool UpdateImage(int imageId, int productId, string imageUrl, int adminId)
        {

            Nettbutikk.Model.Image img = repo.GetImage(imageId);

            if (img == null)
                return false;

            if (!repo.AddOldImage(img.ProductId, img.ImageUrl, adminId))
                return false;

            return repo.UpdateImage(imageId, productId, imageUrl);
        }
    }
}
