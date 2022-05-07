using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Models;
using EntityLayer.Repositories;
using Microsoft.EntityFrameworkCore;
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

        public Mission GetById(int id)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p=>p.Member).Include(p=>p.Project).Where(p => p.Id == id).FirstOrDefault();
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
                        LastName=t.Member.LastName,
                        FirstName=t.Member.FirstName
                    }).OrderBy(t => t.Id)
                    .Skip((pagenumber - 1) * pageSize).Take(pageSize).ToList();

                var allList = databaseContext.Tasks.Include(pm => pm.Member).Include(pm => pm.Project).Where(pm => pm.ProjectId == projectId).ToList();

                double pageCount = (double)((decimal)allList.Count() / Convert.ToDecimal(pageSize));
                int PageCount = (int)Math.Ceiling(pageCount);

                return new Tuple<List<MissionModel>, int>(query, PageCount);
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
                    MissionStatus = EntityLayer.Enums.MissionStatus.Active
                };

                databaseContext.Tasks.Add(model);
                databaseContext.SaveChanges();

                return model.Id;
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
    }
}
