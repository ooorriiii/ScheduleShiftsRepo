using Common;
using Microsoft.EntityFrameworkCore;
using SheduleShiftsAPI.Enums;
using SheduleShiftsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Repositories
{
    public class WorkDayRepository
    {
        private SheduleShiftsContext Context;
        public WorkDayRepository(SheduleShiftsContext context)
        {
            Context = context;
        }
        public async Task<object> Create(WorkDay newWorkDay)
        {
            ObjectResponse response;
            try
            {
                IQueryable<WorkDay> workDays = Context.WorkDays
                    .Where(wd => wd.PositionId == newWorkDay.PositionId);

                if (workDays.Count() < 1)
                {
                    WorkDay workDay = new WorkDay
                    {
                        Day = newWorkDay.Day,
                        CreatedById = newWorkDay.CreatedById,
                        Date = newWorkDay.Date == null ? DateTime.Now : default,
                        PositionId = newWorkDay.PositionId
                    };

                    await Context.WorkDays.AddAsync(workDay);
                    Context.SaveChanges();

                    response = new ObjectResponse
                    {
                        IsPassed = true,
                        Value = newWorkDay
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = false,
                    Value = "This work day it's allredy defined in this position"
                };

                return response;
            }
            catch (Exception)
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthig rong with the database plaese try again again"
                };

                return response;
            }
        }

        public async Task<object> DeleteById(int id)
        {
            ObjectResponse response;
            try
            {
                WorkDay workDay = await Context.WorkDays.FindAsync(id);

                if (workDay == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of work day please try again"
                    };

                    return response;
                }

                Context.WorkDays.Remove(workDay);
                Context.SaveChanges();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workDay
                };

                return response;
            }
            catch (Exception)
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with the database please try again"
                };

                return response;
            }
        }

        public async Task<object> GetById(int id)
        {
            ObjectResponse response;
            try
            {
                WorkDay workDay = await Context.WorkDays.FindAsync(id);

                if (workDay == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of work day please try again"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workDay
                };

                return response;
            }
            catch (Exception)
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with the database please try again"
                };

                return response;
            }
        }

        public async Task<object> GetAll()
        {
            ObjectResponse response;
            try
            {
                IList<WorkDay> workDays = await Context.WorkDays.ToListAsync();

                if (workDays.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The table of work days it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workDays
                };

                return response;
            }
            catch (Exception)
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with the database please try again"
                };

                return response;
            }
        }

        public async Task<object> GetOfEmployeeById(int employeeId)
        {
            ObjectResponse response;
            try
            {
                List<WorkDay> workDays = await Context.WorkDays.ToListAsync();
                List<WorkDay> workDayOfEmployee = new List<WorkDay>();
                foreach (WorkDay workDay in workDays)
                {
                    List<WorkShift> workShifts = workDay.Shifts
                        .Where(s => s.EmployeeId == employeeId).ToList();

                    if (workShifts.Count > 0)
                        workDayOfEmployee.Add(workDay);
                }

                if (workDayOfEmployee.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Sorry but you don't have work shift at all"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workDayOfEmployee
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with the database please try again"
                };

                return response;
            }
        }

        public async Task<object> Update(WorkDay workDay)
        {
            ObjectResponse response;
            try
            {
                WorkDay workDayDB = await Context.WorkDays.FindAsync(workDay.Id);

                if (workDayDB == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of work day on the database plaese try again"
                    };

                    return response;
                }

                workDayDB.CreatedById = workDay.CreatedById;
                workDayDB.PositionId = workDay.PositionId;
                workDayDB.Date = workDay.Date;
                workDayDB.Shifts = workDay.Shifts;
                workDayDB.DayId = workDay.DayId;
                Context.SaveChanges();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workDayDB
                };

                return response;
            }
            catch (Exception)
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with the database please try again"
                };

                return response;
            }

        }
    }
}
