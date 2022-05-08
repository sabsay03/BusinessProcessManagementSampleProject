using EntityLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class ProjectRequestModel
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProjecId { get; set; }
        public int ManagerId { get; set; }
        public string ProjectTitle { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string StudentNo { get; set; }

        public ProjectRequestStatus ProjectRequestStatus { get; set; }
    }
}
