using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Project
{
    public class ProjectDetailViewModel
    {
        public ProjectModel Project { get; set; }
        public ActionResponse ActionResponse { get; set; }
    }
}
 