using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Models;
using EntityLayer.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public List<User> GetAllTeacher()
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Users.Where(u => u.StudentNumber == null).ToList();
            }
        }

        public User GetUserById(int userId)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Users.Include(u=>u.ProjectMembers).ThenInclude(pm=>pm.Project).Include(u=>u.Tasks).Where(u => u.Id == userId).FirstOrDefault();
            }
        }

        public User GetUserByStudentNo(string studentNo)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Users.Where(u => u.StudentNumber == studentNo).FirstOrDefault();
            }
        }

    }
}

