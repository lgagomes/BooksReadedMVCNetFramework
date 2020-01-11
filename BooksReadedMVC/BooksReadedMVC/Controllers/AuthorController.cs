using BooksReadedMVC.Models;
using System.Web.Mvc;

namespace BooksReadedMVC.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Authors
        public ActionResult Index()
        {
            using(AuthorModel model = new AuthorModel())
            {
                return View(model.GetList());
            }            
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Author author)
        {
            using (AuthorModel model = new AuthorModel())
            {
                if (ModelState.IsValid)
                {
                    model.Create(author);
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
            }
        }
    }
}