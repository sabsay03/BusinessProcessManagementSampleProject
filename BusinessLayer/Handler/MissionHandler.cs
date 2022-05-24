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
        private readonly ICommentLogRepository commentLogRepository;
        public MissionHandler(IMissionRepository missionRepository, IProjectMemberRepository projectMemberRepository,ICommentLogRepository commentLogRepository)
        {
            this.missionRepository = missionRepository;
            this.projectMemberRepository = projectMemberRepository;
            this.commentLogRepository = commentLogRepository;
        }

        public MessageResponse CreateMission(MissionModel missionModel)
        {
            var projectMemberCheck = projectMemberRepository.getProjectMember(missionModel.StudentId, missionModel.ProjectId);
            if (projectMemberCheck == null)
            {
                return new MessageResponse { Success = false, Message = "Bu Kişi Belirtilen Projede Yer Almıyor" };
            }
            var enddate=projectMemberCheck.Project.EndDate;
            var startdate=projectMemberCheck.Project.StartDate;
            if (missionModel.StartDate<startdate || missionModel.StartDate>enddate || missionModel.EndDate<startdate || missionModel.EndDate>enddate)
            {
                return new MessageResponse { Success = false, Message = "Görev tarihleri Proje tarihleri dışında ayarlanmıştır. Lütfen tarihleri düzenleyip tekrar giriniz." };

            }

            var check = missionRepository.GetMissionForCheck(missionModel.StudentId, missionModel.Title, missionModel.ProjectId, missionModel.Id);
            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Bu kişiye zaten bu isimde bir görev tanımlanmıştır" };
            }
            var id=missionRepository.SaveMission(missionModel);

            var mission = GetById(id);

            CommentLog log = new CommentLog
            {
                ProjectId = mission.ProjectId,
                Date = DateTime.Now,
                MissionId = id,
                Text = ($"{mission.Project.Title}'de ki {mission.Title} İsminde Görev oluşturulmuştur."),
                commentType = EntityLayer.Enums.CommentType.Active
            };
            commentLogRepository.CreateCommentLog(log);

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
            var mission = GetById(id);

            CommentLog log = new CommentLog{
                ProjectId = mission.ProjectId,
                Date = DateTime.Now,
                MissionId = id,
                Text = ($"{mission.Project.Title}'de ki {mission.Title} İsimli Görev iptal edilmiştir."),
                commentType = EntityLayer.Enums.CommentType.Cancel
            };

            commentLogRepository.CreateCommentLog(log);
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

                CommentLog log = new CommentLog
                {
                    ProjectId = mission.ProjectId,
                    Date = DateTime.Now,
                    MissionId = missionId,
                    Text = ($"{mission.Project.Title}'de ki {mission.Title} İsimli Görev Onay Bekliyor."),
                    commentType = EntityLayer.Enums.CommentType.WaitingForApprove
                };

                commentLogRepository.CreateCommentLog(log);
                missionRepository.UpdateForFinishMission(missionId, filePath);
                return new MessageResponse { Success = true, Message = "Proje Hocaya gönderilmiştir." };

            }
        }

        public MessageResponse ApproveMission(int missionId, string feedBack)
        {
            var mission = missionRepository.GetById(missionId);

            CommentLog log = new CommentLog
            {
                ProjectId = mission.ProjectId,
                Date = DateTime.Now,
                MissionId = missionId,
                Text = ($"{mission.Project.Title}'de ki {mission.Title} Görev Onaylanmıştır."),
                commentType = EntityLayer.Enums.CommentType.Done
            };

            commentLogRepository.CreateCommentLog(log);
            missionRepository.UpdateForApprove(missionId, feedBack);

            return new MessageResponse { Success = true, Message = "Görev Onaylanmıştır." };


        }

        public MessageResponse GiveExtraTime(int missionId, string feedBack, DateTime endDate, DateTime startDate)
        {
            var mission = missionRepository.GetById(missionId);

            CommentLog log = new CommentLog
            {
                ProjectId = mission.ProjectId,
                Date = DateTime.Now,
                MissionId = missionId,
                Text = ($"{mission.Project.Title}'de ki {mission.Title} Görevine ek süre verilmiştir."),
                commentType = EntityLayer.Enums.CommentType.Process
            };

            missionRepository.UpdatForExtraTime(missionId, feedBack, endDate, startDate);

            return new MessageResponse { Success = true, Message = "Ek Süre Verilmiştir." };
        }

        public MessageResponse SendBackMission(int missionId, string feedBack)
        {

            var mission = missionRepository.GetById(missionId);

            CommentLog log = new CommentLog
            {
                ProjectId = mission.ProjectId,
                Date = DateTime.Now,
                MissionId = missionId,
                Text = ($"{mission.Project.Title}'de ki {mission.Title} Görev geri gönderilmiştir."),
                commentType = EntityLayer.Enums.CommentType.WaitingForApprove
            };

            commentLogRepository.CreateCommentLog(log);
            missionRepository.UpdateForSendBack(missionId, feedBack);

            return new MessageResponse { Success = true, Message = "Görev Geri gönderilmiştir." };
        }

        public List<Mission> GetProjectMissionForMember(int memberId, int projectId)
        {
            return missionRepository.GetProjectMissionForMember(memberId, projectId);


        }

        public List<Mission> GetAllProjectMission(int projectId)
        {
            return missionRepository.GetAllProjectMission( projectId);
        }
    }
}
