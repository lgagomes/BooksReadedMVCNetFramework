using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BooksReadedMVC.Models
{
    public class BookModel : IDisposable
    {
        private readonly SqlConnection _connection;

        public BookModel()
        {
            _connection = new SqlConnection("Data Source=DESKTOP-A8U9VRT;Initial Catalog=Books;User Id=admin;Password=123");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }

        public List<Book> GetList()
        {
            List<Book> books = new List<Book>();

            try
            {
                using (_connection)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(Resources.Book.GetBookBase, _connection);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                books.Add(FillBookByReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at BookRepository, 'GetList' method {Environment.NewLine}{ex.Message}",
                    ex.InnerException);
            }
            return books;
        }

        private Book FillBookByReader(SqlDataReader dataReader)
        {
            return new Book
            {
                IdBook = (int)dataReader["IdBook"],
                Title = (string)dataReader["Title"],
                YearPublication = (string)dataReader["YearPublication"],
                Author = new Author
                {
                    IdAuthor = (int)dataReader["IdAuthor"],
                    Name = (string)dataReader["Name"]
                }
            };
        }
    }
}