namespace BookExample.Repository.Implementation
{
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Transactions;

    using BookExample.Data;
    using BookExample.Models;
    using BookExample.Repository.Abstract;

    public class BookRepository : IBookRepository
    {
        private readonly BookExampleContext context;

        public BookRepository(BookExampleContext context)
        {
            this.context = context;
        }

        public IQueryable<Book> Get()
        {
            return this.context.Books.Include(x => x.Authors);
        }

        public Book Get(int id)
        {
            return this.context.Books.Find(id);
        }

        public void Add(Book book)
        {
            using (var scope = new TransactionScope())
            {
                this.context.Books.Add(book);
                foreach (var author in book.Authors)
                {
                    this.context.Entry(author).State = EntityState.Unchanged;
                }

                this.context.SaveChanges();

                scope.Complete();
            }
           
        }

        public void Update(Book book)
        {
            using (var scope = new TransactionScope())
            {
                var currentBook = this.context.Books.Find(book.Id);
                var currentAuthors = this.context.Authors.ToList().Where(x => book.Authors.Any(y => y.Id == x.Id));

                currentBook.Authors.Clear();
                foreach (var currentAuthor in currentAuthors)
                {
                    currentBook.Authors.Add(currentAuthor);
                }

                this.context.SaveChanges();
                scope.Complete();
            }
        }

        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                var book = this.context.Books.Find(id);
                this.context.Books.Remove(book);
                this.context.SaveChanges();

                scope.Complete();
            }
        }
    }
}
