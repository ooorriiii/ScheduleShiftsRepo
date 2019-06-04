using Common;
using Microsoft.EntityFrameworkCore;
using SheduleShiftsAPI.Models;
using SheduleShiftsAPI.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SheduleShiftsAPI.Repositories
{
    public class AreaManagerRepository
    {
        SheduleShiftsContext Context;
        public AreaManagerRepository(SheduleShiftsContext context)
        {
            Context = context;
        }

        public async Task<object> Register(AreaManager newAreaManager)
        {
            ObjectResponse response;
            try
            {
                Employee employee = Context.Employees.Where(
                    e => e.IdNumber == newAreaManager.IdNumber).ToList().LastOrDefault();
                if (employee == null)
                {
                    AreaManager areaManager = Context.AreaManagers.Where(
                        am => am.IdNumber == newAreaManager.IdNumber).ToList().LastOrDefault();
                    if (areaManager == null)
                    {
                        await Context.AreaManagers.AddAsync(newAreaManager);
                        await Context.SaveChangesAsync();

                        response = new ObjectResponse
                        {
                            IsPassed = true,
                            Value = newAreaManager
                        };

                        return response;
                    }
                }

                response = new ObjectResponse
                {
                    IsPassed = false,
                    IsException = false,
                    Value = "The id number it's allredy exist"
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

        public object Login(string idNumber, string password)
        {
            ObjectResponse response;
            try
            {
                    AreaManager areaManager = Context.AreaManagers.Where(
                        m => m.IdNumber == idNumber).ToList().LastOrDefault();
                    if (areaManager == null)
                    {
                        response = new ObjectResponse
                        {
                            IsPassed = false,
                            IsException = false,
                            Value = "The id number it's rong"
                        };

                        return response;
                    }

                    if (areaManager.Password != password)
                    {
                        response = new ObjectResponse
                        {
                            IsPassed = false,
                            IsException = false,
                            Value = "The id number and password dos't match"
                        };

                        return response;
                    }

                    response = new ObjectResponse
                    {
                        IsPassed = true,
                        Value = areaManager
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

        public async Task<object> GetById(int areaManagerId)
        {
            ObjectResponse response;
            try
            {
                AreaManager areaManager = await Context.AreaManagers.FindAsync(areaManagerId);
                if (areaManager == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The id of employee it's not exist in the database"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = areaManager
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

        public async Task<object> GetByAreaId(int areaId)
        {
            ObjectResponse response;
            try
            {
                AreaManager areaManager = (await Context.AreaManagers.Where(
                    am => am.AreaId == areaId).ToListAsync()).LastOrDefault();
                if(areaManager == null || areaManager == default)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Please defind the manager off this area"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = areaManager
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

        public async Task<object> GetAllManagers()
        {
            ObjectResponse response;
            try
            {
                List<AreaManager> areaManagers = await Context.AreaManagers.ToListAsync();
                if(areaManagers.Count < 1 || areaManagers == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The list of area managers it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = areaManagers
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

        public async Task<object> Edit(AreaManager areaManagerToEdit)
        {
            ObjectResponse response;
            try
            {
                AreaManager areaManager = await Context.AreaManagers
                    .FindAsync(areaManagerToEdit.Id);

                if(areaManager == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of area manager"
                    };

                    return response;
                }

                areaManager.AreaId = areaManagerToEdit.AreaId;
                areaManager.FirstName = areaManagerToEdit.FirstName;
                areaManager.LastName = areaManagerToEdit.LastName;
                areaManager.IdNumber = areaManagerToEdit.IdNumber;
                areaManager.Password = areaManagerToEdit.Password;
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = areaManager
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

        public async Task<object> Delete(int areaManagerId)
        {
            ObjectResponse response;
            try
            {
                AreaManager areaManager = await Context.AreaManagers
                    .FindAsync(areaManagerId);
                if(areaManager == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of manager"
                    };

                    return response;
                }

                Context.AreaManagers.Remove(areaManager);
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = areaManager
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
    }
}
