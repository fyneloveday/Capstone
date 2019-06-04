using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class GroupMembersViewModel
    {
        public IEnumerable<MemberModel> Members { get; set; }

        public int GroupId { get; set; }
    }
}