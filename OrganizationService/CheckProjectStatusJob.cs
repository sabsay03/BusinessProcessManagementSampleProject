using System;
using System.Threading.Tasks;
using BusinessLayer.Helpers;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using EntityLayer.Handler;
using EntityLayer.Repositories;
using Quartz;

namespace OrganizationService
{
    public class CheckProjectStatusJob : IJob
    {

        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("checkstatus başladı");
            IProjectRepository projectRepository = new ProjectRepository();
            IUserRepository userRepository = new UserRepository();
            ICommentLogRepository commentLogRepository = new CommentLogRepository();

            var projects = projectRepository.GetAllActiveProcessProject();
            foreach (var item in projects)
            {
                if (item.ProjectStatus == EntityLayer.Enums.ProjectStatus.Active)
                {
                    if (DateTime.Now >= item.StartDate)
                    {
                        var date = item.StartDate.ToString("dd MMM yyyy");
                        CommentLog log = new CommentLog
                        {
                            ProjectId = item.Id,
                            Date = DateTime.Now,
                            Text = ($"{item.Title} İsimli Proje {date} itibariyle  başlamıştır."),
                            commentType = EntityLayer.Enums.CommentType.Done
                        };
                        commentLogRepository.CreateCommentLog(log);
                        projectRepository.UpdateProjectStatus(item.Id, EntityLayer.Enums.ProjectStatus.Process);
                        Console.WriteLine($"{item.Id} li proje güncellendi{EntityLayer.Enums.ProjectStatus.Process}");
                    }
               }
                else
                {
                    var date = item.EndDate.ToString("dd MMM yyyy");
                    if (DateTime.Now > item.EndDate)
                    {
                        if (item.Tasks != null)
                        {
                            bool check = true;
                            foreach (var task in item.Tasks)
                            {
                                if (task.MissionStatus != EntityLayer.Enums.MissionStatus.Done && task.MissionStatus != EntityLayer.Enums.MissionStatus.Cancel)
                                {
                                    projectRepository.UpdateProjectStatus(item.Id, EntityLayer.Enums.ProjectStatus.TimeOut);
                                    Console.WriteLine($"{item.Id} Id'li proje güncellendi{EntityLayer.Enums.ProjectStatus.TimeOut}");

                                    CommentLog log = new CommentLog
                                    {
                                        ProjectId = item.Id,
                                        Date = DateTime.Now,
                                        Text = ($"{item.Title} İsimli Proje ve projenin görevleri {date} itibariyle süresi bitmiştir."),
                                        commentType = EntityLayer.Enums.CommentType.Timeout
                                    };
                                    commentLogRepository.CreateCommentLog(log);


                                    string body = ($"{item.Id} Id'li {item.Title} isimli Projenizin zamanı dolmuştur.");
                                    string subject = "Proje bildirim";
                                    var manager = userRepository.GetUserById(item.ManagerId);
                                    MailService.SendMail(item.Id,body,subject,manager.Email);

                                    break;
                                }
                                if (check == true)
                                {

                                    projectRepository.UpdateProjectStatus(item.Id, EntityLayer.Enums.ProjectStatus.Done);
                                    Console.WriteLine($"{item.Id} li proje güncellendi{EntityLayer.Enums.ProjectStatus.Done}");

                                    CommentLog log = new CommentLog
                                    {
                                        ProjectId = item.Id,
                                        Date = DateTime.Now,
                                        Text = ($"{item.Title} İsimli Proje Tamamlanmıştır."),
                                        commentType = EntityLayer.Enums.CommentType.Done
                                    };
                                    commentLogRepository.CreateCommentLog(log);
                                    string body = ($"{item.Id} Id'li {item.Title} isimli Projeniz Tamamlandı.");
                                    string subject = "Proje bildirim";
                                    var manager = userRepository.GetUserById(item.ManagerId);
                                    MailService.SendMail(item.Id, body, subject, manager.Email); check = false;
                                }
                            }
                        }
                        else
                        {
                            projectRepository.UpdateProjectStatus(item.Id, EntityLayer.Enums.ProjectStatus.TimeOut);
                            Console.WriteLine($"{item.Id} Id'li proje güncellendi{EntityLayer.Enums.ProjectStatus.TimeOut}");
                            CommentLog log = new CommentLog
                            {
                                ProjectId = item.Id,
                                Date = DateTime.Now,
                                Text = ($"{item.Title} İsimli Proje ve projenin görevleri {date} itibariyle süresi bitmiştir."),
                                commentType = EntityLayer.Enums.CommentType.Timeout
                            };
                            commentLogRepository.CreateCommentLog(log);
                            string body = ($"{item.Id} Id'li {item.Title} isimli Projenizin zamanı dolmuştur.");
                            string subject = "Proje bildirim";
                            var manager = userRepository.GetUserById(item.ManagerId);
                            MailService.SendMail(item.Id, body, subject, manager.Email);
                        }

                    }

                }
            }
            var now = DateTime.Now.ToString("dd MMM yyyy HH : mm : ss");
            Console.WriteLine($" Merhaba, gün saat şuan {now}");
            return Task.CompletedTask;
        }
    }
}
