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
        public bool AddFAQ(FAQModel f)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newFAQ = new Nettbutikk.Model.FAQ
                    {
                        Answer = f.Answer,
                        FAQCategoryId = f.FAQCategoryId,
                        Question = f.Question,
                        Score = 0
                    };

                    db.FAQs.Add(newFAQ);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool AddPendingQuestion(QuestionModel question)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var newQuestion = new PendingQuestion()
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

        public List<QuestionModel> AllPendingQuestions()
        {
            using (var db = new TankshopDbContext())
            {
                return db.UserQuestions.Select(q => new QuestionModel()
                {
                    Answer = q.Answer,
                    Date = q.Date,
                    Email = q.Email,
                    Id = q.QuestionId,
                    Question = q.Question,
                    FAQCategoryId = q.FAQCategoryId

                }).ToList();
            }
        }

        public bool DeleteFAQ(int id)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    db.FAQs.Remove(db.FAQs.Find(id));
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool DeletePendingQuestion(int id)
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

        public FAQCategoryModel GetCategoryByFAQ(int id)
        {
            using (var db = new TankshopDbContext())
            {
                var FAQCategoryId = db.FAQs.Find(id).FAQCategoryId;
                return db.FAQCategories.Where(c => c.FAQCategoryId == FAQCategoryId).Select(c => new FAQCategoryModel
                {
                    Id = c.FAQCategoryId,
                    Name = c.Name
                }).Single();

            }
        }

        public FAQModel GetFAQ(int id)
        {
            throw new NotImplementedException();
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
                        FAQCategoryId = dbFaq.FAQCategory.FAQCategoryId,
                        FAQCategoryName = dbFaq.FAQCategory.Name,
                        Question = dbFaq.Question,
                        Answer = dbFaq.Answer,
                        Score = dbFaq.Score
                    };
                    faqs.Add(faq);
                }
                return faqs;
            }
        }

        public List<FAQModel> GetFAQs(int categoryid)
        {
            using (var db = new TankshopDbContext())
            {
                var dbFaqs = db.FAQs.Where(f => f.FAQCategoryId == categoryid).OrderByDescending(q => q.Score).ToList();
                var faqs = new List<FAQModel>();

                foreach (var dbFaq in dbFaqs)
                {
                    var faq = new FAQModel()
                    {
                        Id = dbFaq.FAQId,
                        FAQCategoryId = dbFaq.FAQCategory.FAQCategoryId,
                        FAQCategoryName = dbFaq.FAQCategory.Name,
                        Question = dbFaq.Question,
                        Answer = dbFaq.Answer,
                        Score = dbFaq.Score
                    };
                    faqs.Add(faq);
                }
                return faqs;
            }
        }

        public bool UpdateFAQ(int id, FAQModel faq)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var f = db.FAQs.Find(id);
                    if (f == null)
                        return false;

                    f.Answer = faq.Answer;
                    f.FAQCategoryId = faq.FAQCategoryId;
                    f.Question = faq.Question;
                    f.Score = faq.Score;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool UpdatePendingQuestion(int id, QuestionModel question)
        {
            using (var db = new TankshopDbContext())
            {
                try
                {
                    var dbQuestion = db.UserQuestions.Find(id);
                    dbQuestion.Answer = question.Answer;
                    dbQuestion.FAQCategoryId = question.FAQCategoryId;
                    dbQuestion.Question = question.Question;
                    dbQuestion.Date = question.Date;
                    dbQuestion.Email = question.Email;

                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
