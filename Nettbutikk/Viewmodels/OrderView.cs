using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Nettbutikk.Viewmodels
{
    public class OrderView
    {
        [DisplayName("Ordrenummer")]
        public int OrderId { get; set; }
        [DisplayName("Kundenummer")]
        public int CustomerId { get; set; }
        [DisplayName("Dato")]
        public DateTime Date { get; set; }
        [DisplayName("Ordrelinjer")]
        public List<OrderlineView> Orderlines { get; set; }
    }

    public class OrderlineView
    {
        [DisplayName("Ordrelinjenummer")]
        public int OrderlineId { get; set; }
        [DisplayName("Produkt")]
        public ProductView Product{ get; set; }
        [DisplayName("Antall")]
        public int Count { get; set; }
    }
}