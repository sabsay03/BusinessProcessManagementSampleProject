using EntityLayer.Concrete.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }

        public int ProjectId { get; set; }
        public Project Poject { get; set; }

        public IList<GroupMember> GroupMembers { get; set; }
        public IList<Mission> Tasks { get; set; }


    }
}
