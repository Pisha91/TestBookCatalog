namespace BookExample.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Author
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        public int BirthYear { get; set; }

        public int? DeathYear { get; set; }

        public ICollection<Book> Books { get; set; } 
    }
}
