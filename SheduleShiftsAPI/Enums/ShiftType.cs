using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Enums
{
    public enum ShiftType
    {
        [Description("A normal morning shift is 7 am to 3 pm")]
        Morning,

        [Description("A normal afternon shift is 3 pm to 11 pm")]
        Aternon,

        [Description("A normal night shift is 11 pm to 7 am")]
        Night
    }
}
