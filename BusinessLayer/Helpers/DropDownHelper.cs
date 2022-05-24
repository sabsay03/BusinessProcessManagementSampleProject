using EntityLayer.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public class DropDownHelper
    {
        private readonly IProjectRepository projectRepository;
        private readonly IUserRepository userRepository;
        private readonly IProjectMemberRepository projectMemberRepository;

        public DropDownHelper(IProjectRepository projectRepository,IUserRepository userRepository,IProjectMemberRepository projectMemberRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.projectMemberRepository = projectMemberRepository;
        }

        public List<SelectListItem> ProjectSelectListByManager(int? managerId)
        {
            var projects = projectRepository.GetActiveProjects(managerId).Select(p =>
            new SelectListItem
            {
                Text = p.Title,
                Value = p.Id.ToString()
            }
            ).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--- Seçiniz ---", Value = "" });
            items.InsertRange(items.Count, projects);
            return items;
        }
        public List<SelectListItem> GetProjectMember(int projectId)
        {
            var members = projectMemberRepository.GetActiveProjectMember(projectId).Select(p =>
            new SelectListItem
            {
                Text = ($"{p.Member.FirstName} {p.Member.LastName} "),
                Value = p.MemberId.ToString()
            }
            ).ToList();

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--- Seçiniz ---", Value = "" });
            items.Add(new SelectListItem { Text = "Genel", Value = "99" });
            items.InsertRange(items.Count, members);
            return items;
        }
        public List<SelectListItem> GetAllTeacherSelectList()
        {
            var projects = userRepository.GetAllTeacher().Select(p =>
            new SelectListItem
            {
                Text = p.FirstName+' '+p.LastName,
                Value = p.Id.ToString()
            }
            ).ToList();
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "--- Seçiniz ---", Value = "" });
            items.InsertRange(items.Count, projects);
            return items;
        }
    }
}
