using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Models
{
    public class UserDetailedModel
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string UserFullName { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string StudentNo { get; set; }

    }
}
