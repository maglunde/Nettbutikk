﻿using System;
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

        public bool AddFAQ(FAQModel faq)
        {
            return _repo.AddFAQ(faq);
        }

        public bool AddPendingQuestion(QuestionModel question)
        {
            return _repo.AddPendingQuestion(question);
        }

        public List<QuestionModel> AllPendingQuestions()
        {
            return _repo.AllPendingQuestions();
        }

        public bool DeleteFAQ(int id)
        {
            return _repo.DeleteFAQ(id);
        }

        public bool DeletePendingQuestion(int id)
        {
            return _repo.DeletePendingQuestion(id);
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

        public bool UpdateFAQ(int id, FAQModel faq)
        {
            return _repo.UpdateFAQ(id, faq);
        }

        public FAQCategoryModel GetCategoryByFAQ(int id)
        {
            return _repo.GetCategoryByFAQ(id);
        }

        public List<FAQModel> GetFAQs(int categoryid)
        {
            return _repo.GetFAQs(categoryid);
        }

        public bool UpdatePendingQuestion(int id, QuestionModel question)
        {
            return _repo.UpdatePendingQuestion(id, question);
        }
    }
}
