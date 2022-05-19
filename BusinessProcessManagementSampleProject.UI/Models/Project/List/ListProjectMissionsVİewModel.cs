﻿using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models;

namespace WebUI.Views.Project.List
{
    public class ListProjectMissionsVİewModel
    {
        public List<MissionModel> Missions { get; set; }
        public ActionResponse ActionResponse { get; set; }

        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
        public int ProjectId { get; set; }
    }
}
