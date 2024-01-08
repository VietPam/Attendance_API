using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using System.Collections.Generic;
using static se100_cs.Controllers.Payroll.Response.PayrollDTO;

namespace se100_cs.APIs
{
    public class MyPayroll
    {
        public MyPayroll() { }

        public List<Payroll_DTO_Res> get_payroll_department(string department_code, string startDate , string endDate )
        {
            DateTime start = DateTime.Parse(startDate);
            DateTime end = DateTime.Parse(endDate);
            List<Payroll_DTO_Res> response = new List<Payroll_DTO_Res>();
            using (DataContext context = new DataContext())
            {
                List<SqlDepartment>? departments = new List<SqlDepartment>();

                if (department_code.CompareTo("all") == 0)
                {
                    departments = context.departments!
                        .Include(s => s.employees!)
                        .ThenInclude(s => s.position)
                        .Include(s => s.employees!)
                        .ThenInclude(s => s.attendances)
                        .AsNoTracking()
                        .ToList();
                }
                else
                {
                    departments[0] = context.departments!
                        .Include(s => s.employees!)
                        .ThenInclude(s => s.position!)
                        .Include(s => s.employees!)
                        .ThenInclude(s => s.attendances!)
                        .Where(s => s.code!.CompareTo(department_code) == 0)

                         .Select(s => new SqlDepartment
                         {
                             employees = s.employees!.Select(e => new SqlEmployee
                             {
                                 attendances = e.attendances!
                                                .Where(x => x.time!.CompareTo(start) > 0 && x.time!.CompareTo(start) < 0)
                                                .ToList()
                             }).ToList()
                         })
                        .AsNoTracking()
                        .FirstOrDefault();
                }
                if (!departments.Any())
                {
                    return response;
                }
                List<SqlEmployee>? emps = new List<SqlEmployee>();
                foreach(SqlDepartment dep in departments)
                {
                    foreach(SqlEmployee item in dep.employees.Where(s=>s.isDeleted==false))
                    {
                        emps.Add(item);
                    }
                }

                int salary_per_coef = context.settings.Select(s => s.salary_per_coef).FirstOrDefault();
                foreach (SqlEmployee employee in emps)
                {
                    Payroll_DTO_Res item = new Payroll_DTO_Res();
                    item.Employee_ID = employee.ID;
                    item.Avatar = employee.avatar;
                    item.Name = employee.fullName;
                    if (employee.position is null)
                    {
                        item.Position = "Trống";
                        item.Coefficient = 0;
                    }
                    else
                    {
                        item.Position = employee.position.title!;
                        item.Coefficient = employee.position.salary_coeffcient;
                    }

                    if (employee.attendances is null)
                    {
                        item.day_of_work = 0;
                    }
                    else
                    {
                        item.day_of_work = employee.attendances.Count();
                    }

                    item.salary = item.Coefficient * item.day_of_work * salary_per_coef;

                    response.Add(item);
                }
            }
            return response;
        }
    }
}
