using BusinessProcessManagementSampleProject.Models.Project;
using EntityLayer.Handler;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VeriPark.DigitalBadge.Business;

namespace BusinessProcessManagementSampleProject.Controllers
{
    public class ProjectController : BaseController
    {
        public readonly IProjectHandler projectHandler;

        public ProjectController(IProjectHandler projectHandler):base()
        {
            this.projectHandler = projectHandler;
        }

        // GET: ProjectController


        public ActionResult Index(int? pageNumber, string currentFilter, string name)
        {
            ViewBag.name = name;
            var badgeList = projectHandler.GetProjectsForManager(GetCurrentId(), pageNumber ?? 1, _pageSize, name);

            if (name != null)
                pageNumber = 1;
            else
                name = currentFilter;

            ViewBag.CurrentFilter = name;

            var viewModel = new ListProjectsViewModel { Projects = badgeList };

            return View(viewModel);
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
                    EndDate = model.EndDate
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

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

