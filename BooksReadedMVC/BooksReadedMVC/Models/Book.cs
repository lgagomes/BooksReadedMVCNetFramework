namespace BooksReadedMVC.Models
{
    public class Book
    {
        public int IdBook { get; set; }
        public string Title { get; set; }
        public string YearPublication { get; set; }
        public Author Author { get; set; }
    }
}