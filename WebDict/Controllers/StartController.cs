using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebDict.Services;

namespace WebDict.Controllers
{
    public class StartController : Controller
    {
        static DictIndexParser parser = new DictIndexParser();

        // GET: Start
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SetDictionary()
        {
            parser.SetDictionary(@"C:\Users\Alex\Desktop\dictionary666.txt");
            parser.SetIndex(@"C:\Users\Alex\Desktop\index666.txt");

            return View();
        }

        public ActionResult Results(string query)
        {
            //int[] ar = {1, 2, 3};
            ViewBag.Books = parser.Search(query);

            return View();
        }

        public ActionResult Words()
        {
            ViewBag.Bla = parser.Index;

            return View();
        }
    }
}