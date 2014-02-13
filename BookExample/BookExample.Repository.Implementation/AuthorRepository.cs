namespace BookExample.Repository.Implementation
{
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;

    using BookExample.Data;
    using BookExample.Models;
    using BookExample.Repository.Abstract;

    public class AuthorRepository : IAuthorRepository
    {
        private BookExampleContext context;

        public AuthorRepository(BookExampleContext context)
        {
            this.context = context;
        }

        public IQueryable<Author> Get()
        {
            return this.context.Authors;
        }

        public Author Get(int id)
        {
            return this.context.Authors.Find(id);
        }

        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                var author = this.context.Authors.Find(id);
                this.context.Authors.Remove(author);
                this.context.SaveChanges();

                scope.Complete();
            }
        }

        public void Update(Author author)
        {
            using (var scope = new TransactionScope())
            {
                this.context.Entry(author).State = EntityState.Modified;
                this.context.SaveChanges();

                scope.Complete();
            }
        }

        public void Add(Author author)
        {
            using (var scope = new TransactionScope())
            {
                this.context.Authors.Add(author);
                this.context.SaveChanges();

                scope.Complete();
            }
        }
    }
}
