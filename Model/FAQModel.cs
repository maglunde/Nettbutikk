namespace Nettbutikk.Model
{
    public class FAQModel
    {
        public int Id { get; set; }
        public int FAQCategoryId { get; set; }
        public string FAQCategoryName { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int Score { get; set; }
    }
}
