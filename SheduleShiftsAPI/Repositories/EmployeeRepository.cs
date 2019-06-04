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
    public class EmployeeRepository
    {
        SheduleShiftsContext Context;
        public EmployeeRepository(SheduleShiftsContext context)
        {
            Context = context;
        }

        public async Task<object> Register(Employee newEmployee)
        {
            ObjectResponse response;
            try
            {
                Employee employee = (await Context.Employees.Where(
                    e => e.IdNumber == newEmployee.IdNumber).ToListAsync()).LastOrDefault();
                if (employee == null)
                {
                    AreaManager manager = Context.AreaManagers.Where(
                        am => am.IdNumber == newEmployee.IdNumber).ToList().LastOrDefault();
                    if (manager == null)
                    {
                        await Context.Employees.AddAsync(newEmployee);
                        await Context.SaveChangesAsync();

                        response = new ObjectResponse
                        {
                            IsPassed = true,
                            Value = newEmployee
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

        public async Task<object> Login(string idNumber, string password)
        {
            ObjectResponse response;
            try
            {
                Employee employee = (await Context.Employees.Where(
                    e => e.IdNumber == idNumber).ToListAsync()).LastOrDefault();
                if (employee == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The id number it's rong"
                    };

                    return response;
                }

                if (employee.Password != password)
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
                    Value = employee
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

        public async Task<object> GetById(int employeeId)
        {
            ObjectResponse response;
            try
            {
                Employee employee = await Context.Employees.FindAsync(employeeId);
                if (employee == null)
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
                    Value = employee
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

        public async Task<object> GetAllOfManagerById(int areaManagerId)
        {
            ObjectResponse response;
            try
            {
                AreaManager manager = await Context.AreaManagers.FindAsync(areaManagerId);
                if (manager == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The id of manager it's not exist in the database"
                    };

                    return response;
                }

                List<Employee> employees = Context.Employees.Where(
                    e => e.AreaManagerId == areaManagerId).ToList();

                if (employees.Count < 1)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The list employee of this manager area it's empty"
                    };

                    return response;
                }

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = employees
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

        public async Task<object> Edit(Employee employeeToEdit)
        {
            ObjectResponse response;
            try
            {
                Employee employee = await Context.Employees.FindAsync(employeeToEdit.Id);
                if (employee == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "Can't find the id of employee on the database"
                    };

                    return response;
                }

                Employee employeeCheckId = Context.Employees.Where(
                    e => e.IdNumber == employeeToEdit.IdNumber).ToList().LastOrDefault();
                if (employeeCheckId == null)
                {
                    AreaManager managerCheckId = Context.AreaManagers.Where(
                        m => m.IdNumber == employeeToEdit.IdNumber).ToList().LastOrDefault();
                    if (managerCheckId == null)
                    {
                        employee.FirstName = employeeToEdit.FirstName;
                        employee.LastName = employeeToEdit.LastName;
                        employee.AreaManagerId = employeeToEdit.AreaManagerId;
                        employee.Password = employeeToEdit.Password;
                        employee.IdNumber = employeeToEdit.IdNumber;
                        employee.EmployeeTypeId = employeeToEdit.EmployeeTypeId;
                        await Context.SaveChangesAsync();

                        response = new ObjectResponse
                        {
                            IsPassed = true,
                            Value = employee
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

        public async Task<object> Delete(int employeeId)
        {
            ObjectResponse response;
            try
            {
                Employee employee = await Context.Employees.FindAsync(employeeId);
                if (employee == null)
                {
                    response = new ObjectResponse
                    {
                        IsPassed = false,
                        IsException = false,
                        Value = "The id of employee it's not exist in the database"
                    };

                    return response;
                }

                Context.Employees.Remove(employee);
                await Context.SaveChangesAsync();

                response = new ObjectResponse
                {
                    IsPassed = true,
                    Value = employee
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
