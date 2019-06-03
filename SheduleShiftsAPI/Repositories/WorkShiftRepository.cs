using Common;
using Microsoft.EntityFrameworkCore;
using SheduleShiftsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Repositories
{
    public class WorkShiftRepository
    {
        SheduleShiftsContext Context;
        public WorkShiftRepository(SheduleShiftsContext context)
        {
            Context = context;
        }

        public async Task<object> GetAll()
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.ToListAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
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

        public async Task<object> GetAllOfEmployeeById(int employeeId)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.EmployeeId == employeeId).ToListAsync();

                if (workShifts.Count < 1 || workShifts == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The work shift of this position in this week it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
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

        public async Task<object> GetThisWeekOfEmployeeById(int employeeId)
        {
            ObjectResponse response;
            try
            {
                DateTime endOfWeek = DateTime.Today;
                for (; endOfWeek.DayOfWeek != DayOfWeek.Saturday;)
                    endOfWeek.AddDays(+1);

                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.EmployeeId == employeeId &&
                    ws.WorkDay.Date >= DateTime.Now &&
                    ws.WorkDay.Date <= endOfWeek.Date).ToListAsync();

                if (workShifts.Count < 1 || workShifts == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The work shift of this position in this week it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
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

        public async Task<object> GetThisMounthOfEmployeeById(int employeeId)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.EmployeeId == employeeId && 
                    ws.WorkDay.Date.Month == DateTime.Now.Month).ToListAsync();

                if(workShifts.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The list work shifts of this employee it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = false,
                    Value = "Smthing rong with database please try again"
                };

                return response;
            }
        }

        public async Task<object> GetOfEmployeeByIdFromDateToDate(int employeeId, 
            DateTime startDate, 
            DateTime endDate)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.EmployeeId == employeeId &&
                    ws.WorkDay.Date >= startDate.Date &&
                    ws.WorkDay.Date <= endDate.Date).ToListAsync();

                if(workShifts.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The list work shifts of this employee of this date it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthng rong with database pleasetry again"
                };

                return response;
            }
        }

        public async Task<object> GetAllOfPositionById(int positionId)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.WorkDay.PositionId == positionId).ToListAsync();

                if(workShifts.Count < 1 || workShifts == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The work shifts of this position it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
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

        public async Task<object> GetThisWeekOfPositionById(int positionId)
        {
            ObjectResponse response;
            try
            {
                DateTime endOfWeek = DateTime.Today;
                for (; endOfWeek.DayOfWeek != DayOfWeek.Saturday;)
                    endOfWeek.AddDays(+1);

                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.WorkDay.PositionId == positionId && 
                    ws.WorkDay.Date >= DateTime.Now.Date &&
                    ws.WorkDay.Date <= endOfWeek.Date).ToListAsync();

                if(workShifts.Count < 1 || workShifts == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The work shift of this position in this week it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
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

        public async Task<object> GetThisMonthOfPositionById(int positionId)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.WorkDay.PositionId == positionId &&
                    ws.WorkDay.Date.Month == DateTime.Now.Month).ToListAsync();

                if(workShifts.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The work shifts of this position in this month it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with database please try again"
                };

                return response;
            }
        }

        public async Task<object> GetOfPositionByIdFromDateToDate(int positionId,
            DateTime startDate,
            DateTime endDate)
        {
            ObjectResponse response;
            try
            {
                List<WorkShift> workShifts = await Context.WorkShifts.Where(
                    ws => ws.WorkDay.PositionId == positionId &&
                    ws.WorkDay.Date >= startDate.Date &&
                    ws.WorkDay.Date <= endDate).ToListAsync();

                if(workShifts.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The list of this position by this date it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShifts
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with database please try again"
                };

                return response;
            }
        }

        public async Task<object> Create(WorkShift workShift)
        {
            ObjectResponse response;
            try
            {
                await Context.WorkShifts.AddAsync(workShift);
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShift
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

        public async Task<object> Update(WorkShift workShift)
        {
            ObjectResponse response;
            try
            {
                WorkShift workShiftToUpdate = await Context.WorkShifts.FindAsync(workShift.Id);

                if (workShiftToUpdate == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of work day on the database plaese try again"
                    };

                    return response;
                }

                workShiftToUpdate.ShiftTypeId = workShift.ShiftTypeId;
                workShiftToUpdate.EmployeeId = workShift.EmployeeId;
                workShiftToUpdate.StartShift = workShift.StartShift;
                workShiftToUpdate.EndShift = workShift.EndShift;
                workShiftToUpdate.WorkDayId = workShift.WorkDayId;
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShiftToUpdate
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with database please try again"
                };

                return response;
            }
        }

        public async Task<object> RequestChangeShiftTime(int employeeId, 
            int workShiftId,
            DateTime startShift,
            DateTime endShift)
        {
            ObjectResponse response;
            try
            {

            }
            catch
            {

                throw;
            }
        }

        public async Task<object> DeleteById(int workShiftId)
        {
            ObjectResponse response;
            try
            {
                WorkShift workShift = await Context.WorkShifts.FindAsync(workShiftId);

                if(workShift == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of work day on the database plaese try again"
                    };

                    return response;
                }

                Context.WorkShifts.Remove(workShift);
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = workShift
                };

                return response;
            }
            catch
            {
                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = true,
                    Value = "Somthing rong with database please try again"
                };

                return response;
            }
        }
    }
}
