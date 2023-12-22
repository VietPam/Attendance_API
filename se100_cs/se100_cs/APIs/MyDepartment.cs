using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using se100_cs.Model;

namespace se100_cs.APIs
{
    public class MyDepartment
    {
        public MyDepartment() { }
        public class DTO_Department
        {
            public int current_page { get; set; }
            public int perpage { get; set; }
            public int pages { get; set; }
            public List<Department_DTO_Response> list_dep = new List<Department_DTO_Response>();
        }
        public class Department_DTO_Response
        {
            public long department_ID { get; set; }
            public string name { get; set; } = "";
            public string department_code { get; set; } = "";
            public string nameBoss { get; set; } = "";
            public int numberEmployee { get; set; } = 0;
            public int numberPosition { get; set; } = 0;
        }
        //public class Position_DTO
        //{
        //    public long posiition_ID { get; set; }
        //    public string title { get; set; } = "";
        //    public string position_code { get; set; } = "";
        //    public long salary_coeffcient { get; set; } = 1;
        //    public List<Employee_DTO>? employee_DTOs { get; set; }
        //    public int numberEmployee { get; set; } = 0;
        //}
        //public class Employee_DTO
        //{
        //    public long emp_ID { get; set; }
        //    public string avatar { get; set; } = "";
        //    public string fullName { get; set; } = "";
        //    public string email { get; set; } = "";
        //    public bool gender { get; set; } = true;
        //}

        public DTO_Department getAll(int current_page, int per_page)
        {
            DTO_Department response = new DTO_Department();
            response.current_page = current_page;
            response.perpage = per_page;
            List<Department_DTO_Response> list_dep = new List<Department_DTO_Response>();

            using (DataContext context = new DataContext())
            {
                int count_pages= context.departments.Include(s => s.employees).Where(s => s.isDeleted == false).Count();
                response.pages = (int)count_pages / per_page +1;


                List<SqlDepartment> list_departments = context.departments.Include(s => s.employees).Where(s => s.isDeleted == false).Include(s => s.position).ThenInclude(s => s.employees).Skip((current_page - 1) * per_page).Take(per_page).ToList();
                if (list_departments.Count > 0)
                {
                    foreach (SqlDepartment department in list_departments)
                    {
                        SqlPosition? head_position = department.position.Where(s => s.code == "HEAD").FirstOrDefault();


                        Department_DTO_Response item = new Department_DTO_Response();
                        item.department_ID = department.ID;
                        item.name = department.name;
                        item.department_code = department.code;
                        item.nameBoss = head_position.employees.Any() ? head_position.employees[0].fullName : "Trống";

                        item.numberEmployee = department.employees.Count();
                        item.numberPosition = department.position.Count();
                        list_dep.Add(item);
                    }
                }
                response.list_dep= list_dep;
                return response;
            }
        }
        public async Task<int> createNew(string name, string code)
        {
            using (DataContext context = new DataContext())
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                {
                    return 400;
                }
                SqlDepartment? existing_department = context.departments!.Where(s => s.code == code && s.isDeleted == false).FirstOrDefault();
                if (existing_department != null)
                {
                    return 409;
                }
                SqlDepartment department = new SqlDepartment();
                department.name = name;
                department.code = code;
                SqlPosition head = new SqlPosition();
                head.title = "Trưởng phòng";
                head.code = "HEAD";
                head.department = department;
                context.positions.Add(head);
                context.departments.Add(department);
                await context.SaveChangesAsync();
                return 200;
            }
        }
        public async Task<bool> updateOne(long id, string name, string code)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.ID == id && s.isDeleted == false).FirstOrDefault();
                if (department == null)
                {
                    return false;
                }
                else
                {
                    department.code = code;
                    department.name = name;
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> deleteOne(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.code == code && s.isDeleted == false).FirstOrDefault();
                if (department == null)
                {
                    return false;
                }
                else
                {
                    department.isDeleted = true;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
        }
    }
}
