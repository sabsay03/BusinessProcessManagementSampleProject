using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MissionManager : IMissionService
    {
        IMissionDal _missionDal;
        public MissionManager(IMissionDal mission)
        {
            _missionDal = mission;
        }

        public Mission GetById(int id)
        {
            return _missionDal.GetById(id);
        }

        public List<Mission> GetList()
        {
            return _missionDal.GetListAll();
        }

        public void MissionAdd(Mission mission)
        {
            _missionDal.Insert(mission);
        }

        public void MissionDelete(Mission mission)
        {
            _missionDal.Delete(mission);
        }

        public void MissionUpdate(Mission mission)
        {
            _missionDal.Update(mission);
        }
    }
}
