using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Models.Project
{
    public class ListProjectMemberViewModel
    {
        public List<UserDetailedModel> Members { get; set; }
        public ActionResponse ActionResponse { get; set; }

        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}
