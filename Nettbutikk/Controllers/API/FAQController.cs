﻿using BLL.FAQ;
using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nettbutikk.Controllers.Api
{
    public class FAQController : ApiController
    {
        private IFAQLogic _faqBLL;

        public FAQController()
        {
            _faqBLL = new FAQBLL();
        }

        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            var FAQs = _faqBLL.GetFAQs();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, FAQs);
            return response;
            //return FAQs;
        }

        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            var FAQ = _faqBLL.GetFAQ(id);
            return Request.CreateResponse(HttpStatusCode.OK, FAQ);
        }

        // POST api/<controller>
        public HttpResponseMessage Post(FAQModel question)
        {
            if (ModelState.IsValid && question.Answer != null && question.Answer != "")
            {
                if (_faqBLL.AddFAQ(question))
                {
                    return Request.CreateResponse(HttpStatusCode.Created, "Spørsmålet ble satt inn i databasen");
                }
                return Request.CreateResponse(HttpStatusCode.NotFound, "Kunne ikke sette inn i databasen");
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest,"Feil i modellen, kontroller data");
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, FAQModel faq)
        {
            var FAQs = _faqBLL.UpdateFAQ(id, faq);
            return Request.CreateResponse(HttpStatusCode.OK, FAQs);
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            if(_faqBLL.DeleteFAQ(id))
            {
                return Request.CreateResponse(HttpStatusCode.OK,"FAQ ble slettet fra databasen");
            }
            return Request.CreateResponse(HttpStatusCode.NotFound,"Kunne ikke slette FAQ fra databasen");
        }
    }
}