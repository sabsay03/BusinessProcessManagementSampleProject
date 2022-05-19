using EntityLayer.Concrete;
using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Handler
{
    public interface IMissionHandler
    {
        Mission GetById(int id);
        MissionModel GetMissionDetailById(int id);
        MessageResponse UpdateMission(MissionModel missionModel);

        MessageResponse SendMission(int missionId, string filePath);
        MessageResponse ApproveMission(int missionId, string feedBack);
        MessageResponse SendBackMission(int missionId, string feedBack);

        MessageResponse GiveExtraTime(int missionId, string feedBack, DateTime endDate, DateTime startDate);
        MessageResponse CreateMission(MissionModel missionModel);
        IPagedList<MissionModel> GetMissionForMember(int memberId, int pageNumber, int pageSize, string searchFilter);
        int DeleteMission(int id);
    }
}