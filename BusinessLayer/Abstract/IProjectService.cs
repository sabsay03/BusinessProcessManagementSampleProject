using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IProjectService
    {
        void ProjectAdd(Project project);
        void ProjectDelete(Project project);
        void ProjectUpdate(Project project);
        List<Project> GetList();
        Project GetById(int id);
    }
}
