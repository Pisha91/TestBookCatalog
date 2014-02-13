namespace BookExample.Repository.Abstract
{
    using System.Linq;

    using BookExample.Models;

    public interface IAuthorRepository
    {
        IQueryable<Author> Get();

        Author Get(int id);

        void Add(Author author);

        void Update(Author author);

        void Delete(int id);
    }
}
