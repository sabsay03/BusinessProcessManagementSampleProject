using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.Project
{
    public class ProjectDetailViewModel
    {
        public ProjectModel Project { get; set; }

        public ListProjectMemberViewModel Members { get; set; }
        public ActionResponse ActionResponse { get; set; }
    }
}
