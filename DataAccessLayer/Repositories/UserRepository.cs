using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.Models;
using EntityLayer.Repositories;
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
        public User GetUserByStudentNo(string studentNo)
        {
            using (Context databaseContext = new Context())
            {
                return databaseContext.Users.Where(u => u.StudentNumber == studentNo).FirstOrDefault();
            }
        }
    }
}

