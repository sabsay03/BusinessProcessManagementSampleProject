using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeriPark.DigitalBadge.Backoffice.Models
{
    public class ActionResponse
    {
        public ActionType? ActionType { get; set; }
        public string ActionMessage { get; set; }
    }
}