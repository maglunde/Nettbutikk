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

        public bool AddUserQuestion(QuestionModel question)
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

        public List<QuestionModel> AllUserQuestions()
        {
            using (var db = new TankshopDbContext())
            {
                return db.UserQuestions.Select(q => new QuestionModel()
                {
                    Answer = q.Answer,
                    Date = q.Date,
                    Email = q.Email,
                    Id = q.QuestionId,
                    Question = q.Question

                }).ToList();
            }
        }

        public List<FAQModel> CategoryQuestions(int id)
        {
            List<FAQModel> CategoryQuestions = new List<FAQModel>();

            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbCAtQs = db.FAQs.Where(q => q.FAQCategory.FAQCategoryId == id).ToList();
                    foreach(var q in dbCAtQs)
                    {
                        var faq = new FAQModel()
                        {
                            Id = q.FAQId,
                            CategoryId = q.FAQCategory.FAQCategoryId,
                            CategoryName = q.FAQCategory.Name,
                            Question = q.Question,
                            Answer = q.Answer,
                            Score = q.Score
                        };

                        CategoryQuestions.Add(faq);
                    }

                    return CategoryQuestions;
                }
                catch(Exception)
                {
                    return CategoryQuestions;
                }
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

        public List<FAQModel> GetFAQs()
        {
            using (var db = new TankshopDbContext())
            {
                var dbFaqs = db.FAQs.OrderByDescending(q => q.Score).ToList();
                var faqs = new List<FAQModel>();

                foreach (var dbFaq in dbFaqs)
                {
                    var faq = new FAQModel()
                    {
                        Id = dbFaq.FAQId,
                        CategoryId = dbFaq.FAQCategory.FAQCategoryId,
                        CategoryName = dbFaq.FAQCategory.Name,
                        Question = dbFaq.Question,
                        Answer = dbFaq.Answer,
                        Score = dbFaq.Score
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

        public bool VoteUp(int id)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var faq = db.FAQs.Find(id);
                    if (faq == null) return false;
                    faq.Score++;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool VoteDown(int id)
        {
            using(var db = new TankshopDbContext())
            {
                try
                {
                    var faq = db.FAQs.Find(id);
                    if (faq == null) return false;
                    faq.Score--;
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public FAQCategoryModel getCategoryByFAQ(int id)
        {
            using (var db = new TankshopDbContext())
            {
                var FAQCategoryId= db.FAQs.Find(id).FAQCategoryId;
                return db.FAQCategories.Where(c => c.FAQCategoryId == FAQCategoryId).Select(c => new FAQCategoryModel
                {
                    Id = c.FAQCategoryId, 
                    Name = c.Name
                }).Single();

            }
        }

        public List<FAQModel> GetFAQs(int categoryid)
        {
            using (var db = new TankshopDbContext())
            {
                var dbFaqs = db.FAQs.Where(f => f.FAQCategoryId == categoryid).ToList();
                var faqs = new List<FAQModel>();

                foreach (var dbFaq in dbFaqs)
                {
                    var faq = new FAQModel()
                    {
                        Id = dbFaq.FAQId,
                        CategoryId = dbFaq.FAQCategory.FAQCategoryId,
                        CategoryName = dbFaq.FAQCategory.Name,
                        Question = dbFaq.Question,
                        Answer = dbFaq.Answer,
                        Score = dbFaq.Score
                    };
                    faqs.Add(faq);
                }
                return faqs;
            }
        }
    }
}
