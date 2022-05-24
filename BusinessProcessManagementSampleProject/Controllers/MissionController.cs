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
            ModelState.AddModelError("", "Görev Ekleme Başarısız.");
            return View();
        }

        // POST: MissionController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id,int projectId)
        {
            try
            {
                missionHandler.DeleteMission(id);
                TempData["messageCreateOrEdit"] = "Görev İptal edilmiştir.";
                TempData["successCreateOrEdit"] = true;
                return RedirectToAction("Detail", "Project", new { projectId = projectId });
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Detail(int missionId)
        {
            var mission = missionHandler.GetMissionDetailById(missionId);

            var viewModel = new MissionDetailViewModel { Mission = mission };

            return View(viewModel);
        }
        public JsonResult ApproveMission(int missionId,string feedBack)
        {
            var response = missionHandler.ApproveMission(missionId, feedBack);
            TempData["messageCreateOrEdit"] = response.Message;
            TempData["successCreateOrEdit"] = response.Success;

            return Json(response);
        }

        public JsonResult SendBack(int missionId, string feedBack)
        {
            var response = missionHandler.SendBackMission(missionId, feedBack);
            TempData["messageCreateOrEdit"] = response.Message;
            TempData["successCreateOrEdit"] = response.Success;

            return Json(response);
        }

        public JsonResult GiveExtraTime(int missionId,string feedBack, DateTime endDate,DateTime startDate)
        {
            var response = missionHandler.GiveExtraTime(missionId, feedBack,endDate,startDate);
            TempData["messageCreateOrEdit"] = response.Message;
            TempData["successCreateOrEdit"] = response.Success;

            return Json(response);
        }



    }
}
