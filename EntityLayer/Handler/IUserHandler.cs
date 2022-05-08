using EntityLayer.Models;
using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Handler
{
    public interface IUserHandler
    {
        UserDetailedModel GetUserByStudenNo(string studentNumber);

    }
}
