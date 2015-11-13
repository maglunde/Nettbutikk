using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;

namespace DAL.FAQ
{
    public class FAQRepo : IFAQRepo
    {
        public List<FAQModel> AddFAQ(FAQModel faq)
        {
            throw new NotImplementedException();
        }

        public List<FAQModel> DeleteFAQ(int id)
        {
            throw new NotImplementedException();
        }

        public List<FAQModel> GetAllFAQs()
        {
            using (var db = new TankshopDbContext())
            {
                var dbFaqs = db.FAQs.ToList();
                var faqs = new List<FAQModel>();

                foreach(var dbFaq in dbFaqs)
                {
                    var faq = new FAQModel()
                    {
                        Question = dbFaq.Question,
                        Answer = dbFaq.Answer,
                        CategoryId = dbFaq.FAQCategory.FAQCategoryId,
                        CategoryName = dbFaq.FAQCategory.Name
                    };
                    faqs.Add(faq);
                }
                return faqs;
            }
        }

        public FAQModel GetFAQ(int id)
        {
            throw new NotImplementedException();
        }

        public List<FAQModel> UpdateFAQ(int id, FAQModel faq)
        {
            throw new NotImplementedException();
        }
    }
}
