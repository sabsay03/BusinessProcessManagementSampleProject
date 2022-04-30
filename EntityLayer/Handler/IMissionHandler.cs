using EntityLayer.Concrete;
using EntityLayer.Models;
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

        MessageResponse UpdateMission(MissionModel missionModel);
        MessageResponse CreateMission(MissionModel missionModel);
    }
}
