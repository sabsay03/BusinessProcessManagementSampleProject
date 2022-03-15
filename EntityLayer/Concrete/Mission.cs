using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Mission
    {
        [Key]
        public int Id { get; set; }
        public int? MemberId{ get; set; }
        public User Member { get; set; }
        public string  Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Title { get; set; }
        public bool IsApproved { get; set; }

       // List
        public ICollection<Comment> Comments { get; set; }
        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}
