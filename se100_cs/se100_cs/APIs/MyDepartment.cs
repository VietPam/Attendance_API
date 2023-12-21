using MessagePack.Formatters;
using Microsoft.EntityFrameworkCore;
using se100_cs.Model;

namespace se100_cs.APIs
{
    public class MyDepartment
    {
        public MyDepartment() { }

        public class Department_DTO_Response
        {
            public long department_ID { get; set; }
            public string name { get; set; } = "";
            public string department_code { get; set; } = "";
            //public long idBoss { get; set; } = -1;
            public string nameBoss { get; set; } = "";
            public int numberEmployee { get; set; } = 0;
            public List<Position_DTO> position_DTOs { get; set; }
        }
        public class Position_DTO
        {
            public long posiition_ID { get; set; }
            public string title { get; set; } = "";
            public string position_code { get; set; } = "";
            public long salary_coeffcient { get; set; } = 1;
            public List<Employee_DTO>? employee_DTOs { get; set; }
            public int numberEmployee { get; set; } = 0;
        }
        public class Employee_DTO
        {
            public long emp_ID { get; set; }
            public string avatar { get; set; } = "";
            public string fullName { get; set; } = "";
            public string email { get; set; } = "";
            public bool gender { get; set; } = true;
        }
        public List<Department_DTO_Response> getAll()
        {
            using (DataContext context = new DataContext())
            {
                List<SqlDepartment> list_departments = context.departments.Where(s => s.isDeleted == false).Include(s=>s.position).ThenInclude(s=>s.employees).ToList();
                List<Department_DTO_Response> repsonse = new List<Department_DTO_Response>();
                if (list_departments.Count > 0)
                {
                    foreach (SqlDepartment department in list_departments)
                    {
                        SqlPosition? position = department.position.Where(s => s.code == "HEAD").FirstOrDefault();
                        SqlEmployee? head = context.employees.Where(s=>s.position==position).FirstOrDefault();
                        Department_DTO_Response item = new Department_DTO_Response();
                        item.department_ID = department.ID;
                        item.name = department.name;
                        item.department_code = department.code;
                        if (head != null)
                        {
                            //item.idBoss = head.ID;
                            item.nameBoss = head.fullName;
                        }
                        item.numberEmployee = context.employees!.Include(s => s.department).Where(s => s.isDeleted == false && s.department == department).Count();

                        List<Position_DTO> posion_DTOs = new List<Position_DTO>();
                        foreach(SqlPosition item_position in department.position)
                        {
                            Position_DTO tmp = new Position_DTO();
                            tmp.posiition_ID = item_position.ID;
                            tmp.position_code = item_position.code;
                            tmp.title = item_position.title;
                            tmp.salary_coeffcient = item_position.salary_coeffcient;
                            tmp.numberEmployee = item_position.employees.Count();

                            List<Employee_DTO> employee_DTOs = new List<Employee_DTO>();
                            foreach(SqlEmployee item_employee in item_position.employees)
                            {
                                Employee_DTO item_tmp = new Employee_DTO();
                                item_tmp.emp_ID = item_employee.ID;
                                item_tmp.avatar = item_employee.avatar;
                                item_tmp.fullName = item_employee.fullName;
                                item_tmp.email = item_employee.email;
                                item_tmp.gender=item_employee.gender;
                                employee_DTOs.Add(item_tmp);
                            }
                            tmp.employee_DTOs= employee_DTOs;
                            posion_DTOs.Add(tmp);
                        }
                        item.position_DTOs = posion_DTOs;
                        repsonse.Add(item);
                    }
                }
                return repsonse;
            }
        }
        public async Task<int> createNew(string name, string code)
        {
            using (DataContext context = new DataContext())
            {
                if(string.IsNullOrEmpty(name)|| string.IsNullOrEmpty(code))
                {
                    return 400;
                }                
                SqlDepartment? existing_department = context.departments!.Where(s=>s.code == code&& s.isDeleted==false).FirstOrDefault();
                if(existing_department != null)
                {
                    return 409;
                }
                SqlDepartment department= new SqlDepartment();
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
            if(string.IsNullOrEmpty (name)|| string.IsNullOrEmpty(code)) 
            {
                return false;
            }
            using(DataContext context = new DataContext()) { 
                SqlDepartment? department= context.departments!.Where(s=>s.ID==id && s.isDeleted==false).FirstOrDefault();
                if(department==null)
                {
                    return false;
                }
                else
                {
                    department.code=code;
                    department.name=name;
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
            using(DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.code== code&& s.isDeleted == false).FirstOrDefault();
                if( department==null)
                {
                    return false;
                }
                else
                {
                    department.isDeleted=true;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
        }
    }
}
