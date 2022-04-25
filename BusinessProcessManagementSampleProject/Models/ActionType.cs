using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VeriPark.DigitalBadge.Backoffice.Models
{
    public enum ActionType
    {
        UserStatusUpdated = 1,
        CompanyApproved = 2,
        CompanyCombined = 3,
        BadgeRequestRedirected = 4,
        BadRequestRejected = 5,
        InvalidIncomingFile = 6,
        BadgeStatusUpdated=7,
        BadgeDelete=8
    }
}