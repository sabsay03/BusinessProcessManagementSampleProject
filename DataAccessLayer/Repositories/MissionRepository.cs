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
        public Mission GetById(int id)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Tasks.Include(p=>p.Member).Include(p=>p.Project).Where(p => p.Id == id).FirstOrDefault();
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
