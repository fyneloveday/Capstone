using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class MemberModel
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        //[Required]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        public string Password { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime MemberSince { get; set; }
        [Display(Name = "Favorite Book")]
        public string FavoriteBook { get; set; }
        [Display(Name = "Rating")]
        public int Rating { get; set; }
        [Display(Name = "Currently Reading")]
        public string CurrentlyReading { get; set; }
        [Display(Name = "Reading Progress")]
        public int ProgressInBook { get; set; }
        [Display(Name = "About Yourself")]
        public string AboutYourself { get; set; }
        public GroupModel YourGroups { get; set; }
        public List<BookAPIModel> BookRating { get; set; }

        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}