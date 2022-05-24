using BusinessLayer.Helpers;
using BusinessProcessManagementSampleProject.UI.Models;
using BusinessProcessManagementSampleProject.UI.Models.Project;
using EntityLayer.Handler;
using EntityLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using WebUI.Models.Project;

namespace BusinessProcessManagementSampleProject.UI.Controllers
{
    [AllowAnonymous]
    public class ProjectController : BaseController
    {
        public readonly IProjectHandler projectHandler;
        public readonly IUserHandler userHandler;
        private readonly DropDownHelper dropDownHelper;

        public ProjectController(IProjectHandler projectHandler, IUserHandler userHandler, DropDownHelper dropDownHelper)
        {
            this.projectHandler = projectHandler;
            this.userHandler = userHandler;
            this.dropDownHelper = dropDownHelper;
        }

        [HttpGet]
        public ActionResult CreateProjectRequest()
        {
            ViewBag.TeacherSelectList = dropDownHelper.GetAllTeacherSelectList();
            ViewBag.ProjectSelectList = dropDownHelper.ProjectSelectListByManager(null);
            return View();
        }

        [HttpPost]
        public JsonResult GetProjectsDropdownValues(Request request)
        {
            var projects = dropDownHelper.ProjectSelectListByManager(request.id);
            return Json(projects);
        }
        //[HttpGet]

        [HttpPost]
        public ActionResult CreateProjectRequest(ProjectRequestCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var projectRequestModel = new ProjectRequestModel { 
                MemberId=GetCurrentId(),
                ProjecId=model.ProjecId,
                ManagerId=model.ManagerId,
                ProjectRequestStatus=EntityLayer.Enums.ProjectRequestStatus.Waiting
                };

                var response = projectHandler.CreateProjectRequest(projectRequestModel);
                TempData["messageCreateOrEdit"] = response.Message;
                TempData["successCreateOrEdit"] = response.Success;
                return View();
            }
            ModelState.AddModelError("", "Talep Başarısız.");
            return View(model);
        }

        public ActionResult Index(int? page, string currentFilter, string name)
        {

            ViewBag.name = name;
            var projects = projectHandler.GetProjectsForMember(GetCurrentId(), page ?? 1, _pageSize, name);

            if (name != null)
                page = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var viewModel = new ListProjectsViewModel { Projects = projects,Id=GetCurrentId()};

            return View(viewModel);
        }

        public ActionResult Detail(int projectId)
        {
            var project = projectHandler.GetProjectDetailById(projectId);
            ViewBag.StudentList = dropDownHelper.GetProjectMember(projectId);

            var viewModel = new ProjectDetailViewModel { Project = project };

            return View(viewModel);
        }
        public IActionResult GetMissionsListForDetail(int projectId, int page)
        {
            return ViewComponent("MissionListForDetail", new { projectId = projectId, page = page, pagesize = _pageSize });
        }
        public IActionResult GetMembersListForDetail(int projectId, int page)
        {
            return ViewComponent("MembersListForDetail", new { projectId = projectId, page = page, pagesize = _pageSize });
        }
    }
}
