using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using static se100_cs.APIs.MyPosition;

namespace se100_cs.APIs
{
    public class MyEmployee
    {
        public MyEmployee() { }
        public class Employee_DTO_Response
        {
            public string email { get; set; } = "";
        }
        public List<Employee_DTO_Response> getByDepartmentCode(string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.code == departmentCode && s.isDeleted==false).FirstOrDefault();
                if (department == null)
                {
                    return new List<Employee_DTO_Response>();
                }
                List<SqlEmployee>? list = context.employees!.Include(s => s.department).Where(s => s.isDeleted == false && s.department!.code == departmentCode ).ToList();
                List<Employee_DTO_Response> repsonse = new List<Employee_DTO_Response>();
                if (list.Count > 0)
                {
                    foreach (SqlEmployee position in list)
                    {
                        Employee_DTO_Response item = new Employee_DTO_Response();
                        item.email = position.email;
                        repsonse.Add(item);
                    }
                }
                return repsonse;
            }
        }

        public async Task<bool> createNew(string email,  string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                if (string.IsNullOrEmpty(email)  || string.IsNullOrEmpty(departmentCode))
                {
                    return false;
                }
                SqlDepartment? department = context.departments!.Where(s => s.code == departmentCode).FirstOrDefault();
                if (department == null)
                {
                    return false;
                }
                SqlEmployee? existing = context.employees!.Where(s=>s.email == email).FirstOrDefault();
                if (existing != null)
                {
                    return false;
                }
                SqlEmployee item = new SqlEmployee();
                item.email = email;
                item.department = department;
                context.employees!.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}
