using EntityLayer.Concrete;
using EntityLayer.Enums;
using EntityLayer.Handler;
using EntityLayer.Models;
using EntityLayer.Repositories;
using PagedList.Core;
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
        private readonly IProjectMemberRepository projectMemberRepository;

        public MissionHandler(IMissionRepository missionRepository, IProjectMemberRepository projectMemberRepository)
        {
            this.missionRepository = missionRepository;
            this.projectMemberRepository = projectMemberRepository;
        }

        public MessageResponse CreateMission(MissionModel missionModel)
        {
            var projectMemberCheck = projectMemberRepository.getProjectMember(missionModel.StudentId, missionModel.ProjectId);
            if (projectMemberCheck == null)
            {
                return new MessageResponse { Success = false, Message = "Bu Kişi Belirtilen Projede Yer Almıyor" };
            }
            var enddate=projectMemberCheck.Project.StartDate;
            var startdate=projectMemberCheck.Project.EndDate;
            if (missionModel.StartDate<startdate || missionModel.StartDate>enddate || missionModel.EndDate<startdate || missionModel.EndDate>enddate)
            {
                return new MessageResponse { Success = false, Message = "Görev tarihleri Proje tarihleri dışında ayarlanmıştır. Lütfen tarihleri düzenleyip tekrar giriniz." };

            }

            var check = missionRepository.GetMissionForCheck(missionModel.StudentId, missionModel.Title, missionModel.ProjectId, missionModel.Id);
            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Bu kişiye zaten bu isimde bir görev tanımlanmıştır" };
            }
            

            missionRepository.SaveMission(missionModel);
            return new MessageResponse { Success = true, Message = "Görev tanımlanmıştır" };
        }
        public MessageResponse UpdateMission(MissionModel missionModel)
        {
            var projectMemberCheck = projectMemberRepository.getProjectMember(missionModel.StudentId, missionModel.ProjectId); ;
            if (projectMemberCheck == null)
            {
                return new MessageResponse { Success = false, Message = "Bu Kişi Belirtilen Projede Yer Almıyor" };
            }
            var check = missionRepository.GetMissionForCheck(missionModel.StudentId, missionModel.Title, missionModel.ProjectId, missionModel.Id);

            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Bu kişiye zaten bu isimde bir görev tanımlanmıştır" };
            }
            {
                missionRepository.UpdateMission(missionModel);
                return new MessageResponse { Success = true, Message = "Görev Güncellenmiştir" };
            }
        }

        public Mission GetById(int id)
        {
            return missionRepository.GetById(id);
        }

        public int DeleteMission(int id)
        {
            return missionRepository.DeleteMission(id);
        }

        public IPagedList<MissionModel> GetMissionForMember(int memberId, int pageNumber, int pageSize, string searchFilter)
        {
            var pagedlist = missionRepository.GetMissionForMember(memberId, pageNumber, pageSize, searchFilter);

            foreach (var mission in pagedlist)
            {
                mission.MissionStatusString = getMissionStatusString(mission.MissionStatus);
            }
            return pagedlist;
        }

        public string getMissionStatusString(MissionStatus missionStatus)
        {
            switch (missionStatus)
            {
                case MissionStatus.Process: { return "Görev Devam Ediyor"; }
                case MissionStatus.Cancel: { return "İptal Edildi"; }
                case MissionStatus.Done: { return "Tamamlandı"; }
                case MissionStatus.Waiting: { return "Görev Zamanı Bekleniyor"; }
                case MissionStatus.WaitingForApprove: { return "Onay Bekleniyor"; }
                case MissionStatus.Timeout: { return "Görev Süresi dolmuş"; }

                default: { return null; }
            }
        }

        public MissionModel GetMissionDetailById(int id)
        {
            var mission = missionRepository.GetMissionDetailById(id);

            mission.MissionStatusString = getMissionStatusString(mission.MissionStatus);

            mission.StudenFullName = $"{mission.FirstName} {mission.LastName}";

            return mission;
        }

        public MessageResponse SendMission(int missionId, string filePath)
        {

            var mission = missionRepository.GetById(missionId);

            if (DateTime.Now < mission.StartDate)
            {
                return new MessageResponse { Success = false, Message = "Proje daha başlamamış." };

            }
            else if (mission.EndDate < DateTime.Now)
            {
                return new MessageResponse { Success = false, Message = "Proje Süresi Bitmiş.Ek süre için Hocanızla iletişime geçebilirsiniz." };
            }
            else
            {
                missionRepository.UpdateForFinishMission(missionId, filePath);
                return new MessageResponse { Success = true, Message = "Proje Hocaya gönderilmiştir." };

            }
        }

        public MessageResponse ApproveMission(int missionId, string feedBack)
        {

            missionRepository.UpdateForApprove(missionId, feedBack);

            return new MessageResponse { Success = true, Message = "Görev Onaylanmıştır." };


        }

        public MessageResponse GiveExtraTime(int missionId, string feedBack, DateTime endDate, DateTime startDate)
        {

            missionRepository.UpdatForExtraTime(missionId, feedBack, endDate, startDate);

            return new MessageResponse { Success = true, Message = "Ek Süre Verilmiştir." };
        }

        public MessageResponse SendBackMission(int missionId, string feedBack)
        {
            missionRepository.UpdateForSendBack(missionId, feedBack);

            return new MessageResponse { Success = true, Message = "Görev Geri gönderilmiştir." };
        }
    }
}
