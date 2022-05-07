using EntityLayer.Concrete;
using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Handler
{
    public interface IProjectHandler
    {
        Project GetById(int id);
        ProjectModel GetProjectDetailById(int id);

        MessageResponse UpdateProject(ProjectModel project);
        MessageResponse CreateProject(ProjectModel project);

        MessageResponse AddStudenToProject(ProjectMemberModel projectMember);
        int DeleteProject(int id);

        Tuple<List<UserDetailedModel>,int>GetMembersOfProject(int projetId, int pagenumber, int pageSize, string searchFilter);

        Tuple<List<MissionModel>, int> GetMissionsOfProject(int projetId, int pagenumber, int pageSize, string searchFilter);

        IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pageNumber, int pageSize, string searchFilter);

        IPagedList<ProjectRequestModel> GetProjectRequestsForManager(int managerId, int pageNumber, int pageSize, string searchFilter);
    }
}
