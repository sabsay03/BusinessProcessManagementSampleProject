using EntityLayer.Concrete.Relations;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
        public string Description { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ManagerId { get; set; }
        public User Manager { get; set; }
        public ProjectStatus ProjectStatus { get; set; } 
        public DateTime CreateDate { get; set; }

        public string FilePath { get; set; }

        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<CommentLog> Comments { get; set; }

        public IList<Mission> Tasks { get; set; }


    }
}
