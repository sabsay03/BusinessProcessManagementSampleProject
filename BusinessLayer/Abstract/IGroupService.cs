using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IGroupService
    {
        void GroupAdd(Group group);
        void GroupDelete(Group group);
        void GroupUpdate(Group group);
        List<Group> GetList();
        List<Group> GetListByProject();
        Group GetById(int id);
    }
}
