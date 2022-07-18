using Library_Management.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Library_Management.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Books()
        {
            ViewBag.Message = "Books Page";

            Books book = new Books();
            book.BookId = 1;
            book.Load();
            //String name =book.BookName;
            ViewBag.Message = book.BookName;

            return View();


        }
    }
}