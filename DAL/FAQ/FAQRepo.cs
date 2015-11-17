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

        public bool AddUserQuestion(UserQuestionModel question)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newQuestion = new UserQuestion()
                    {
                        Question = question.Question,
                        Date = DateTime.Now,
                        Email = question.Email
                    };

                    db.UserQuestions.Add(newQuestion);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<UserQuestionModel> AllUserQuestions()
        {
            using (var db = new TankshopDbContext())
            {
                return db.UserQuestions.Select(q => new UserQuestionModel()
                {
                    Answer = q.Answer,
                    Date = q.Date,
                    Email = q.Email,
                    Id = q.QuestionId,
                    Question = q.Question

                }).ToList();
            }
        }

        public List<FAQModel> DeleteFAQ(int id)
        {
            throw new NotImplementedException();
        }

        public bool DeleteUserQuestion(int id)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbQuestion = db.UserQuestions.Find(id);
                    if (dbQuestion == null)
                        return false;
                    db.UserQuestions.Remove(dbQuestion);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }

        }

        public List<FAQCategoryModel> GetAllCategories()
        {
            using (var db = new TankshopDbContext())
            {
                return db.FAQCategories.Select(c => new FAQCategoryModel
                {
                    Id = c.FAQCategoryId,
                    Name = c.Name
                }).ToList();


            }
        }

        public List<FAQModel> GetAllFAQs()
        {
            using (var db = new TankshopDbContext())
            {
                var dbFaqs = db.FAQs.ToList();
                var faqs = new List<FAQModel>();

                foreach (var dbFaq in dbFaqs)
                {
                    var faq = new FAQModel()
                    {
                        Id = dbFaq.FAQId,
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
