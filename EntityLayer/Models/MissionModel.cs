using EntityLayer.Concrete;
using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class MissionModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int ManagerId { get; set; }
        public string StudentNumber { get; set; }

        public string StudenFullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ManagerName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FeedBack { get; set; }
        public int ProjectId { get; set; }
        public MissionStatus MissionStatus { get; set; }
        public string MissionStatusString { get; set; }

        public User Manager { get; set; }

    }
}
