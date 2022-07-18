using Library_Management.DAL;
using System.Web.Mvc;
using Library_Management.Models;
using System;

namespace Library_Management.Controllers
{
    public class IssueBooksController : Controller
    {
        // GET: IssueBooks
        public ActionResult IssueBook()
        {
            IssueBooks issueobj = new IssueBooks();
            BooksClass booksclassobj = new BooksClass();
            booksclassobj.categorieslist = new Categories().CategoriesGetList();
            booksclassobj.publisherslist = new Publishers().PublishersGetList();
            booksclassobj.PageSize = 5;
            return View(booksclassobj);
        }

        [HttpPost]
        public ActionResult IssueBook(IssueBooks bookobj)
        {
            IssueBooks issueobj = new IssueBooks();
            Books bookObj = new Books();
            BooksClass b = new BooksClass();
            issueobj.BookId = bookobj.BookId;
            issueobj.UserId = bookobj.UserId;
            issueobj.BookQuantity = bookobj.BookQuantity;
            issueobj.IssueDate = bookobj.IssueDate;
            b.bookslist = bookObj.BooksGetList();
            issueobj.Insert(); 
            return PartialView("_Issuebook", b);
          //  return View("_Issuebook", issueobj);
        }

        public ActionResult IssuedBooks(int id = 0)
        {
            int BookId = id;
            Books bookObj = new Books();
            IssueBooks issueobj = new IssueBooks();
            BooksClass booksclassobj = new BooksClass();
            if (BookId > 0)
            {
                bookObj.BookId = BookId;
                bookObj.Load();
                issueobj.BookId = bookObj.BookId;
                issueobj.BookName = bookObj.BookName;
                issueobj.CategoryName = bookObj.CategoryName;
                issueobj.PublisherName = bookObj.PublisherName;
                issueobj.IsActive = bookObj.IsActive;
            }
            return Json(issueobj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult InsertIssue(IssueBooks bookobj)
        {
            IssueBooks issueobj = new IssueBooks();
            Books bookObj = new Books();
            BooksClass b = new BooksClass();
            issueobj.BookId = bookobj.BookId;
            issueobj.UserId = bookobj.UserId;
            issueobj.BookQuantity = bookobj.BookQuantity;
            issueobj.IssueDate = bookobj.IssueDate;
            b.IssueBookList = issueobj.InsertBookList();
            issueobj.Insert();
            //return PartialView("_Issuebook", b);
            return View(issueobj);
        }
    }
}