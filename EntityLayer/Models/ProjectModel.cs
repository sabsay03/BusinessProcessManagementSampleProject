using EntityLayer.Concrete;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int ManagerId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string FilePath { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public string ProjectStatusString { get; set; }
        public DateTime CreateDate { get; set; }
        public User Manager { get; set; }


    }
}
