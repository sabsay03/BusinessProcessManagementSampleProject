using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete.Relations
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int ManagerId { get; set; }
        public DateTime CreateDate { get; set; }
        public virtual User User { get; set; }
        public virtual User Manager { get; set; }
        public virtual Project Project { get; set; }
    }
}
