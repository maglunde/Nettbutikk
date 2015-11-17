using BLL.Category;
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
        private ICategoryLogic _categoryBLL;

        public FAQController()
        {
            _categoryBLL = new CategoryBLL();
        }

        // GET: FAQ
        public ActionResult Index()
        {
            return View("FAQ");
        }
    }
}