using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class GroupModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Group Name")]
        public string GroupName { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Rules")]
        public string Rules { get; set; }
        [Display(Name = "Reading Assignment")]
        public string ReadingAssignment { get; set; }
        public MemberModel Members { get; set; }
    
        [ForeignKey("MemberModel")]
        public int GroupAdminId { get; set; }
        public virtual MemberModel MemberModel { get; set; }
    }
}