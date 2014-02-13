namespace BookExample.Repository.Abstract
{
    using System.Linq;

    using BookExample.Models;

    public interface IBookRepository
    {
        IQueryable<Book> Get();

        Book Get(int id);

        void Add(Book book);

        void Update(Book book);

        void Delete(int id);
    }
}
