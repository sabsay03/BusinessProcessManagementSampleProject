using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class ProjectManager : IProjectService
    {
        IProjectDal _projtecdal;

        public ProjectManager(IProjectDal projectDal)
        {
            _projtecdal = projectDal;
        }

        public Project GetById(int id)
        {
           return _projtecdal.GetById(id);
        }

        public List<Project> GetList()
        {
            return _projtecdal.GetListAll();
        }

        public void ProjectAdd(Project project)
        {
            _projtecdal.Insert(project);
        }

        public void ProjectDelete(Project project)
        {
            _projtecdal.Delete(project);
        }

        public void ProjectUpdate(Project project)
        {
            _projtecdal.Update(project);
        }
    }
}
