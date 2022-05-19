using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Relations;
using EntityLayer.Models;
using EntityLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class ProjectRequestRepository : IProjectRequestRepository
    {
        public ProjectRequest GetProjectRequest(int memberId, int managerId, int projectId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.ProjectRequests.Where(pr => pr.ProjectId == projectId && pr.ManagerId == managerId && pr.UserId == memberId).FirstOrDefault();
            }
        }

        public int SaveProjectRequest(ProjectRequestModel model)
        {
            using (Context databaseContext = new Context())
            {
                ProjectRequest projectRequest = new ProjectRequest
                {
                    UserId = model.MemberId,
                    ProjectId = model.ProjecId,
                    ProjectRequestStatus = EntityLayer.Enums.ProjectRequestStatus.Waiting,
                    ManagerId = model.ManagerId,
                    CreateDate = DateTime.Now

                };
                databaseContext.ProjectRequests.Add(projectRequest);
                databaseContext.SaveChanges();
                return projectRequest.Id;
            }
        }
    }
}
