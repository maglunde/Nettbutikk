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
        // CRUD
        bool AddFAQ(FAQModel faq);
        List<FAQModel> GetFAQs();
        FAQModel GetFAQ(int id);
        List<FAQModel> UpdateFAQ(int id, FAQModel faq);
        bool DeleteFAQ(int id);
        bool AddUserQuestion(QuestionModel question);
        List<QuestionModel> AllUserQuestions();
        bool DeleteUserQuestion(int id);
        List<FAQCategoryModel> GetAllCategories();
        List<FAQModel> CategoryQuestions(int id);
        bool VoteUp(int id);
        bool VoteDown(int id);
        FAQCategoryModel getCategoryByFAQ(int id);
        List<FAQModel> GetFAQs(int categoryid);
        bool UpdateQuestion(int id, QuestionModel question);
    }
}
