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

        public DropDownHelper(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
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
    }
}
