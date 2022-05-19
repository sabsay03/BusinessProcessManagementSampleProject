using EntityLayer.Concrete.Relations;
using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Repositories
{
    public interface IProjectMemberRepository
    {
        ProjectMember getProjectMember(int memberId,int ProjectId);
        Tuple<List<UserDetailedModel>, int> GetMembers(int projectId, int pagenumber, int pageSize, string searchFilter);
        IPagedList<ProjectModel> GetProjectsForMember(int memberId, int pagenumber, int pageSize, string searchFilter);
        int SaveProjectMember(ProjectMemberModel projectMember);
    }
}
