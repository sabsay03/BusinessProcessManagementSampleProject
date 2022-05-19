using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Project
{
    public class ListProjectsViewModel
    {
        public IPagedList<ProjectModel> Projects { get; set; }
        public ActionResponse ActionResponse { get; set; }

    }
}
