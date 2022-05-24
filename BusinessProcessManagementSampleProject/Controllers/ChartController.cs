using BusinessLayer.Helpers;
using BusinessProcessManagementSampleProject.Models;
using BusinessProcessManagementSampleProject.Models.Chart;
using EntityLayer.Handler;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessProcessManagementSampleProject.Controllers
{
    public class ChartController : BaseController
    {
        private readonly IMissionHandler missionHandler;
        private readonly IProjectHandler projectHandler;

        public ChartController(IMissionHandler missionHandler, IProjectHandler projectHandler)
        {
            this.missionHandler = missionHandler;
            this.projectHandler = projectHandler;
        }

        public ActionResult Index()
        {
            return View();
        }


        public JsonResult MissionChart(RequestModel request)
        {
            if (request.id == 99)
            {
                var mission = missionHandler.GetAllProjectMission(request.ProjectID);
                int totalCount = mission.Count();
                int cancelCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Cancel).Count();
                int activecount = totalCount - cancelCount;
                int processCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Process).Count();
                int doneCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Done).Count();
                int timeoutCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Timeout).Count();
                int WaitingCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting).Count();
                int WaitingApproveCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.WaitingForApprove).Count();
                List<ChartModel> list = new List<ChartModel>();

                list.Add(new ChartModel
                {
                    statu = "Devam Ediyor",
                    count = processCount
                });
                list.Add(new ChartModel
                {
                    statu = "Süresi Dolmuş",
                    count = timeoutCount
                });
                list.Add(new ChartModel
                {
                    statu = "Tamamlandı",
                    count = doneCount
                });
                list.Add(new ChartModel
                {
                    statu = "bekleniyor",
                    count = WaitingCount
                });
                list.Add(new ChartModel
                {
                    statu = "OnayBekleniyor",
                    count = WaitingApproveCount
                });

                return Json(new { jsonlist = list, totalCount = totalCount, activecount = activecount, cancelCount = cancelCount });
            }
            else
            {
                var mission = missionHandler.GetProjectMissionForMember(request.id, request.ProjectID);
                int totalCount = mission.Count();
                int cancelCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Cancel).Count();
                int activecount = totalCount - cancelCount;
                int processCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Process).Count();
                int doneCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Done).Count();
                int timeoutCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Timeout).Count();
                int WaitingCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting).Count();
                int WaitingApproveCount = mission.Where(m => m.MissionStatus == EntityLayer.Enums.MissionStatus.WaitingForApprove).Count();
                List<ChartModel> list = new List<ChartModel>();

                list.Add(new ChartModel
                {
                    statu = "Devam Ediyor",
                    count = processCount
                });
                list.Add(new ChartModel
                {
                    statu = "Süresi Dolmuş",
                    count = timeoutCount
                });
                list.Add(new ChartModel
                {
                    statu = "Tamamlandı",
                    count = doneCount
                });
                list.Add(new ChartModel
                {
                    statu = "bekleniyor",
                    count = WaitingCount
                });
                list.Add(new ChartModel
                {
                    statu = "OnayBekleniyor",
                    count = WaitingApproveCount
                });

                return Json(new { jsonlist = list, totalCount = totalCount, activecount = activecount, cancelCount = cancelCount });
            }

        }
    }
}
