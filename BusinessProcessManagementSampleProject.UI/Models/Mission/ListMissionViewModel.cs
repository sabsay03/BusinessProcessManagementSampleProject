using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models.Mission
{
    public class ListMissionViewModel
    {
        public IPagedList<MissionModel> Missions { get; set; }
        public ActionResponse ActionResponse { get; set; }

    }
}
