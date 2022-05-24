using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Enums
{
    public enum CommentType
    {
        Active = 0,
        Cancel = 1,
        Process = 2,
        Done = 3,
        WaitingForApprove = 4,
        Timeout = 5,
    }
}
