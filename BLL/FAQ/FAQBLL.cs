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

        public bool AddUserQuestion(QuestionModel question)
        {
            return _repo.AddUserQuestion(question);
        }

        public List<QuestionModel> AllUserQuestions()
        {
            return _repo.AllUserQuestions();
        }

        public List<FAQModel> CategoryQuestions(int id)
        {
            return _repo.CategoryQuestions(id);
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

        public List<FAQModel> GetFAQs()
        {
            return _repo.GetFAQs();
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
