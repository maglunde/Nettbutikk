using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class ImageView
    {
        [DisplayName("Bilde id")]
        public int ImageId { get; set; }
        [DisplayName("Produktnummer")]
        public int ProductId { get; set; }
        [DisplayName("Bilde url")]
        public string ImageUrl { get; set; }
    }
}