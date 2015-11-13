using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class ProductView
    {
        [DisplayName("Produkt nr")]
        public int ProductId { get; set; }
        [DisplayName("Produkt")]
        public string ProductName { get; set; }
        [DisplayName("Pris")]
        public double Price { get; set; }
        [DisplayName("Beskrivelse")]
        public string Description { get; set; }
        [DisplayName("Antall på lager")]
        public int Stock { get; set; }
        [DisplayName("Kategori nr")]
        public int CategoryId { get;  set; }
        [DisplayName("Kategori")]
        public string CategoryName { get; set; }
        public List<ImageView> Images { get; set; }
    }
}