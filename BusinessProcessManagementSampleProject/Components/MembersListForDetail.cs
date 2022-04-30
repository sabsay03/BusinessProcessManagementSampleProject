using BusinessLayer.Handler;
using BusinessProcessManagementSampleProject.Models.Project;
using EntityLayer.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Components
{
    public class MembersListForDetail:ViewComponent
    {
        public  readonly IProjectHandler projectHandler;
        private int _pageSize = 6;
        public MembersListForDetail(IProjectHandler projectHandler)
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

            var tuple = projectHandler.GetMembersOfProject(projectId, page ?? 1, _pageSize, filter);

            var viewModel = new ListProjectMemberViewModel { Members = tuple.Item1, ActionResponse = null, PageCount=tuple.Item2, CurrentPageIndex =Convert.ToInt32(page)};

            return View(viewModel);
        }
    }
}
