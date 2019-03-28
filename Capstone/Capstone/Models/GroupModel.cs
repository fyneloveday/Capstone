﻿using System;
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
        [ForeignKey("MemberModel")]
        public int? MemberModelId { get; set; }
        public MemberModel MemberModel { get; set; }

    }
}