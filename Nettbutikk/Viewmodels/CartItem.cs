using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class CartItem
    {
        [DisplayName("Produktnummer")]
        public int ProductId { get; set; }
        [DisplayName("Produkt")]
        public string Name { get; set; }
        [DisplayName("Pris")]
        public double Price { get; set; }
        [DisplayName("Antall")]
        public int Count { get; set; }
    }
}