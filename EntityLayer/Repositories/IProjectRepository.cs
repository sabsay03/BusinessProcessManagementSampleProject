using EntityLayer.Concrete;
using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Repositories
{
    public interface IProjectRepository
    {
        Project GetById(int id);

        Project GetByTitle(int id,string title, int managerId);

        IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pagenumber, int pageSize, string searchFilter);

        int UpdateProject(ProjectModel project);

        int SaveProject(ProjectModel project);

    }
}
