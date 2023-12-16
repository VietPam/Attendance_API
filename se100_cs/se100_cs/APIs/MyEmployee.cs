using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using System.Collections.Generic;
using static se100_cs.APIs.MyPosition;
using static se100_cs.Controllers.EmployeeController;

namespace se100_cs.APIs
{
    public class MyEmployee
    {
        public MyEmployee() { }
        public class Employee_DTO_Response
        {
            public long ID { get; set; }
            public string email { get; set; } = "";
            public string fullName { get; set; } = "";
            public string phoneNumber { get; set; } = "";
            public string avatar { get; set; } = "";
            public DateTime birth_day { get; set; } = DateTime.UtcNow;
            public bool gender { get; set; } = true;
            public string cmnd { get; set; } = "";
            public string address { get; set; } = "";
        }
        public List<Employee_DTO_Response> getByDepartmentCode(string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.code == departmentCode && s.isDeleted == false).Include(s => s.employees).FirstOrDefault();
                if (department == null)
                {
                    return new List<Employee_DTO_Response>();
                }
                List<SqlEmployee>? list = department.employees;
                List<Employee_DTO_Response> repsonse = new List<Employee_DTO_Response>();
                if (list.Count > 0)
                {
                    foreach (SqlEmployee employee in list)
                    {
                        Employee_DTO_Response item = new Employee_DTO_Response();
                        item.ID = employee.ID;
                        item.email = employee.email;
                        item.fullName = employee.fullName;
                        item.phoneNumber = employee.phoneNumber;
                        item.avatar = employee.avatar;
                        item.gender = employee.gender;
                        item.cmnd = employee.cmnd;
                        item.address = employee.address;
                        repsonse.Add(item);
                    }
                }
                return repsonse;
            }
        }

        public string getRole(long employee_id)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee employee = context.employees!.Where(s => s.ID == employee_id && s.isDeleted == false).FirstOrDefault();
                if (employee == null)
                {
                    return "NotFound";
                }
                return employee.role.ToString();
            }
        }

        public async Task<bool> createNew(string email, string fullName, string phoneNumber, DateTime birth_day, bool gender, string cmnd, string address, string avatar, string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(departmentCode) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(avatar) || string.IsNullOrEmpty(cmnd))
                {
                    return false;
                }
                SqlDepartment? department = context.departments!.Where(s => s.code == departmentCode).FirstOrDefault();
                if (department == null)
                {
                    return false;
                }

                SqlEmployee? existing = context.employees!.Where(s => s.email == email).FirstOrDefault();
                if (existing != null)
                {
                    return false;
                }
                SqlEmployee item = new SqlEmployee();
                item.email = email;
                item.password = DataContext.randomString(6);
                item.fullName = fullName;
                item.phoneNumber = phoneNumber;
                item.birth_day = birth_day;
                item.gender = gender;
                item.cmnd = cmnd;
                item.token = DataContext.randomString(8);
                item.avatar = avatar;
                item.address = address;
                item.department = department;
                context.employees!.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> link_to_position(long user_id, long position_id)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee emp = context.employees.Where(s => s.ID == user_id).Include(s => s.position).FirstOrDefault();
                if (emp == null)
                {
                    return false;
                }
                else
                {
                    if (emp.position == null)
                    {
                        SqlPosition position = context.positions.Where(s => s.ID == position_id).FirstOrDefault();
                        if (position == null)
                        {
                            return false;
                        }
                        else
                        {
                            emp.position = position;
                            await context.SaveChangesAsync();
                            return true;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
        public List<Non_Position_Emp> list_non_position()
        {
            List<Non_Position_Emp> response = new List<Non_Position_Emp>();
            using (DataContext context = new DataContext())
            {
                List<SqlEmployee>? list_emp = context.employees.Where(s => s.isDeleted == false && s.position == null).Include(s => s.department).ToList();
                if (list_emp != null && list_emp.Any())
                {
                    foreach (SqlEmployee emp in list_emp)
                    {
                        Non_Position_Emp item = new Non_Position_Emp();
                        item.user_ID = emp.ID;
                        item.fullName = emp.fullName;
                        item.avatar = emp.avatar;
                        item.gender = emp.gender;
                        item.department_code = emp.department.code;
                        response.Add(item);
                    }
                }
                return response;
            }
        }

        public async Task<bool> updateOne(string email, string fullName, string phoneNumber, DateTime birth_day, bool gender, string cmnd, string address, string avatar, long id)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(avatar) || string.IsNullOrEmpty(cmnd))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees!.Where(s => s.ID == id && s.isDeleted == false).FirstOrDefault();
                if (employee == null)
                {
                    return false;
                }
                else
                {
                    employee.email = email;
                    employee.password = DataContext.randomString(6);
                    employee.fullName = fullName;
                    employee.phoneNumber = phoneNumber;
                    employee.birth_day = birth_day;
                    employee.gender = gender;
                    employee.cmnd = cmnd;
                    employee.avatar = avatar;
                    employee.address = address;
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> updateRole(string role, long id)
        {
            if (string.IsNullOrEmpty(role))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees!.Where(s => s.ID == id && s.isDeleted == false).FirstOrDefault();
                if (employee == null)
                {
                    return false;
                }
                else
                {
                    if (role == "admin")
                    {
                        employee.role = Role.ADMIN;
                    }
                    else if (role == "director") { employee.role = Role.DIRECTOR; }
                    else if (role == "manager") { employee.role = Role.MANAGER; }
                    else { employee.role = Role.EMPLOYEE; }
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> deleteOne(long id)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees!.Where(s => s.ID == id && s.isDeleted == false).FirstOrDefault();
                if (employee == null)
                {
                    return false;
                }
                else
                {
                    employee.isDeleted = true;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
        }
        public class _Token
        {
            public string token { get; set; }
        }
        public _Token login(string email, string password)
        {
            _Token response = new _Token();
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return response;
            }
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees!.Where(s => s.email == email).FirstOrDefault();
                if (employee == null)
                {
                    return response;
                }
                else
                {
                    if (employee.password != password)
                    {
                        return response;
                    }
                    else
                    {
                        response.token = employee.token;
                        return response;
                    }
                }
            }
        }

        public long checkEmployee(string token)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee? employee = context.employees.Where(s => s.token == token && s.isDeleted == false).FirstOrDefault();
                if (employee == null)
                {
                    return -1;
                }
                else
                {
                    return employee.ID;
                }
            }
        }

        public int countTotalEmployee()
        {
            int count = 0;
            using (DataContext context = new DataContext())
            {
                count = context.employees!.Where(s => s.isDeleted == false).Count();
            }
            return count;
        }
        public int countMaleEmployee()
        {
            int count = 0;
            using (DataContext context = new DataContext())
            {
                count = context.employees!.Where(s => s.isDeleted == false && s.gender == true).Count();
            }
            return count;

        }
        public class Total_Employee
        {
            public int man { get; set; } = 0;
            public int woman { get; set; } = 0;
            public int total { get; set; } = 0;
        }
        public Total_Employee getTotal_Employee()
        {
            int total = countTotalEmployee();
            int male = countMaleEmployee();
            Total_Employee today = new Total_Employee();
            today.man = male;
            today.total = total;
            today.woman = total - male;
            return today;
        }
        public async Task<string> reset_password(long emp_id)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee emp = context.employees.Where(s => s.ID == emp_id).FirstOrDefault();
                if (emp == null)
                {
                    return "sai emp_id";
                }
                emp.password = DataContext.randomString(6);
                await context.SaveChangesAsync();
                return emp.password;
            }
        }
        public string get_email(long emp_id)
        {
            using (DataContext context = new DataContext())
            {
                SqlEmployee emp = context.employees.Where(s => s.ID == emp_id).FirstOrDefault();
                if (emp == null)
                {
                    return "sai emp_id";
                }
                return emp.email;
            }
        }
    }
}
