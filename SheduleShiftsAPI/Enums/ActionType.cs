using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Enums
{
    public enum ActionType
    {
        [Description("Create new work shift")]
        Create,

        [Description("Edit exist work shift")]
        Edit,

        [Description("Delete exist work shift")]
        Delete,

        [Description("Employee request to cancel the shift he was supposed to work")]
        RequestCancel,

        [Description("Employee request for finish the shift earlier")]
        RequestFinishEarlier,

        [Description("The employee is supposed to work after confirmed to start earlier")]
        FinishEarlierEmployeeConfirmed,

        [Description("The manager confirmed the changes")]
        FinishEarlierManagerConfirmed,

        [Description("Employee request for start the shift later")]
        RequestStartLater,

        [Description("The employee is working right now confirmed to finish later")]
        StartLaterEmployeeConfirmed,

        [Description("Manager confirmed the changes")]
        StartLaterManagerConfirmed,

        [Description("The shift started")]
        ShiftStart,

        [Description("The shift ended")]
        ShiftFinish,

        [Description("Employee request for the change shift with the other employee")]
        RequestChange,

        [Description("The employee confirmed the request for the change shift")]
        EmployeeConfirmedChange,

        [Description("Manager confirmed the change shift request")]
        ManagerConfirmedChange

    }
}
