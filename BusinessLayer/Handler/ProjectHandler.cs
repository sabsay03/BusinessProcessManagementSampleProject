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


        public ProjectHandler(IProjectRepository projectRepository, IProjectMemberRepository projectMemberRepository)
        {
            this.projectRepository = projectRepository;
            this.projectMemberRepository = projectMemberRepository;
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

        public MessageResponse AddStudenToProject(ProjectMemberModel projectMember)
        {
            var check = projectMemberRepository.getProjectMember(projectMember.MemberId,projectMember.ProjecId);

            if (check != null)
            {
                return new MessageResponse { Success = false, Message = "Öğrenci Zaten Projede Tanımlanmıştır" };
            }
            projectMemberRepository.SaveProjectMember(projectMember);
            return new MessageResponse { Success = true, Message = "Öğrenci Projeye Tanımlanmıştır" };

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
                CreateDate = DateTime.Now,
                ManagerId = project.ManagerId,
                ProjectStatusString = getProjectStatusString(project.ProjectStatus),
                Manager=project.Manager


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
                case ProjectStatus.Active: { return "Proje Devam Ediyor"; }
                case ProjectStatus.Cancel: { return "İptal Edildi"; }
                case ProjectStatus.Done: { return "Tamamlandı"; }
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
    }
}
