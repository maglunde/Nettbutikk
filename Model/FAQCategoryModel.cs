using System.Collections.Generic;

namespace Nettbutikk.Model
{
   public class FAQCategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<FAQModel> Questions { get; set; }
    }
}
