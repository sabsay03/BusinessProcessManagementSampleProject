using EntityLayer.Handler;
using EntityLayer.Models;
using EntityLayer.Repositories;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Handler
{
    public class UserHandler:IUserHandler
    {
        private readonly IUserRepository userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

      

        public UserDetailedModel GetUserByStudenNo(string studentNumber)
        {
            var user = userRepository.GetUserByStudentNo(studentNumber);

            if (user == null)
            {
                return null;
            }
            var userModel = new UserDetailedModel
            {
                Id = user.Id,
                Firstname=user.FirstName,
                Lastname=user.LastName,
                UserFullName= $"{user.FirstName} {user.LastName}",
                Email=user.Email,
                StudentNo=user.StudentNumber,

            };
            return userModel;            
        }
    }
}
