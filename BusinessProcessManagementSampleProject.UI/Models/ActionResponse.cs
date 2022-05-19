using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ActionResponse
    {
        public ActionType? ActionType { get; set; }
        public string ActionMessage { get; set; }
    }
}
