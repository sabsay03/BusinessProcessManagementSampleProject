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
    public class CheckMissionStatusJob:IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("CheckMissionStatus başladı");
            IProjectRepository projectRepository = new ProjectRepository();
            IUserRepository userRepository = new UserRepository();
            ICommentLogRepository commentLogRepository = new CommentLogRepository();
            IMissionRepository missionRepository = new MissionRepository();

            var missions = missionRepository.GetsWaitingProcessMission();
            foreach (var item in missions)
            {
                if (item.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting)
                {
                    if (DateTime.Now >= item.StartDate)
                    {
                        var date = item.StartDate.ToString("dd MMM yyyy");
                        CommentLog log = new CommentLog
                        {
                            ProjectId = item.ProjectId,
                            Date = DateTime.Now,
                            MissionId=item.Id,
                            Text = ($"{item.Title} İsimli Görev {date} itibariyle  başlamıştır."),
                            commentType = EntityLayer.Enums.CommentType.Process
                        };
                        commentLogRepository.CreateCommentLog(log);
                        missionRepository.UpdateMissionStatus(item.Id, EntityLayer.Enums.MissionStatus.Process);
                        Console.WriteLine($"{item.Id} li Görev güncellendi{EntityLayer.Enums.MissionStatus.Process}");
                    }
                }
                else
                {
                    if (DateTime.Now >= item.EndDate)
                    {
                        var date = item.StartDate.ToString("dd MMM yyyy");
                        CommentLog log = new CommentLog
                        {
                            ProjectId = item.ProjectId,
                            Date = DateTime.Now,
                            MissionId = item.Id,
                            Text = ($"{item.Title} İsimli Görev {date} itibariyle  Bitmiştir."),
                            commentType = EntityLayer.Enums.CommentType.Timeout
                        };
                        commentLogRepository.CreateCommentLog(log);
                        missionRepository.UpdateMissionStatus(item.Id, EntityLayer.Enums.MissionStatus.Timeout);
                        Console.WriteLine($"{item.Id} li Görev güncellendi{EntityLayer.Enums.MissionStatus.Timeout}");
                    }

                }
            }
            var now = DateTime.Now.ToString("dd MMM yyyy HH : mm : ss");
            Console.WriteLine($" Merhaba, gün saat şuan {now}");
            return Task.CompletedTask;
        }
    }
}

