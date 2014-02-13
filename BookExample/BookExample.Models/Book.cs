namespace BookExample.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security;

    public class Book
    {
        public int Id { get; set; }

        [Required]
        public string Isnb { get; set; }

        [Required]
        public string Title { get; set; }

        public int ReleseYear { get; set; }

        public virtual ICollection<Author> Authors { get; set; } 
    }
}
