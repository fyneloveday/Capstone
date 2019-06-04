using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public enum GroupMembershipStatus
    {
        [Display(Name = "Pending")]
        Pending,
        [Display(Name = "Accepted")]
        Done,
    }
}