using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Enums
{
    public enum Active
    {
        [Description("The position is active and have a guard")]
        Active,
        [Description("The position it's not active at all")]
        NotActive,
        [Description("In the position there is no guard but there is a guard-post")]
        Center,
        [Description("The position is active but don't have a guard or pos-guard")]
        Empty
    }
}
