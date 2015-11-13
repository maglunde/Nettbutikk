using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class CategoryView
    {
        [DisplayName("Kategorinummer")]
        public int CategoryId { get; set; }
        [DisplayName("Kategori")]
        public string CategoryName { get; set; }
    }
}