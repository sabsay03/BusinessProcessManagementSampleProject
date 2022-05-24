using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Enums;
using EntityLayer.Models;
using EntityLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class MissionRepository : IMissionRepository
    {
        public int DeleteMission(int id)
        {
            using (Context databaseContext = new Context())
            {
                var mission = GetById(id);
                databaseContext.Tasks.Attach(mission);
                mission.MissionStatus = EntityLayer.Enums.MissionStatus.Cancel;
                databaseContext.SaveChanges();
                return id;

            }
        }

        public List<Mission> GetAllProjectMission(int projectId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(t => t.Project).Include(t => t.Member).Where(t => t.ProjectId == projectId).ToList();
            }
        }

        public List<Mission> GetAllWaitingProcessMission()
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p => p.Member).Include(p => p.Project)
                    .Where(p => p.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting  || p.MissionStatus == EntityLayer.Enums.MissionStatus.Process )
                    .ToList();
            }
        }

        public Mission GetById(int id)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p => p.Member).Include(p => p.Project).Where(p => p.Id == id).FirstOrDefault();
            }
        }

        public Tuple<List<MissionModel>, int> GetMissionByProjectId(int projectId, int pagenumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {

                var query = databaseContext.Tasks.Include(t => t.Member).Include(t => t.Project).Where(t => t.ProjectId == projectId).
                    Select(t => new MissionModel
                    {
                        Id = t.Id,
                        StudentId = Convert.ToInt32(t.MemberId),
                        Title = t.Title,
                        Description = t.Description,
                        StartDate = t.StartDate,
                        EndDate = t.EndDate,
                        StudentNumber = t.Member.StudentNumber,
                        ProjectId = t.ProjectId,
                        LastName = t.Member.LastName,
                        FirstName = t.Member.FirstName,
                        MissionStatus = t.MissionStatus
                    }).OrderBy(t => t.Id)
                    .Skip((pagenumber - 1) * pageSize).Take(pageSize).ToList();

                var allList = databaseContext.Tasks.Include(pm => pm.Member).Include(pm => pm.Project).Where(pm => pm.ProjectId == projectId).ToList();

                double pageCount = (double)((decimal)allList.Count() / Convert.ToDecimal(pageSize));
                int PageCount = (int)Math.Ceiling(pageCount);

                return new Tuple<List<MissionModel>, int>(query, PageCount);
            }
        }

        public MissionModel GetMissionDetailById(int id)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p => p.Member).Include(p => p.Project).ThenInclude(p => p.Manager).Where(p => p.Id == id).Select(p => new MissionModel
                {
                    Id = p.Id,
                    Description = p.Description,
                    Title = p.Title,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    MissionStatus = p.MissionStatus,
                    ProjectId = p.ProjectId,
                    FirstName = p.Member.FirstName,
                    LastName = p.Member.LastName,
                    StudentId = (int)p.MemberId,
                    StudentNumber = p.Member.StudentNumber,
                    ManagerId = (int)p.Project.ManagerId,
                    ManagerName = p.Project.Manager.FirstName,
                    Manager = p.Project.Manager,
                    FilePath = p.FilePath,
                    FeedBack = p.FeedBack
                }).FirstOrDefault();
            }
        }


        public Mission GetMissionForCheck(int memberId, string title, int projectId, int id)
        {
            using (Context databaseContext = new Context())
            {
                if (id == 0)
                {
                    return databaseContext.Tasks.Where(p => p.MemberId == memberId && p.Title == title && p.ProjectId == projectId).FirstOrDefault();

                }
                else
                {
                    return databaseContext.Tasks.Where(p => p.Id != id && p.MemberId == memberId && p.Title == title && p.ProjectId == projectId).FirstOrDefault();

                }
            }
        }

        public IPagedList<MissionModel> GetMissionForMember(int memberId, int pageNumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {

                var query = databaseContext.Tasks.Include(pm => pm.Member).Include(pm => pm.Project).ThenInclude(p => p.Manager)
                    .Where(pm => pm.MemberId == memberId).
                        Select(p => new MissionModel
                        {
                            Id = p.Id,
                            Description = p.Description,
                            Title = p.Title,
                            StartDate = p.StartDate,
                            EndDate = p.EndDate,
                            MissionStatus = p.MissionStatus,
                            ProjectId = p.ProjectId,
                            FirstName = p.Member.FirstName,
                            LastName = p.Member.LastName,
                            StudentId = (int)p.MemberId,
                            StudentNumber = p.Member.StudentNumber,
                            ManagerId = (int)p.Project.ManagerId,
                            ManagerName = p.Project.Manager.FirstName
                        });


                //if (!String.IsNullOrEmpty(searchFilter))
                //    query = query.Where(p =>
                //        p..ToLower().Contains(searchFilter.ToLower())
                //        );

                return query.OrderBy(p => p.Id).ToPagedList(pageNumber, pageSize);
            }
        }

        public List<Mission> GetProjectMissionForMember(int memberUd, int projectId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(t => t.Project).Include(t => t.Member).Where(t => t.ProjectId == projectId && t.MemberId == memberUd).ToList();
            }
        }

        public List<Mission> GetsWaitingProcessMission()
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p => p.Member).Include(p => p.Project)
                    .Where(p => p.MissionStatus == EntityLayer.Enums.MissionStatus.Waiting || p.MissionStatus == EntityLayer.Enums.MissionStatus.Process)
                    .ToList();
            }
        }

        public int SaveMission(MissionModel missionModel)
        {
            using (Context databaseContext = new Context())
            {
                Mission model = new Mission
                {
                    Title = missionModel.Title,
                    Description = missionModel.Description,
                    EndDate = missionModel.EndDate,
                    StartDate = missionModel.StartDate,
                    ProjectId = missionModel.ProjectId,
                    MemberId = missionModel.StudentId,
                    MissionStatus = EntityLayer.Enums.MissionStatus.Waiting
                };

                databaseContext.Tasks.Add(model);
                databaseContext.SaveChanges();

                return model.Id;
            }
        }

        public int UpdateForApprove(int missionId, string feedBack)
        {
            using (Context databaseContext = new Context())
            {
                var entity = GetById(missionId);
                databaseContext.Tasks.Attach(entity);

                entity.FeedBack = feedBack;
                entity.MissionStatus = EntityLayer.Enums.MissionStatus.Done;
                databaseContext.SaveChanges();

                return entity.Id;
            }
        }

        public int UpdateForFinishMission(int missionId, string filePath)
        {
            using (Context databaseContext = new Context())
            {
                var entity = GetById(missionId);
                databaseContext.Tasks.Attach(entity);

                entity.FilePath = filePath;
                entity.MissionStatus = EntityLayer.Enums.MissionStatus.WaitingForApprove;

                databaseContext.SaveChanges();

                return entity.Id;
            }
        }

        public int UpdateForSendBack(int missionId, string feedBack)
        {
            using (Context databaseContext = new Context())
            {
                var entity = GetById(missionId);
                databaseContext.Tasks.Attach(entity);

                entity.FeedBack = feedBack;
                entity.MissionStatus = EntityLayer.Enums.MissionStatus.Process;
                databaseContext.SaveChanges();

                return entity.Id;
            }
        }

        public int UpdateMission(MissionModel missionModel)
        {
            using (Context databaseContext = new Context())
            {
                var entitiy = GetById(missionModel.Id);
                databaseContext.Tasks.Attach(entitiy);


                entitiy.Title = missionModel.Title;
                entitiy.Description = missionModel.Description;
                entitiy.EndDate = missionModel.EndDate;
                entitiy.StartDate = missionModel.StartDate;

                databaseContext.SaveChanges();

                return entitiy.Id;
            }
        }

        public int UpdateMissionStatus(int missionId, MissionStatus status)
        {
            using (Context databaseContext = new Context())
            {
                var entity = GetById(missionId);
                databaseContext.Tasks.Attach(entity);

                entity.MissionStatus = status;
                databaseContext.SaveChanges();

                return entity.Id;
            }
        }

        public int UpdatForExtraTime(int missionId, string feedBack, DateTime endDate, DateTime startDate)
        {
            using (Context databaseContext = new Context())
            {
                var entity = GetById(missionId);
                databaseContext.Tasks.Attach(entity);

                entity.FeedBack = feedBack;
                entity.StartDate = startDate;
                entity.EndDate = endDate;
                entity.MissionStatus = EntityLayer.Enums.MissionStatus.Process;

                databaseContext.SaveChanges();

                return entity.Id;
            }
        }
    }
}
