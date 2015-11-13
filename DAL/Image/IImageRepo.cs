using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.Image
{
    public interface IImageRepo
    {

        bool AddImage(int productId, string imageUrl);
        bool UpdateImage(int imageId, int productId, string imageUrl);
        bool DeleteImage(int imageId);
        List<Nettbutikk.Model.Image> GetAllImages();
        Nettbutikk.Model.Image GetImage(int imageId);


        //OldImage
        bool AddOldImage(int productId, string imageUrl, int adminId);


    }
}
