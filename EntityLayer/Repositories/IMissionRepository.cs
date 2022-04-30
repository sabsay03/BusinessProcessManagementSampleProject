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

        int SaveMission(MissionModel missionModel);
        int UpdateMission(MissionModel missionModel);
    }
}
