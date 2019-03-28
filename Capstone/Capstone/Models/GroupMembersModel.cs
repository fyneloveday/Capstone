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
        [ForeignKey("MemberModel")]
        public int GroupAdmin { get; set; }
        public MemberModel MemberModel { get; set; }
        public List<MemberModel> GroupMembers { get; set; }
    }
}