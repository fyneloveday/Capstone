using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class BookEntryModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Author First Name")]
        public string AuthorFirstName { get; set; }
        [Display(Name = "Author Middle Name")]
        public string AuthorMiddleName { get; set; }
        [Required]
        [Display(Name = "Author Last Name")]
        public string AuthorLastName { get; set; }
        public int YearPublished { get; set; }
        public int ISBN { get; set; }
        [Required]
        [Display(Name = "Publisher")]
        public string Publisher { get; set; }
        [Display(Name = "Synopsis")]
        public string Synopsis { get; set; }
        [Range(0, 9)]
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}