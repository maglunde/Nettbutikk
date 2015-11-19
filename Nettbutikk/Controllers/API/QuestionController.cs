using BLL.FAQ;
using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nettbutikk.Controllers.API
{
    public class QuestionController : ApiController
    {
        private IFAQLogic _faqBLL;

        public QuestionController()
        {
            _faqBLL = new FAQBLL();
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var Questions = _faqBLL.AllUserQuestions();
            Questions.Reverse();

            return Request.CreateResponse(HttpStatusCode.OK, Questions);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            return Request.CreateResponse();
        }

        // POST api/<controller>
        public HttpResponseMessage Post(QuestionView question)
        {
            if (ModelState.IsValid)
            {
                var questionModel = new QuestionModel
                {
                    Question = question.Question,
                    Email = question.Email
                };

                if (_faqBLL.AddUserQuestion(questionModel))
                {
                    var Questions = _faqBLL.AllUserQuestions();
                    Questions.Reverse();

                    return Request.CreateResponse(HttpStatusCode.Created, Questions);
                }
                else
                {
                    Request.CreateResponse(HttpStatusCode.NotFound, "Kunne ikke sette inn nytt spørsmål");
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Kunne ikke sette inn nytt spørsmål");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, QuestionModel question)
        {
            if (_faqBLL.UpdateQuestion(id, question))
            {
                var questions = _faqBLL.AllUserQuestions();
                return Request.CreateResponse(HttpStatusCode.OK, questions);

            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,"Kunne ikke lagre oppdateringene");
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            if (_faqBLL.DeleteUserQuestion(id))
            {
                var Questions = _faqBLL.AllUserQuestions();
                Questions.Reverse();

                return Request.CreateResponse(HttpStatusCode.OK, Questions);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Kunne ikke slette spørsmål med id " + id);
        }
    }
}