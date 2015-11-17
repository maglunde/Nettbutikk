using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nettbutikk.Model;
using DAL.FAQ;

namespace BLL.FAQ
{
    public class FAQBLL : IFAQLogic
    {

        private IFAQRepo _repo;

        public FAQBLL()
        {
            _repo = new FAQRepo();
        }

        public List<FAQModel> AddFAQ(FAQModel faq)
        {
            return _repo.AddFAQ(faq);
        }

        public bool AddUserQuestion(UserQuestionModel question)
        {
            return _repo.AddUserQuestion(question);
        }

        public List<UserQuestionModel> AllUserQuestions()
        {
            return _repo.AllUserQuestions();
        }

        public List<FAQModel> DeleteFAQ(int id)
        {
            return _repo.DeleteFAQ(id);
        }

        public bool DeleteUserQuestion(int id)
        {
            return _repo.DeleteUserQuestion(id);
        }

        public List<FAQCategoryModel> GetAllCategories()
        {
            return _repo.GetAllCategories();
        }

        public List<FAQModel> GetAllFAQs()
        {
            return _repo.GetAllFAQs();
        }

        public FAQModel GetFAQ(int id)
        {
            return _repo.GetFAQ(id);
        }

        public List<FAQModel> UpdateFAQ(int id, FAQModel faq)
        {
            return _repo.UpdateFAQ(id, faq);
        }
    }
}
