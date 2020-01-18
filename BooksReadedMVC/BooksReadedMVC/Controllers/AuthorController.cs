using BooksReadedMVC.Models;
using System.Web.Mvc;
using System.Linq;

namespace BooksReadedMVC.Controllers
{
    public class AuthorController : Controller
    {
        // GET: Authors
        public ActionResult Index(string sortOrder, string search)
        {
            using(AuthorModel model = new AuthorModel())
            {
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

                return !string.IsNullOrWhiteSpace(search)
                    ? View(model.GetList().Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList())
                    : string.Equals(sortOrder, "name_desc")
                        ? View(model.GetList().OrderByDescending(x => x.Name).ToList())
                        : View(model.GetList().OrderBy(x => x.Name).ToList());
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