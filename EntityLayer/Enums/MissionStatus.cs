using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Enums
{
    public enum MissionStatus
    {
        Active = 0,
        Waiting=1,
        Cancel = 2,
        Process=3,
        Done = 4,
        WaitingForApprove=5,
        Timeout=6,
    }
}
