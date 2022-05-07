using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Models;
using EntityLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        public Project GetById(int id)
        {
            int userid = Convert.ToInt32(id);
            using (Context databaseContext = new Context())
            {
                return databaseContext.Projects.Include(p=>p.ProjectMembers).Include(p=>p.Manager).Where(p => p.Id == userid).FirstOrDefault();
            

            }
        }

        public Project GetByTitle(string title, int managerId,int id)
        {
            using (Context databaseContext = new Context())
            {
                if (id == 0)
                    return databaseContext.Projects.Where(p =>p.Title == title && p.ManagerId == managerId && p.ProjectStatus != EntityLayer.Enums.ProjectStatus.Cancel).FirstOrDefault();

                else
                    return databaseContext.Projects.FirstOrDefault(p => p.Id != id && p.Title == title && p.ManagerId == managerId && p.ProjectStatus != EntityLayer.Enums.ProjectStatus.Cancel);
            }
        }

        public int SaveProject(ProjectModel project)
        {
            using (Context databaseContext = new Context())
            {
                Project model = new Project
                {
                    Title = project.Title,
                    Description = project.Description,
                    StartDate = project.StartDate,
                    EndDate = project.EndDate,
                    CreateDate = DateTime.Now,
                    ManagerId=project.ManagerId,
                    ProjectStatus = EntityLayer.Enums.ProjectStatus.Active
                };

                databaseContext.Projects.Add(model);
                databaseContext.SaveChanges();

                return model.Id;
            }
        }

        public int UpdateProject(ProjectModel project)
        {
            using (Context databaseContext = new Context())
            {
                var Entitiy = GetById(project.Id);

                databaseContext.Projects.Attach(Entitiy);

                Entitiy.Description = project.Description;
                Entitiy.Title = project.Title;
                Entitiy.StartDate = project.StartDate;
                Entitiy.EndDate = project.EndDate;
                Entitiy.CreateDate = DateTime.Now;

                databaseContext.SaveChanges();

                return project.Id;
            }
        }

        public IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pagenumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {
                var query = databaseContext.Projects.Where(p => p.ManagerId == managerId).
                 Select(p => new ProjectModel
                 {
                     Id = Convert.ToInt32(p.Id),
                     Description = p.Description,
                     Title = p.Title,
                     StartDate = p.StartDate,
                     EndDate = p.EndDate,
                     ManagerId = p.ManagerId,
                     ProjectStatus = p.ProjectStatus
                 });


                if (!String.IsNullOrEmpty(searchFilter))
                    query = query.Where(p =>
                        p.Title.ToLower().Contains(searchFilter.ToLower())
                        );

                return query.OrderBy(p => p.Id).ToPagedList(pagenumber, pageSize);
            }
        }

        public int DeleteProject(int id)
        {
            int userid = Convert.ToInt32(id);
            using (Context databaseContext = new Context())
            {
                var project = GetById(id);
                databaseContext.Projects.Attach(project);
                project.ProjectStatus = EntityLayer.Enums.ProjectStatus.Cancel;
                databaseContext.SaveChanges();
                return id;

            }
        }

        public List<Project> GetActiveProjects(int? managerId)
        {
            using (var databaseContext = new Context())
            {
                return databaseContext.Projects.Where(p => p.ProjectStatus == EntityLayer.Enums.ProjectStatus.Active && p.ManagerId == managerId).OrderBy(p => p.Id).ToList();
            }
        }

        public IPagedList<ProjectModel> GetProjectRequestsForManager(int managerId, int pagenumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {
                var query = databaseContext.ProjectRequests.Include(p=> p.Project).Include(p=>p.Manager).Include(p=>p.User).Where(p => p.ManagerId == managerId && p.ProjectRequestStatus==EntityLayer.Enums.ProjectRequestStatus.Waiting).
                 Select(p => new ProjectRequestModel
                 {
                     Id = Convert.ToInt32(p.Id),
                     ProjecId=p.ProjectId,
                     MemberId=p.UserId,
                     ManagerId=p.ManagerId,
                     ProjectTitle=p.Project.Title,
                     StudentNo=p.User.StudentNumber,
                     FullName= item.StudenFullName = $"{item.FirstName} {item.LastName}";


            });


                if (!String.IsNullOrEmpty(searchFilter))
                    query = query.Where(p =>
                        p.Title.ToLower().Contains(searchFilter.ToLower())
                        );

                return query.OrderBy(p => p.Id).ToPagedList(pagenumber, pageSize);
            }
        }
    }
}
