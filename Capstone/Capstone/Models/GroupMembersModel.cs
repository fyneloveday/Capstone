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

        [ForeignKey("GroupModel")]
        public int GroupId { get; set; }
        public GroupModel GroupModel { get; set; }

        [ForeignKey("MemberModel")]
        public int MemberId { get; set; }
        public MemberModel MemberModel { get; set; }
    }
}