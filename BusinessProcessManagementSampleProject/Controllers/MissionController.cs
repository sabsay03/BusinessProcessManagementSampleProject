using BusinessLayer.Helpers;
using BusinessProcessManagementSampleProject.Models.Mission;
using EntityLayer.Handler;
using EntityLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Controllers
{
    public class MissionController : BaseController
    {
        public readonly IProjectHandler projectHandler;
        public readonly IUserHandler userHandler;
        private readonly DropDownHelper dropDownHelper;
        private readonly IMissionHandler missionHandler;

        public MissionController(IProjectHandler projectHandler, IUserHandler userHandler, DropDownHelper dropDownHelper, IMissionHandler missionHandler)
        {
            this.projectHandler = projectHandler;
            this.userHandler = userHandler;
            this.dropDownHelper = dropDownHelper;
            this.missionHandler = missionHandler;
        }

        // GET: MissionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MissionController/CreateMission
        public ActionResult CreateMission(int? id)
        {
            ViewBag.ProjectSelectListItems = dropDownHelper.ProjectSelectListByManager(GetCurrentId());
            if (id != null)
            {   
                var model = missionHandler.GetById(Convert.ToInt32(id));
                return View(new MissionCreateViewModel
                {
                    Id=model.Id,
                    Title=model.Title,
                    Description=model.Description,
                    EndDate=model.EndDate,
                    StartDate=model.StartDate,
                    StudentNumber=model.Member.StudentNumber,
                    ProjectId=model.ProjectId,
                    StudenFullName= $"{model.Member.FirstName} {model.Member.LastName}"
            });
            }
            return View();
        }

        // POST: MissionController/Create
        [HttpPost]
        public ActionResult CreateMission(MissionCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == null)
                {
                    var member = userHandler.GetUserByStudenNo(model.StudentNumber);
                    var missionModel = new MissionModel
                    {
                        Title = model.Title,
                        Description = model.Description,
                        EndDate = model.EndDate,
                        StartDate = model.StartDate,
                        StudentNumber = model.StudentNumber,
                        StudentId = member.Id,
                        ProjectId = model.ProjectId,
                    };
                    var response = missionHandler.CreateMission(missionModel);
                    TempData["messageCreateOrEdit"] = response.Message;
                    TempData["successCreateOrEdit"] = response.Success;
                    return View ();
                }
                else
                {
                    var member = userHandler.GetUserByStudenNo(model.StudentNumber);
                    var missionModel = new MissionModel
                    {
                        Id=Convert.ToInt32(model.Id),
                        Title = model.Title,
                        Description = model.Description,
                        EndDate = model.EndDate,
                        StartDate = model.StartDate, 
                        StudentNumber = model.StudentNumber,
                        StudentId = member.Id,
                        ProjectId = model.ProjectId,
                    };
                    var response = missionHandler.UpdateMission(missionModel);
                    TempData["messageCreateOrEdit"] = response.Message;
                    TempData["successCreateOrEdit"] = response.Success;
                    return View();
                }
            }
            ViewBag.ProjectSelectListItems = dropDownHelper.ProjectSelectListByManager(GetCurrentId());
            ModelState.AddModelError("", "Öğrenci Ekleme Başarısız.");
            return View(model);
        }

        // POST: MissionController/Delete/5
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
