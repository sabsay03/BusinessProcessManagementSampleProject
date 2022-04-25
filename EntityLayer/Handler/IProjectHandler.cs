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

        MessageResponse UpdateProject(ProjectModel project);
        MessageResponse CreateProject(ProjectModel project);

        IPagedList<ProjectModel> GetProjectsForManager(int managerId, int pageNumber, int pageSize, string searchFilter);
    }
}
