using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CommentLogRepository : ICommentLogRepository
    {
        public bool CreateCommentLog(CommentLog commentLog)
        {
            using (Context databaseContext = new Context())
            {
                databaseContext.Comments.Add(commentLog);
                databaseContext.SaveChanges();

                return true;
            }
        }

        public List<CommentLog> GetMissionCommentLogForManager(int managerId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Comments.Include(c => c.Project).Where(c => c.Project.ManagerId == managerId && c.MissionId!=null).OrderByDescending(c => c.Date).Take(10).ToList();
            }
        }

        public List<CommentLog> GetMissionCommentLogForMember(int memberId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Comments.Include(c => c.Mission).Where(c => c.Mission.MemberId==memberId && c.MissionId!=null).OrderByDescending(c => c.Date).Take(10).ToList();
            }
        }

        public List<CommentLog> GetProjectCommentLogForManager(int managerId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Comments.Include(c => c.Project).Where(c => c.Project.ManagerId == managerId && c.MissionId==null).OrderByDescending(c => c.Date).Take(10).ToList();
            }
        }

        public List<CommentLog> GetProjectCommentLogForMember(int memberId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Comments.Include(c => c.Project).ThenInclude(m=>m.ProjectMembers).Where(c=> c.Project.ProjectMembers.Select(pm=>pm.MemberId).Contains(memberId)&& c.MissionId==null).OrderByDescending(c => c.Date).Take(10).ToList();
            }
        }
    }
}
