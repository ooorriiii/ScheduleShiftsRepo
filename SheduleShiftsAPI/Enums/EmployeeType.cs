using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Enums
{
    public enum EmployeeType
    {
        Controller,
        Gurd,
        Cleaner,
        [Description("A gurd station, is not defiend like a normal employee")]
        GurdStation
    }
}
