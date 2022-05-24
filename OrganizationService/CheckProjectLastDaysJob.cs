using DataAccessLayer.Repositories;
using EntityLayer.Repositories;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Enums;
using BusinessLayer.Helpers;
using EntityLayer.Concrete;

namespace OrganizationService
{
    public class CheckProjectLastDaysJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("lastday başladı");
            IProjectRepository projectRepository = new ProjectRepository();
            IUserRepository userRepository = new UserRepository();
            ICommentLogRepository commentLogRepository = new CommentLogRepository();

            var projects = projectRepository.GetAllActiveProcessProject();

            foreach (var item in projects)
            {
                if (item.ProjectStatus == ProjectStatus.Active && DateTime.Now<item.StartDate && (item.StartDate - DateTime.Now).Days < 5)
                {
                    string body = ($"{item.Id}'li {item.Title} isimli  Projenizin başlamasına {Math.Abs(item.StartDate.Day - DateTime.Now.Day)} günden az kaldı.");
                    string subject = "Proje bildirim";
                    var manager = userRepository.GetUserById(item.ManagerId);
                    MailService.SendMail(item.Id, body, subject, manager.Email);

                    CommentLog log = new CommentLog
                    {
                        ProjectId = item.Id,
                        Date = DateTime.Now,
                        Text = ($"{item.Title} isimli  Projenizin başlamasına {Math.Abs(item.StartDate.Day - DateTime.Now.Day)} günden az kaldı."),
                        commentType = EntityLayer.Enums.CommentType.Done
                    };
                    commentLogRepository.CreateCommentLog(log);
                    foreach (var member in item.ProjectMembers)
                    {
                        string body1 = ($"{item.Id}'li {item.Title} isimli  Projenizin başlamasına {Math.Abs(item.StartDate.Day - DateTime.Now.Day)} günden az kaldı.");
                        string subject1 = "Proje bildirim";

                        MailService.SendMail(item.Id, body1, subject1, member.Member.Email);
                    }
                }if(item.ProjectStatus == ProjectStatus.Process && DateTime.Now<item.EndDate &&(item.EndDate - DateTime.Now).Days < 5)
                {
                    string body = ($"{item.Id}'li {item.Title} isimli  Projenizin bitmesine {Math.Abs(item.EndDate.Day - DateTime.Now.Day)} günden az kaldı.");
                    string subject = "Proje bildirim";
                    var manager = userRepository.GetUserById(item.ManagerId);
                    MailService.SendMail(item.Id, body, subject, manager.Email);
                    CommentLog log = new CommentLog
                    {
                        ProjectId = item.Id,
                        Date = DateTime.Now,
                        Text = ($"{item.Title} isimli  Projenizin bitmesine {Math.Abs(item.EndDate.Day - DateTime.Now.Day)} günden az kaldı."),
                        commentType = EntityLayer.Enums.CommentType.Done
                    };
                    commentLogRepository.CreateCommentLog(log);
                    foreach (var member in item.ProjectMembers)
                    {
                        string body1 = ($"{item.Id}'li {item.Title} isimli  Projenizin bitmesine {Math.Abs(item.EndDate.Day - DateTime.Now.Day)} günden az kaldı.");
                        string subject1 = "Proje bildirim";

                        MailService.SendMail(item.Id, body1, subject1, member.Member.Email);
                    }
                }


            }
            return Task.CompletedTask;
        }
    }
}