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
    public class CategoryController : ApiController
    {
        private IFAQLogic _faqBLL;

        public CategoryController()
        {
            _faqBLL = new FAQBLL();
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            List<FAQCategoryModel> Categories = _faqBLL.GetAllCategories();
            return Request.CreateResponse(HttpStatusCode.OK, Categories);
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            List<FAQModel> CategoryQuestions;

            if (id <= 0)
                CategoryQuestions = _faqBLL.GetFAQs();
            else
                CategoryQuestions = _faqBLL.GetFAQs(id);

            return Request.CreateResponse(HttpStatusCode.OK, CategoryQuestions);
        }

        // POST api/<controller>
        public HttpResponseMessage Post(QuestionView question)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody]string value)
        {
            return Request.CreateResponse();
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}