using EntityLayer.Concrete.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class User
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public int TypeId { get; set; }
        public UserType Type { get; set; }

        //list

        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Mission> Tasks { get; set; }
        public ICollection<Comment> comments { get; set; }








    }
}
