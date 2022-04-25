using EntityLayer.Concrete;
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

        public ProjectHandler(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public MessageResponse CreateProject(ProjectModel project)
        {
            var titleCheck = projectRepository.GetByTitle(project.Id, project.Title, project.ManagerId);

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

        public Project GetById(int id)
        {
            return projectRepository.GetById(id);
        }

        public IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pageNumber, int pageSize, string searchFilter)
        {
            return projectRepository.GetProjectsForManager(managerId, pageNumber, pageSize, searchFilter);
        }

        public MessageResponse UpdateProject(ProjectModel project)
        {
            var titleCheck = projectRepository.GetByTitle(project.Id,project.Title, project.ManagerId);

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
