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
    public class GroupManager : IGroupService
    {
        IGroupDal _groupDal;
        public GroupManager(IGroupDal groupDal)
        {
            _groupDal = groupDal;
        }

        public Group GetById(int id)
        {
            return _groupDal.GetById(id) ; 
        }

        public List<Group> GetList()
        {
            return _groupDal.GetListAll();
        }

        public List<Group> GetListByProject()
        {
            return _groupDal.GetListGroupByProject() ;
        }

        public void GroupAdd(Group group)
        {
            _groupDal.Insert(group);
        }



        public void GroupDelete(Group group)
        {
            _groupDal.Delete(group);
        }



        public void GroupUpdate(Group group)
        {
            _groupDal.Delete(group);
        }

    }
}
