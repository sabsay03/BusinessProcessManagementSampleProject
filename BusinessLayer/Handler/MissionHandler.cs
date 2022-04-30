using EntityLayer.Concrete;
using EntityLayer.Handler;
using EntityLayer.Models;
using EntityLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Handler
{
    public class MissionHandler : IMissionHandler
    {
        private readonly IMissionRepository missionRepository;

        public MissionHandler(IMissionRepository missionRepository)
        {
            this.missionRepository = missionRepository;
        }

        public MessageResponse CreateMission(MissionModel missionModel)
        {
            var check = missionRepository.GetMissionForCheck(missionModel.StudentId,missionModel.Title,missionModel.ProjectId,missionModel.Id);

            if (check!=null) 
            {
                return new MessageResponse { Success = false, Message = "Bu kişiye zaten bu isimde bir görev tanımlanmıştır" };
            }
            {
                missionRepository.SaveMission(missionModel);
                return new MessageResponse { Success = true, Message = "Görev tanımlanmıştır" };
            }



        }
        public MessageResponse UpdateMission(MissionModel missionModel)
        {
            var check = missionRepository.GetMissionForCheck(missionModel.StudentId, missionModel.Title, missionModel.ProjectId, missionModel.Id);

            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Bu kişiye zaten bu isimde bir görev tanımlanmıştır" };
            }
            {
                missionRepository.SaveMission(missionModel);
                return new MessageResponse { Success = true, Message = "Görev Güncellenmiştir" };
            }
        }

        public Mission GetById(int id)
        {
            return missionRepository.GetById(id);
        }

    

    }
}
