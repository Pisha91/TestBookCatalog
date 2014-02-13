namespace BookExample.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Isnb { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public int ReleseYear { get; set; }

        public List<int> SelectedAuthors { get; set; }

        public IEnumerable<AuthorViewModel> Authors { get; set; }

        public IEnumerable<SelectListItem> AvailableReleseYears { get; set; }

        [DisplayName("Authors")]
        public string AuthorsString
        {
            get
            {
                return this.Authors == null ? string.Empty : string.Join(", ", this.Authors.Select(x => x.FullName));
            }
        }
    }
}
