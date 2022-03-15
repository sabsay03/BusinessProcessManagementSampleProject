using EntityLayer.Concrete.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
   public class Project
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Explanation { get; set; }
        public int ManagerId { get; set; }
        public string Status { get; set; }
        public User Manager { get; set; }


        public ICollection<ProjectMember> ProjectMembers { get; set; }

        public IList<Mission> Tasks { get; set; }


    }
}
