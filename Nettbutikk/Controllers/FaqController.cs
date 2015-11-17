using BLL.Category;
using BLL.FAQ;
using Nettbutikk.Model;
using Nettbutikk.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Nettbutikk.Controllers
{
    public class FAQController : Controller
    {
        private IFAQLogic _faqBLL;

        public FAQController()
        {
            _faqBLL = new FAQBLL();
        }

        // GET: FAQ
        public ActionResult Index()
        {
            ViewBag.Categories = _faqBLL.GetAllCategories();
            return View("FAQ");
        }

        [HttpPost]
        public JsonResult VoteUp(int id)
        {
            _faqBLL.VoteUp(id);
            var category = _faqBLL.getCategoryByFAQ(id);
            var json = Json(_faqBLL.GetFAQs(category.Id));
            return json;
        }

        [HttpPost]
        public JsonResult VoteDown(int id)
        {
            _faqBLL.VoteDown(id);
            var category = _faqBLL.getCategoryByFAQ(id);
            var json = Json(_faqBLL.GetFAQs(category.Id));
            return json;
        }
    }
}