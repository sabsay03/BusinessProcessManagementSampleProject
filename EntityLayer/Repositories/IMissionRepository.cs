using EntityLayer.Concrete;
using EntityLayer.Enums;
using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Repositories
{
    public interface IMissionRepository
    {
        Mission GetById(int id);
        Mission GetMissionForCheck(int memberId,string title,int projectId,int id);
        MissionModel GetMissionDetailById(int id);

        Tuple<List<MissionModel>, int> GetMissionByProjectId(int projectId, int pagenumber, int pageSize, string searchFilter);
        IPagedList<MissionModel> GetMissionForMember(int memberId, int pageNumber, int pageSize, string searchFilter);
        List<Mission> GetAllWaitingProcessMission();

        List<Mission> GetProjectMissionForMember(int memberUd, int projectId);
        List<Mission> GetAllProjectMission(int projectId);

        List<Mission> GetsWaitingProcessMission();

        int SaveMission(MissionModel missionModel);
        int UpdateMission(MissionModel missionModel);
        int UpdateMissionStatus(int missionId,MissionStatus status);
        int UpdateForFinishMission(int missionId, string filePath);

        int UpdatForExtraTime(int missionId, string feedBack, DateTime endDate, DateTime startDate);
        int UpdateForApprove(int missionId, string feedBack);
        int UpdateForSendBack(int missionId, string feedBack);
        int DeleteMission(int id);
    }
}
