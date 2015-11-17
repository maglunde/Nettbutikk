﻿namespace Nettbutikk.Model
{
    public class FAQModel
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }
    }
}
