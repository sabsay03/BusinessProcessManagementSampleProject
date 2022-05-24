using BusinessLayer.Helpers;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrganizationService
{
    public class CheckMissionLastDaysJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {

            Console.WriteLine("missionCheck başladı");
            IMissionRepository missionRepository = new MissionRepository();
            IUserRepository userRepository = new UserRepository();
            ICommentLogRepository commentLogRepository = new CommentLogRepository();


            var missions = missionRepository.GetAllWaitingProcessMission();

            foreach (var item in missions)
            {
                if (item.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting && DateTime.Now < item.StartDate && (item.StartDate - DateTime.Now).Days < 3)
                {
                    string body = ($"{item.Id} Id'li {item.Title} isimli  Görevinizin başlamasına {item.StartDate.Day - DateTime.Now.Day} günden az kaldı.");
                    string subject = "Proje bildirim";
                    var manager = userRepository.GetUserById(item.Project.ManagerId);
                    MailService.SendMail(item.Id, body, subject, manager.Email);
                    MailService.SendMail(item.Id, body, subject, item.Member.Email);
                    CommentLog log = new CommentLog
                    {
                        ProjectId = item.ProjectId,
                        Date = DateTime.Now,
                        MissionId=item.Id,
                        Text = ($"{item.Title} isimli  görevinizin başlamasına {item.StartDate.Day-DateTime.Now.Day} günden az kaldı."),
                        commentType = EntityLayer.Enums.CommentType.Done
                    };
                    commentLogRepository.CreateCommentLog(log);
                }
                 if(item.MissionStatus == EntityLayer.Enums.MissionStatus.Process && DateTime.Now < item.EndDate && (item.EndDate - DateTime.Now).Days < 3)
                {
                    string body = ($"{item.Id} Id'li {item.Title} isimli  Görevinizin bitmesine {item.EndDate.Day - DateTime.Now.Day} günden az kaldı.");
                    string subject = "Proje bildirim";
                    var manager = userRepository.GetUserById(item.Project.ManagerId);
                    MailService.SendMail(item.Id, body, subject, manager.Email);
                    MailService.SendMail(item.Id, body, subject, item.Member.Email);
                    CommentLog log = new CommentLog
                    {
                        ProjectId = item.ProjectId,
                        Date = DateTime.Now,
                        MissionId = item.Id,
                        Text = ($"{item.Title} isimli  Görevinizin bitmesine {item.EndDate.Day - DateTime.Now.Day} günden az kaldı."),
                        commentType = EntityLayer.Enums.CommentType.Done
                    };
                    commentLogRepository.CreateCommentLog(log);
                }
            }
             
            return Task.CompletedTask;
        }
    }
}
