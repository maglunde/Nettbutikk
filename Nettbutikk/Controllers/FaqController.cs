using BLL.FAQ;
using Nettbutikk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nettbutikk.Controllers
{
    public class FAQController : ApiController
    {
        private IFAQLogic _faqBLL;

        public FAQController()
        {
            _faqBLL = new FAQBLL();
        }

        // GET api/<controller>
        //public List<FAQModel> Get()
        public HttpResponseMessage Get()
        {
            var FAQs = _faqBLL.GetAllFAQs();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, FAQs);
            return response;
            //return FAQs;
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var FAQ = _faqBLL.GetFAQ(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, FAQ);
            return response;
        }

        // POST api/<controller>
        public HttpResponseMessage Post(FAQModel faq)
        {
            var FAQs = _faqBLL.AddFAQ(faq);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, FAQs);
            return response;
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, FAQModel faq)
        {
            var FAQs = _faqBLL.UpdateFAQ(id, faq);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, FAQs);
            return response;
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            var FAQs = _faqBLL.DeleteFAQ(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, FAQs);
            return response;
        }
    }
}