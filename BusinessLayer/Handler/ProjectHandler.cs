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
    public class ProjectHandler : IProjectHandler
    {
        private readonly IProjectRepository projectRepository;
        private readonly IProjectMemberRepository projectMemberRepository;
        private readonly IMissionRepository missionRepository;
        private readonly IProjectRequestRepository projectRequestRepository;
        private readonly ICommentLogRepository commentLogRepository;
        private readonly IUserRepository userRepository;


        public ProjectHandler(IProjectRepository projectRepository, IProjectMemberRepository projectMemberRepository,IMissionRepository missionRepository,IProjectRequestRepository projectRequestRepository,ICommentLogRepository commentLogRepository,IUserRepository userRepository)
        {
            this.projectRepository = projectRepository;
            this.projectMemberRepository = projectMemberRepository;
            this.missionRepository = missionRepository;
            this.projectRequestRepository = projectRequestRepository;
            this.commentLogRepository = commentLogRepository;
            this.userRepository = userRepository;
        }
        public Tuple<List<UserDetailedModel>,int> GetMembersOfProject(int projetId, int pagenumber, int pageSize, string searchFilter)
        {
            var pagedlist = projectMemberRepository.GetMembers(projetId,pagenumber,pageSize,searchFilter);

            foreach (var item in pagedlist.Item1)
            {
                item.UserFullName = $"{item.Firstname} {item.Lastname}";
            }
            return pagedlist;
        }

        public Tuple<List<MissionModel>, int> GetMissionsOfProject(int projetId, int pagenumber, int pageSize, string searchFilter)
        {
            var pagedlist =missionRepository.GetMissionByProjectId(projetId, pagenumber, pageSize, searchFilter);

            foreach (var item in pagedlist.Item1)
            {
                item.StudenFullName = $"{item.FirstName} {item.LastName}";
                item.MissionStatusString = getMissionStatusString(item.MissionStatus);
            }
            return pagedlist;
        }

        public MessageResponse AddStudenToProject(ProjectMemberModel projectMember)
        {
            var check = projectMemberRepository.getProjectMember(projectMember.MemberId,projectMember.ProjecId);

            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Öğrenci Zaten Projede Tanımlanmıştır" };
            }
            var project = GetById(projectMember.ProjecId);
            var user = userRepository.GetUserById(projectMember.MemberId);

            CommentLog log = new CommentLog
            {
                ProjectId = project.Id,
                Date = DateTime.Now,
                Text = ($"{user.FirstName} {user.LastName} öğrencisi {project.Title} İsimli Projeye dahil olmuştur."),
                commentType = EntityLayer.Enums.CommentType.Done
            };
            commentLogRepository.CreateCommentLog(log);

            projectMemberRepository.SaveProjectMember(projectMember);
            return new MessageResponse { Success = true, Message = "Öğrenci Projeye Tanımlanmıştır" };

        }
        public MessageResponse CreateProjectRequest(ProjectRequestModel projectrequest)
        {
            var check = projectMemberRepository.getProjectMember(projectrequest.MemberId, projectrequest.ProjecId);
            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Bu Projede Zaten Tanımlısın" };
            }
            var checkrequest = projectRequestRepository.GetProjectRequest(projectrequest.MemberId,projectrequest.ManagerId,projectrequest.ProjecId);
            if (checkrequest != null)
            {
                return new MessageResponse { Success = false, Message = "Bu Projeye önceden talep gönderilmiş." };
            }
            projectRequestRepository.SaveProjectRequest(projectrequest);
            return new MessageResponse { Success = true, Message = "Talep Gönderilmiştir" };
        }

        public MessageResponse CreateProject(ProjectModel project)
        {     
           var titleCheck = projectRepository.GetByTitle(project.Title, project.ManagerId,project.Id);

            

            if (titleCheck != null)
            {
                return new MessageResponse { Success = false, Message = "Bu isimde bir Proje zaten tanımlanmıştır" };
            }
            else
            {
                projectRepository.SaveProject(project);
                return new MessageResponse { Success = true, Message = "Proje Oluşturulmuştur " };
            }

        }

        public int DeleteProject(int id)
        {
            var project = GetById(id);

            CommentLog log = new CommentLog
            {
                ProjectId = project.Id,
                Date = DateTime.Now,
                Text = ($"{project.Title} İsimli Proje İptal olmuştur."),
                commentType = EntityLayer.Enums.CommentType.Cancel
            };
            commentLogRepository.CreateCommentLog(log);

            return projectRepository.DeleteProject(id);
        }


        public Project GetById(int id)
        {
            return projectRepository.GetById(id);
        }

        public ProjectModel GetProjectDetailById(int id)
        {
            var project=projectRepository.GetById(id);
            return new ProjectModel
            {
                Id = project.Id,
                Title = project.Title,
                Description = project.Description,
                StartDate = project.StartDate,
                EndDate = project.EndDate,
                CreateDate = project.CreateDate,
                ManagerId = (int)project.ManagerId,
                ProjectStatusString = getProjectStatusString(project.ProjectStatus),
                Manager=project.Manager,
                FilePath=project.FilePath
            };
        }

        public IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pageNumber, int pageSize, string searchFilter)
        {
            var projects=projectRepository.GetProjectsForManager(managerId, pageNumber, pageSize, searchFilter);

            foreach (var project in projects)
            {
                project.ProjectStatusString = getProjectStatusString(project.ProjectStatus);
            }
            return projects;
        }
        public string getProjectStatusString(ProjectStatus projectStatus)
        {
            switch (projectStatus)
            {
                case ProjectStatus.Active: { return "Proje tarihi Bekleniyor."; }
                case ProjectStatus.Cancel: { return "İptal Edildi."; }
                case ProjectStatus.Done: { return "Tamamlandı."; }
                case ProjectStatus.Process: { return "Proje Devam Ediyor."; }
                case ProjectStatus.TimeOut: { return "Proje Süresi Bitti."; }
                default: { return null; }
            }
        }

        public MessageResponse UpdateProject(ProjectModel project)
        {
            var titleCheck = projectRepository.GetByTitle(project.Title, project.ManagerId, project.Id);

            if (titleCheck != null)
            {
                return new MessageResponse { Success = false, Message = "Bu isimde bir Proje zaten tanımlanmıştır" };
            }
            else
            {
                projectRepository.UpdateProject(project);
                return new MessageResponse { Success = true, Message = "Proje Güncellenmiştir " };
            }
        }

        public IPagedList<ProjectRequestModel> GetProjectRequestsForManager(int managerId, int pageNumber, int pageSize, string searchFilter)
        {
            var requests = projectRepository.GetProjectRequestsForManager(managerId, pageNumber, pageSize, searchFilter);

            foreach (var item in requests)
            {
                if (item.ProjectRequestStatus==EntityLayer.Enums.ProjectRequestStatus.Approved)
                {
                    item.ProjectRequestStatusString = "Onaylanmış";
                }else if (item.ProjectRequestStatus == EntityLayer.Enums.ProjectRequestStatus.Waiting)
                {
                    item.ProjectRequestStatusString = "Onay Bekleniyor";
                }
                else
                {
                    item.ProjectRequestStatusString = "Red Edildi";
                }
            }

            return requests;
        }

        public IPagedList<ProjectModel> GetProjectsForMember(int memberId, int pageNumber, int pageSize, string searchFilter)
        {
            var pagedlist = projectMemberRepository.GetProjectsForMember(memberId, pageNumber, pageSize, searchFilter);

            foreach (var project in pagedlist)
            {
                project.ProjectStatusString = getProjectStatusString(project.ProjectStatus);
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

        public MessageResponse CompleteProject(int projectId)
        {
            var project = GetById(projectId);

            CommentLog log = new CommentLog
            {
                ProjectId = project.Id,
                Date = DateTime.Now,
                Text = ($"{project.Title} İsimli Proje Tamamlanmıştır."),
                commentType = EntityLayer.Enums.CommentType.Done
            };

            commentLogRepository.CreateCommentLog(log);
            projectRepository.UpdateProjectForComplete(projectId);
            return new MessageResponse { Success = true, Message = "Proje Tamamlanmıştır." };
        }

        public int DeniedMember(int projectId, int memberId)
        {
            return projectMemberRepository.DeniedMember(projectId,memberId);
        }

        public int updateMemberRequest(int projectId, int memberId)
        {
            return projectMemberRepository.updateMemberRequest(projectId, memberId);
        }
    }
}
