using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Capstone.Models
{
    public class GroupMembersModel
    {
        [Key]
        public int Id { get; set; }

        public int GroupId { get; set; }
        [ForeignKey("GroupId")]
        public virtual GroupModel GroupModel { get; set; }
        public int MemberId { get; set; }
        [ForeignKey("MemberId")]
        public virtual MemberModel MemberModel { get; set; }
        public GroupMembershipStatus GroupMembershipStatus { get; set; }
    }
}