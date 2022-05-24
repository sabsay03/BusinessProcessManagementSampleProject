using EntityLayer.Concrete;
using EntityLayer.Handler;
using EntityLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Handler
{
    public class CommentLogHandler : ICommentLogHandler
    {
        private readonly ICommentLogRepository commentLogRepository;

        public CommentLogHandler(ICommentLogRepository commentLogRepository)
        {
            this.commentLogRepository = commentLogRepository;
        }

        public bool CreateMission(CommentLog commentLog)
        {
            return commentLogRepository.CreateCommentLog(commentLog);
        }

        public List<CommentLog> GetMissionCommentLogForManager(int managerId)
        {
            return commentLogRepository.GetMissionCommentLogForManager(managerId);
        }

        public List<CommentLog> GetMissionCommentLogForMember(int managerId)
        {
            return commentLogRepository.GetMissionCommentLogForMember(managerId);
        }

        public List<CommentLog> GetProjectCommentLogForManager(int managerId)
        {
            return commentLogRepository.GetProjectCommentLogForManager(managerId);
        }

        public List<CommentLog> GetProjectCommentLogForMember(int managerId)
        {
            return commentLogRepository.GetProjectCommentLogForMember(managerId);
        }
    }
}
