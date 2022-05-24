using EntityLayer.Concrete.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EntityLayer.Concrete
{
   public class User:IdentityUser<int>
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string StudentNumber { get; set; }

        //list

        public ICollection<ProjectMember> ProjectMembers { get; set; }
        public ICollection<Mission> Tasks { get; set; }







    }
}
