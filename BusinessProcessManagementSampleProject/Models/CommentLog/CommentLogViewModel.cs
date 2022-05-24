using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.CommentLog
{
    public class CommentLogViewModel
    {
       public List<EntityLayer.Concrete.CommentLog> ProjectsLog { get; set; }
        public List<EntityLayer.Concrete.CommentLog> MissionsLog { get; set; }


    }
}
