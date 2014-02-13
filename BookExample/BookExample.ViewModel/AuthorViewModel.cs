namespace BookExample.ViewModel
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class AuthorViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Full name")]
        public string FullName { get; set; }

        [Required]
        [DisplayName("Year of birth")]
        public int BirthYear { get; set; }

        [DisplayName("Year of deth")]
        public int? DeathYear { get; set; }

        public IEnumerable<SelectListItem> AvailableBirthYears { get; set; }

        public IEnumerable<SelectListItem> AvailableDeathYears { get; set; }
    }
}
