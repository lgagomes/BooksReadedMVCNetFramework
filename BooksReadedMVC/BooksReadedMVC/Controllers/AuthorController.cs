using BooksReadedMVC.Models;
using System.Linq;
using System.Web.Mvc;
using X.PagedList;

namespace BooksReadedMVC.Controllers
{
    public class AuthorController : Controller
    {
        private const int ELEMENTS_PER_PAGE = 5;

        // GET: Authors
        public ActionResult Index(string sortOrder, string search, int? page)
        {
            using(AuthorModel model = new AuthorModel())
            {
                ViewBag.NameSortParm = string.IsNullOrEmpty(sortOrder) ? "nameDesc" : "";

                var authorsList = 
                    !string.IsNullOrWhiteSpace(search)
                        ? model.GetList().Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList()
                        : string.Equals(sortOrder, "nameDesc")
                            ? model.GetList().OrderByDescending(x => x.Name).ToList()
                            : model.GetList().OrderBy(x => x.Name).ToList();

                return View(authorsList.ToPagedList(page ?? 1, ELEMENTS_PER_PAGE));
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