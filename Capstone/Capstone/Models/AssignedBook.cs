using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class AssignedBook
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Reading Assignment")]
        public string ReadingAssignment { get; set; }
        [Display(Name = "Author")]
        public string Author { get; set; }
        [Column(TypeName = "ntext")]
        public string Body { get; set; }
        public int BlogPostID { get; set; }
        public MemberModel Members { get; set; }
    }
}