
using System;

namespace Nettbutikk.Model
{
    public class QuestionModel
    {
        public int Id { get; set; }
        public int FAQCategoryId { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
