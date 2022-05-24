using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Handler
{
    public interface ICommentLogHandler
    {
        bool CreateMission(CommentLog commentLog);

        List<CommentLog> GetProjectCommentLogForManager(int managerId);
        List<CommentLog> GetMissionCommentLogForManager(int managerId);

        List<CommentLog> GetProjectCommentLogForMember(int managerId);
        List<CommentLog> GetMissionCommentLogForMember(int managerId);


    }
}
