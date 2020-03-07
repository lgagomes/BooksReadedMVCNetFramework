using BooksReadedMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace BooksReadedMVC.Controllers
{
    public class BookController : Controller
    {
        private const int ELEMENTS_PER_PAGE = 10;

        // GET: Books
        public ActionResult Index(string sortOrder, string titleSearch, string authorSearch, int? page)
        {
            // TODO: Keep title and author's name filter after applying a sort
            using (BookModel model = new BookModel())
            {
                ViewBag.ReadingOrderSortParam = string.IsNullOrWhiteSpace(sortOrder) ? "readingOrderDesc" : "";
                ViewBag.TitleSortParam = sortOrder == "titleAsc" ? "titleDesc" : "titleAsc";
                ViewBag.AuthorSortParam = sortOrder == "authorAsc" ? "authorDesc" : "authorAsc";
                ViewBag.PublicationYearSortParam = sortOrder == "publicationYearAsc" ? "publicationYearDesc" : "publicationYearAsc";

                List<Book> booksList = Sort(sortOrder, model.GetList());
                booksList = FilterByTitleAndAuthor(booksList, titleSearch, authorSearch);

                return View(booksList.ToPagedList(page ?? 1, ELEMENTS_PER_PAGE));
            }
        }

        private List<Book> Sort(string sortOrder, List<Book> booksList)
        {
            switch (sortOrder)
            {
                case "readingOrderDesc":    return booksList.OrderByDescending(x => x.IdBook).ToList();
                case "titleAsc":            return booksList.OrderBy(x => x.Title).ToList();
                case "titleDesc":           return booksList.OrderByDescending(x => x.Title).ToList();
                case "authorAsc":           return booksList.OrderBy(x => x.Author.Name).ToList();
                case "authorDesc":          return booksList.OrderByDescending(x => x.Author.Name).ToList();
                case "publicationYearAsc":  return booksList.OrderBy(x => x.YearPublication).ToList();
                case "publicationYearDesc": return booksList.OrderByDescending(x => x.YearPublication).ToList();
                default: return booksList;
            }
        }

        private List<Book> FilterByTitleAndAuthor(List<Book> booksListFiltered, string titleSearch, string authorSearch)
        {
            if (!string.IsNullOrWhiteSpace(titleSearch))
            {
                booksListFiltered = booksListFiltered
                    .Where(x => x.Title.ToUpper().Contains(titleSearch.ToUpper())).ToList();
            }

            if (!string.IsNullOrWhiteSpace(authorSearch))
            {
                booksListFiltered = booksListFiltered
                    .Where(x => x.Author.Name.ToUpper().Contains(authorSearch.ToUpper())).ToList();
            }

            return booksListFiltered;
        }
    }
}