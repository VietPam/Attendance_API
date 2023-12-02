using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using System.ComponentModel.DataAnnotations.Schema;
using static se100_cs.APIs.MyDepartment;

namespace se100_cs.APIs
{
    public class MyPosition
    {
        public MyPosition() { }
        public class Position_DTO_Response
        {
            public string title { get; set; } = "";
            public string code { get; set; } = "";
            public long salary_coeffcient { get; set; } = 0;
        }
        public List<Position_DTO_Response> getByDepartmentCode(string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                List<SqlPosition>? list_positions = context.positions!.Include(s=>s.department).Where(s => s.isDeleted == false&& s.department!.code==departmentCode).ToList();
                List<Position_DTO_Response> repsonse = new List<Position_DTO_Response>();
                if (list_positions.Count > 0)
                {
                    foreach (SqlPosition position in list_positions)
                    {
                        Position_DTO_Response item = new Position_DTO_Response();
                        item.title = position.title;
                        item.code = position.code;
                        item.salary_coeffcient = position.salary_coeffcient;
                        repsonse.Add(item);
                    }
                }
                return repsonse;
            }
        }
        public async Task<bool> createNew(string name, string code)
        {
            using (DataContext context = new DataContext())
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(code))
                {
                    return false;
                }
                SqlDepartment department = new SqlDepartment();
                department.ID = DateTime.Now.Ticks;
                department.name = name;
                department.code = code;
                context.departments.Add(department);
                await context.SaveChangesAsync();
                return true;
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
