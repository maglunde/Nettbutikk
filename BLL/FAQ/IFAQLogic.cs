using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.FAQ
{
   public interface IFAQLogic
    {
        bool AddFAQ(FAQModel faq);
        bool AddPendingQuestion(QuestionModel question);
        List<QuestionModel> AllPendingQuestions();
        bool DeleteFAQ(int id);
        bool DeletePendingQuestion(int id);
        List<FAQCategoryModel> GetAllCategories();
        FAQCategoryModel GetCategoryByFAQ(int id);
        FAQModel GetFAQ(int id);
        List<FAQModel> GetFAQs();
        List<FAQModel> GetFAQs(int categoryid);
        bool UpdateFAQ(int id, FAQModel faq);
        bool UpdatePendingQuestion(int id, QuestionModel question);
    }
}
