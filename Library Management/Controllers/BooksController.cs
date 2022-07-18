using Library_Management.DAL;
using System.Web.Mvc;
using Library_Management.Models;
using System;

namespace Library_Management.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult Books(BooksClass model)
        {         
            Books bookObj = new Books();            
            BooksClass booksclassobj = new BooksClass();
            BooksClass bookobjnew = (BooksClass)Session["Books"];
            booksclassobj.categorieslist = new Categories().CategoriesGetList();
            booksclassobj.publisherslist = new Publishers().PublishersGetList();
            //booksclassobj.categorieslist = bookObj.BooksGetList();

            if (bookobjnew != null)
            {
                model = (BooksClass)Session["Books"];
            }

            if (Session["Books"] != null) {
                booksclassobj.CategoryId = model.CategoryId;
                booksclassobj.PublisherId = model.PublisherId;
                booksclassobj.BookName = model.BookName;
                booksclassobj.PageNumber = model.PageNumber;
                booksclassobj.PageSize = model.PageSize;
            }
            Session["Books"] = null;
            return View(booksclassobj);          
        }

        [HttpPost]
        public ActionResult BooksJson(BooksClass model)
        {
            Books bookObj = new Books();
            BooksClass b = new BooksClass();
            b.categorieslist = new Categories().CategoriesGetList();
            b.publisherslist = new Publishers().PublishersGetList();

            bookObj.CategoryId = model.CategoryId;
            bookObj.PublisherId = model.PublisherId;
            bookObj.BookName = model.BookName;
            bookObj.PageNumber = model.PageNumber;
            bookObj.PageSize = model.PageSize;
            bookObj.publisherslist = b.publisherslist;
            bookObj.categorieslist = b.categorieslist;

            b.bookslist = bookObj.BooksGetList();
            Session["Books"] = model;
            b.PageSize = model.PageSize;
            b.PageNumber = model.PageNumber;
            b.TotalRecords = bookObj.TotalRecords;
            model.PageSize = bookObj.PageSize;
            b.count = Convert.ToInt32(b.bookslist[0].TotalRecords);
            b.TotalPages = (int)Math.Ceiling((double)b.count / b.PageSize);
            return PartialView("_BookList", b);
           // return Json(b, JsonRequestBehavior.AllowGet);
        }

        // GET: BooksInsert
        public ActionResult BooksInsert(int id = 0)
        {
            int BookId = id;
            Books bookObj = new Books();
            BooksClass booksclassobj = new BooksClass();
            booksclassobj.categorieslist = new Categories().CategoriesGetList();
            booksclassobj.publisherslist = new Publishers().PublishersGetList();
            bookObj.publisherslist = booksclassobj.publisherslist;
            bookObj.categorieslist = booksclassobj.categorieslist;

            if (BookId > 0)
            {
                bookObj.BookId = BookId;
                bookObj.Load();
                booksclassobj.BookId = bookObj.BookId;
                booksclassobj.BookName = bookObj.BookName;
                booksclassobj.CategoryId = bookObj.CategoryId;
                booksclassobj.PublisherId = bookObj.PublisherId;
                booksclassobj.IsActive = bookObj.IsActive;
            }
            else
            {
                booksclassobj.categorieslist = new Categories().CategoriesGetList();
                booksclassobj.publisherslist = new Publishers().PublishersGetList();
            }
            return View(booksclassobj);
        }

        public ActionResult BooksInsertModal(int id = 0)
        {
            int BookId = id;
            Books bookObj = new Books();
            BooksClass booksclassobj = new BooksClass();
            booksclassobj.categorieslist = new Categories().CategoriesGetList();
            booksclassobj.publisherslist = new Publishers().PublishersGetList();
            bookObj.publisherslist = booksclassobj.publisherslist;
            bookObj.categorieslist = booksclassobj.categorieslist;

            if (BookId > 0)
            {
                bookObj.BookId = BookId;
                bookObj.Load();
                booksclassobj.BookId = bookObj.BookId;
                booksclassobj.BookName = bookObj.BookName;
                booksclassobj.CategoryId = bookObj.CategoryId;
                booksclassobj.PublisherId = bookObj.PublisherId;
                booksclassobj.IsActive = bookObj.IsActive;
            }
            else
            {
                booksclassobj.categorieslist = new Categories().CategoriesGetList();
                booksclassobj.publisherslist = new Publishers().PublishersGetList();
            }
            return Json(booksclassobj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BooksInsert(Books bookobj)
        {
            Books bookObj = new Books();
            BooksClass booksclassobj = new BooksClass();
            booksclassobj.categorieslist = new Categories().CategoriesGetList();
            booksclassobj.publisherslist = new Publishers().PublishersGetList();

            bookObj.BookId = bookobj.BookId;
            bookObj.BookName = bookobj.BookName;
            bookObj.CategoryId = bookobj.CategoryId;
            bookObj.PublisherId = bookobj.PublisherId;
            bookObj.IsActive = bookobj.IsActive;
            bookObj.publisherslist = booksclassobj.publisherslist;
            bookObj.categorieslist = booksclassobj.categorieslist;

            bookObj.Save();
            return Json("Saved Successfully");

            //return View(bookObj);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Books bookObj = new Books();
            bookObj.BookId = id;
            bookObj.Delete();
            //return Json("Deleted successfully");
            return Json(bookObj,JsonRequestBehavior.AllowGet);
        }
    }
}