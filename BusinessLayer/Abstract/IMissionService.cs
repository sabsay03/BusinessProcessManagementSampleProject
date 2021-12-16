using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMissionService
    {
        void MissionAdd(Mission mission);
        void MissionDelete(Mission mission);
        void MissionUpdate(Mission mission);
        List<Mission> GetList();
        Mission GetById(int id);
    }
}
