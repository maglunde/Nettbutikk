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
        List<FAQModel> AddFAQ(FAQModel faq);
        List<FAQModel> GetAllFAQs();
        FAQModel GetFAQ(int id);
        List<FAQModel> UpdateFAQ(int id, FAQModel faq);
        List<FAQModel> DeleteFAQ(int id);
        bool AddUserQuestion(UserQuestionModel question);
        List<UserQuestionModel> AllUserQuestions();
        bool DeleteUserQuestion(int id);
        List<FAQCategoryModel> GetAllCategories();
    }
}
