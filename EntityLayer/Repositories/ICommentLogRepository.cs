using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Repositories
{
    public interface ICommentLogRepository
    {
        bool CreateCommentLog(CommentLog commentLog);
        List<CommentLog> GetProjectCommentLogForManager(int managerId);
        List<CommentLog> GetMissionCommentLogForManager(int managerId);

        List<CommentLog> GetProjectCommentLogForMember(int memberId);
        List<CommentLog> GetMissionCommentLogForMember(int memberId);

    }
}
