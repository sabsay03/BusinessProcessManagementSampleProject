using BusinessLayer.Helpers;
using BusinessProcessManagementSampleProject.UI.Models;
using BusinessProcessManagementSampleProject.UI.Models.Project;
using EntityLayer.Handler;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WebUI.Models.Mission;
using WebUI.Models.Project;

namespace WebUI.Controllers
{
    public class MissionController : BaseController
    {
        public readonly IProjectHandler projectHandler;
        public readonly IMissionHandler missionHandler; 
        public readonly IUserHandler userHandler;
        private readonly DropDownHelper dropDownHelper;

        public MissionController(IProjectHandler projectHandler, IUserHandler userHandler, DropDownHelper dropDownHelper,IMissionHandler missionHandler )
        {
            this.projectHandler = projectHandler;
            this.userHandler = userHandler;
            this.dropDownHelper = dropDownHelper;
            this.missionHandler = missionHandler;
        }
        public ActionResult Index(int? page, string currentFilter, string name)
        {

            ViewBag.name = name;
            var missions = missionHandler.GetMissionForMember(GetCurrentId(), page ?? 1, _pageSize, name);

            if (name != null)
                page = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var viewModel = new ListMissionViewModel { Missions = missions };

            return View(viewModel);
        }

        public ActionResult Detail(int missionId)
        {
            var mission = missionHandler.GetMissionDetailById(missionId);
            if (mission.StudentId == GetCurrentId())
            {
                var viewModel1 = new MissionDetailViewModel { Mission = mission };

                return View(viewModel1);
            }

            TempData["ProjectmessageCreateOrEdit"] = "Görev Size ait değil.";
            TempData["ProjectsuccessCreateOrEdit"] = false;

            return RedirectToAction("Detail","Project",new { projectId=mission.ProjectId });
        }
        public JsonResult SendMission(int missionId, string filePath)
        {
            var response = missionHandler.SendMission(missionId, filePath);

            TempData["messageCreateOrEdit"] = response.Message;
            TempData["successCreateOrEdit"] = response.Success;
            return Json(response);
        }
    }
}
