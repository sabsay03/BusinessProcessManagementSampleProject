using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Relations
{
    public class GroupMember
    {
        public int MemberId { get; set; }
        public User Member { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

    }
}
