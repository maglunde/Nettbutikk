using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;
using Logging;


namespace DAL.Image
{
    public class ImageRepo : IImageRepo
    {


        public Nettbutikk.Model.Image GetImage(int imageId)
        {
            try {
                return new TankshopDbContext().Images.FirstOrDefault(i => i.ImageId == imageId);
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
                return null;
            }

        }

        public List<Nettbutikk.Model.Image> GetAllImages()
        {

            try {
                return new TankshopDbContext().Images.OrderBy(i => i.ProductId).ToList();
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
                return new List<Nettbutikk.Model.Image>();
            }
            
        }


        public bool AddImage(int productId, string imageUrl)
        {

            try
            {
                var db = new TankshopDbContext();
                db.Images.Add(new Nettbutikk.Model.Image() { ProductId = productId, ImageUrl = imageUrl });
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
            }

            return false;
            
        }

        public bool DeleteImage(int imageId)
        {

            var db = new TankshopDbContext();

            Nettbutikk.Model.Image img = (from i in db.Images where i.ImageId == imageId select i).FirstOrDefault();

            if (img == null)
            {
                return false;
            }

            try {
                db.Images.Remove(img);
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
            }

            return false;
        }

        public bool UpdateImage(int imageId, int productId, string imageUrl)
        {

            var db = new TankshopDbContext();

            Nettbutikk.Model.Image img = (from i in db.Images where i.ImageId == imageId select i).FirstOrDefault();

            if (img == null)
            {
                return false;
            }

            img.ProductId = productId;
            img.ImageUrl = imageUrl;


            try {
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
            }

            return false;
        }


        //OldImage
        public bool AddOldImage(int productId, string imageUrl, int adminId)
        {

            var db = new TankshopDbContext();
            OldImage oldImage = new OldImage();

            oldImage.ProductId = productId;
            oldImage.ImageUrl = imageUrl;
            oldImage.AdminId = adminId;
            oldImage.Changed = DateTime.Now;

            db.OldImages.Add(oldImage);

            try {
                db.SaveChanges();
                return true;
            }
            catch (Exception e) {
                LogHandler.WriteToLog(e);
            }

            return false;
        }


    }
}
