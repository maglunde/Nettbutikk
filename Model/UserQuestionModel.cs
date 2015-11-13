
using System;

namespace Nettbutikk.Model
{
    public class UserQuestionModel
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }

    }
}
