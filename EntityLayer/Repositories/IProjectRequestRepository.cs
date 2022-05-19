using EntityLayer.Concrete.Relations;
using EntityLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Repositories
{
    public interface IProjectRequestRepository
    {
        ProjectRequest GetProjectRequest(int memberId,int managerId,int projectId);

        int SaveProjectRequest(ProjectRequestModel model);
    }
}
