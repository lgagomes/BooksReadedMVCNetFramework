using BooksReadedMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BooksReadedMVC.Controllers
{
    public class BookController : Controller
    {
        // GET: Books
        public ActionResult Index()
        {
            using (BookModel model = new BookModel())
            {
                return View(model.GetList());
            }
        }
    }
}