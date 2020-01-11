using System.ComponentModel.DataAnnotations;

namespace BooksReadedMVC.Models
{
    public class Author
    {
        public int IdAuthor { get; set; }

        [Required(ErrorMessage = "Preencha o nome.")]
        [MaxLength(100, ErrorMessage = "O nome pode ter no máximo 100 caracteres.")]
        public string Name { get; set; }
    }
}