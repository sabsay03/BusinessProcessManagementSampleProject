using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntitiyFrameWork
{
    public class EfGroupRepo : GenericRepository<Group>, IGroupDal
    {
        public List<Group> GetListGroupByProject()
        {
            Context c = new Context();
            var groups = c.Groups.Include(x=>x.GroupMembers).ThenInclude(y=>y.Member).Where(x => x.ProjectId == 1).ToList();
            return groups;
        }
    }
}
