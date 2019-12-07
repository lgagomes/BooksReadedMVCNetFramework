using System.Collections.Generic;

namespace BooksReadedMVC.Models
{
    public interface IModel<TEntity> where TEntity : class
    {
        TEntity GetById(int Id);
        List<TEntity> GetList();
        void Save(TEntity entity);
        void Edit(TEntity entity);
    }
}
