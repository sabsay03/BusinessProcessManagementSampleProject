using BusinessLayer.Handler;
using BusinessProcessManagementSampleProject.Models.CommentLog;
using BusinessProcessManagementSampleProject.Models.Project;
using EntityLayer.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Components
{
    public class CommentLogListForManager : ViewComponent
    {
        public readonly IProjectHandler projectHandler;
        private readonly ICommentLogHandler commentLogHandler;
        private int _pageSize = 6;
        public CommentLogListForManager(IProjectHandler projectHandler,ICommentLogHandler commentLogHandler)
        {
            this.projectHandler = projectHandler;
            this.commentLogHandler = commentLogHandler;
        }
        public IViewComponentResult Invoke(int managerId)
        {

            var projectlogs = commentLogHandler.GetProjectCommentLogForManager(managerId);
            var missionLog = commentLogHandler.GetMissionCommentLogForManager(managerId);

            var viewModel = new CommentLogViewModel { ProjectsLog=projectlogs,MissionsLog=missionLog};
            return View(viewModel);
        }


    }
}
