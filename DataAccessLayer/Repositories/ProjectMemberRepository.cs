using DataAccessLayer.Concrete;
using EntityLayer.Concrete.Relations;
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
    public class ProjectMemberRepository : IProjectMemberRepository
    {
        public ProjectMember getProjectMember(int memberId, int projectId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.ProjectMembers.Include(pm=> pm.Project).Include(pm=> pm.Member).Where(pm => pm.MemberId == memberId && pm.ProjecId == projectId).FirstOrDefault();
            }
        }
        public Tuple<List<UserDetailedModel>, int> GetMembers(int projectId, int pagenumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {

                var query = databaseContext.ProjectMembers.Include(pm => pm.Member).Include(pm => pm.Project).Where(pm => pm.ProjecId == projectId
                    && pm.ProjectMemberStatus == EntityLayer.Enums.ProjectMemberStatus.active).
                          Select(pm => new UserDetailedModel
                          {
                              Id = pm.Member.Id,
                              Firstname = pm.Member.FirstName,
                              Lastname = pm.Member.LastName,
                              Email = pm.Member.Email,
                              StudentNo = pm.Member.StudentNumber
                          }).OrderBy(pm => pm.Id).

                          Skip((pagenumber - 1) * pageSize).Take(pageSize).ToList() ;

                var allList = databaseContext.ProjectMembers.Include(pm => pm.Member).Include(pm => pm.Project).Where(pm => pm.ProjecId == projectId
                       && pm.ProjectMemberStatus == EntityLayer.Enums.ProjectMemberStatus.active).ToList();


                double pageCount = (double)((decimal)allList.Count() / Convert.ToDecimal(pageSize));
                int PageCount = (int)Math.Ceiling(pageCount);

                return new Tuple<List<UserDetailedModel>,int>(query, PageCount);

            }
        }
        public int SaveProjectMember(ProjectMemberModel projectMember)
        {
            using (Context databaseContext = new Context())
            {
                ProjectMember member = new ProjectMember
                {
                    MemberId = projectMember.MemberId,
                    ProjecId = projectMember.ProjecId,
                    ProjectMemberStatus = EntityLayer.Enums.ProjectMemberStatus.active

                };
                databaseContext.ProjectMembers.Add(member);
                databaseContext.SaveChanges();
                return member.Id;
            }
        }

        public IPagedList<ProjectModel> GetProjectsForMember(int memberId, int pagenumber, int pageSize, string searchFilter)
        {
            using (Context databaseContext = new Context())
            {

                var query = databaseContext.ProjectMembers.Include(pm => pm.Member).Include(pm => pm.Project).Where(pm => pm.MemberId == memberId
                    && pm.ProjectMemberStatus == EntityLayer.Enums.ProjectMemberStatus.active).
                        Select(p => new ProjectModel
                        {
                            Id = p.Project.Id,
                            Description = p.Project.Description,
                            Title = p.Project.Title,
                            StartDate = p.Project.StartDate,
                            EndDate = p.Project.EndDate,
                            ManagerId = p.Project.ManagerId,
                            ProjectStatus = p.Project.ProjectStatus
                        });


                //if (!String.IsNullOrEmpty(searchFilter))
                //    query = query.Where(p =>
                //        p..ToLower().Contains(searchFilter.ToLower())
                //        );

                return query.OrderBy(p => p.Id).ToPagedList(pagenumber, pageSize);

            }
        }
    }
}
