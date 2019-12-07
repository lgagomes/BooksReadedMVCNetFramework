using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BooksReadedMVC.Models
{
    public class AuthorModel : IDisposable
    {
        private readonly SqlConnection _connection;

        public AuthorModel()
        {
            _connection = new SqlConnection("Data Source=DESKTOP-A8U9VRT;Initial Catalog=Books;User Id=admin;Password=123");
            _connection.Open();
        }

        public void Dispose()
        {
            _connection.Close();
        }
        
        public List<Author> GetList()
        {
            List<Author> authors = new List<Author>();
            try
            {
                using (_connection)
                {
                    if (_connection.State == ConnectionState.Open)
                    {
                        SqlCommand sqlCommand = new SqlCommand(Resources.Author.GetAuthorBase, _connection);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                authors.Add(FillAuthorByReader(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error at AuthorRepository, 'GetList' method {Environment.NewLine}{ex.Message}",
                    ex.InnerException);
            }
            return authors;
        }

        private Author FillAuthorByReader(SqlDataReader dataReader)
        {
            return new Author
            {
                IdAuthor = (int)dataReader["IdAuthor"],
                Name = (string)dataReader["Name"]
            };
        }
    }
}