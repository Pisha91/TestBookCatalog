namespace BookExample.Data
{
    using System.Data.Entity;

    using BookExample.Models;

    public class BookExampleContext : DbContext
    {
        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Book> Books { get; set; } 

        public BookExampleContext()
            : base("BookExampleDB")
        {
            
        }

    }
}
