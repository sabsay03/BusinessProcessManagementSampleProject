using BusinessLayer.Helpers;
using BusinessProcessManagementSampleProject.Models;
using BusinessProcessManagementSampleProject.Models.Project;
using EntityLayer.Handler;
using EntityLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace BusinessProcessManagementSampleProject.Controllers
{
    public class ProjectController : BaseController
    {
        public readonly IProjectHandler projectHandler;
        public readonly IUserHandler userHandler;
        private readonly DropDownHelper dropDownHelper;

        public ProjectController(IProjectHandler projectHandler,DropDownHelper dropDownHelper,IUserHandler userHandler):base()
        {
            this.projectHandler = projectHandler;
            this.dropDownHelper = dropDownHelper;
            this.userHandler = userHandler;
        }
        [HttpGet]
        public ActionResult CreateProjectMember()
        {
            ViewBag.ProjectSelectListItems = dropDownHelper.ProjectSelectListByManager(GetCurrentId());
            return View();
        }
        [HttpPost]
        public ActionResult CreateProjectMember(ProjectMemberCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var member = userHandler.GetUserByStudenNo(model.StudentNo);
                var projectMemberModel = new ProjectMemberModel { MemberId = member.Id, ProjecId = model.ProjecId };
                var response = projectHandler.AddStudenToProject(projectMemberModel);
                TempData["messageCreateOrEdit"] = response.Message;
                TempData["successCreateOrEdit"] = response.Success;
                return View();
            }
            ViewBag.ProjectSelectListItems = dropDownHelper.ProjectSelectListByManager(GetCurrentId());
            ModelState.AddModelError("", "Öğrenci Ekleme Başarısız.");
            return View(model);
        }

        public IActionResult GetMembersListForDetail(int projectId,int page)
        {
            return ViewComponent("MembersListForDetail",new {projectId=projectId,page=page,pagesize=_pageSize});
        }
        public IActionResult GetMissionsListForDetail(int projectId, int page)
        {
            return ViewComponent("MissionListForDetail", new { projectId = projectId, page = page, pagesize = _pageSize });
        }

        public ActionResult Index(int? page, string currentFilter, string name)
        {
            var currentState = GetCurrentActionState();
            ViewBag.name = name;
            var projects = projectHandler.GetProjectsForManager(GetCurrentId(), page ?? 1, _pageSize, name);

            if (name != null)
                page = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var viewModel = new ListProjectsViewModel { Projects = projects, ActionResponse=currentState };

            return View(viewModel);
        }

        // GET: ProjectController/Details/5
        public ActionResult Detail(int projectId)
        {
            var project=projectHandler.GetProjectDetailById(projectId);

            var viewModel = new ProjectDetailViewModel { Project = project};

            return View(viewModel);
        }
        // GET: ProjectController/Create
        public ActionResult CreateProject(int? id)
        {
            if (id != null)
            {
                var model = projectHandler.GetById(Convert.ToInt32(id));
                return View(new ProjectCreateViewModel
                {
                    Id = model.Id,
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    FilePath=model.FilePath
                });

            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateProject(ProjectCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id != null)
                {
                    var project = new ProjectModel
                    {
                        Id = Convert.ToInt32(model.Id),
                        Description = model.Description,
                        Title = model.Title,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        ManagerId = GetCurrentId(),
                        FilePath=model.FilePath
                    };
                   var response= projectHandler.UpdateProject(project);
                   TempData["messageCreateOrEdit"] = response.Message;
                   TempData["successCreateOrEdit"] = response.Success;
                   return View(); 
                }
                else
                {
                    var project = new ProjectModel
                    {
                        Description = model.Description,
                        Title = model.Title,
                        StartDate = model.StartDate,
                        EndDate = model.EndDate,
                        ManagerId = GetCurrentId(),
                        FilePath = model.FilePath
                    };
                    var response = projectHandler.CreateProject(project);
                    TempData["messageCreateOrEdit"] = response.Message;
                    TempData["successCreateOrEdit"] = response.Success;
                    return View();


                }
            }
            ModelState.AddModelError("", "Kayıt veya Güncelleme başarısız.");
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                projectHandler.DeleteProject(id);

                SetCurrentActionState(ActionType.Delete,"Proje İptal Edilmiştir");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult GetProjectRequests(int? page, string currentFilter, string name)
        {
            ViewBag.name = name;
            var projectRequestModels = projectHandler.GetProjectRequestsForManager(GetCurrentId(), page ?? 1, _pageSize, name);

            if (name != null)
                page = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var viewModel = new ListProjectRequestViewModel { ProjectRequest = projectRequestModels };

            return View(viewModel);
        }


        private void SetCurrentActionState(ActionType actionType, string actionResult)
        {
            TempData["AdminViewModel"] =JsonConvert.SerializeObject(new ActionResponse { ActionType = actionType, ActionMessage = actionResult });
        }

        private ActionResponse GetCurrentActionState()
        {
            if (TempData["AdminViewModel"] != null)
                return TempData["AdminViewModel"] as ActionResponse;

            else return null;
        }
    }
}

