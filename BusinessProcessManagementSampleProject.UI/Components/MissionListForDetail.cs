using EntityLayer.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Views.Project.List;

namespace WebUI.Components
{
    public class MissionListForDetail : ViewComponent
    {
        public readonly IProjectHandler projectHandler;
        private int _pageSize = 6;
        public MissionListForDetail(IProjectHandler projectHandler)
        {
            this.projectHandler = projectHandler;
        }
        public IViewComponentResult Invoke(int projectId, int? page, string currentFilter, string filter)
        {
            if (filter != null)
                page = 1;
            else
                filter = currentFilter;

            ViewBag.CurrentFilter = filter;
            ViewBag.ProjectId = projectId;

            var tuple = projectHandler.GetMissionsOfProject(projectId, page ?? 1, _pageSize, filter);

            var viewModel = new ListProjectMissionsVİewModel { Missions = tuple.Item1, ActionResponse = null, PageCount = tuple.Item2, CurrentPageIndex = Convert.ToInt32(page), ProjectId = projectId };

            return View(viewModel);
        }
    }
}
