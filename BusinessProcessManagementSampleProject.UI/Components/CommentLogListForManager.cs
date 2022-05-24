using EntityLayer.Handler;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Models.CommentLog;

namespace WebUI.Components
{
    public class CommentLogListForManager : ViewComponent
    {
        public readonly IProjectHandler projectHandler;
        private readonly ICommentLogHandler commentLogHandler;
        private int _pageSize = 6;
        public CommentLogListForManager(IProjectHandler projectHandler, ICommentLogHandler commentLogHandler)
        {
            this.projectHandler = projectHandler;
            this.commentLogHandler = commentLogHandler;
        }
        public IViewComponentResult Invoke(int memberId)
        {

            var projectlogs = commentLogHandler.GetProjectCommentLogForMember(memberId);
            var missionLog = commentLogHandler.GetMissionCommentLogForMember(memberId);

            var viewModel = new CommentLogViewModel { ProjectsLog = projectlogs, MissionsLog = missionLog };
            return View(viewModel);
        }


    }
}
