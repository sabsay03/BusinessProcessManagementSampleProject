using EntityLayer.Concrete;
using EntityLayer.Models;
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

        Tuple<List<MissionModel>, int> GetMissionByProjectId(int projectId, int pagenumber, int pageSize, string searchFilter);
        int SaveMission(MissionModel missionModel);
        int UpdateMission(MissionModel missionModel);

        int DeleteMission(int id);
    }
}
