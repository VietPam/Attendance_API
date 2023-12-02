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
            public long Id { get; set; }
            public string title { get; set; } = "";
            public string code { get; set; } = "";
            public long salary_coeffcient { get; set; } = 0;

            //public long quantity { get; set; }= 0;
        }
        public List<Position_DTO_Response> getByDepartmentCode(string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s=>s.code == departmentCode).FirstOrDefault();
                if(department == null)
                {
                    return new List<Position_DTO_Response>()  ;
                }
                List<SqlPosition>? list_positions = context.positions!.Include(s=>s.department).Where(s => s.isDeleted == false&& s.department!.code==departmentCode).ToList();
                List<Position_DTO_Response> repsonse = new List<Position_DTO_Response>();
                if (list_positions.Count > 0)
                {
                    foreach (SqlPosition position in list_positions)
                    {
                        Position_DTO_Response item = new Position_DTO_Response();
                        item.Id = position.ID;
                        item.title = position.title;
                        item.code = position.code;
                        item.salary_coeffcient = position.salary_coeffcient;
                        repsonse.Add(item);
                    }
                }
                return repsonse;
            }
        }
        public async Task<bool> createNew(string title, string code, long salary_coeffcient, string departmentCode)
        {
            using (DataContext context = new DataContext())
            {
                if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(code)||string.IsNullOrEmpty(departmentCode))
                {
                    return false;
                }
                SqlDepartment? department = context.departments!.Where(s => s.code == departmentCode).FirstOrDefault();
                if (department == null)
                {
                    return false;
                }
                
                SqlPosition item = new SqlPosition();
                item.title = title;
                item.code = code;
                item.salary_coeffcient=salary_coeffcient;
                item.department=department;
                context.positions!.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> updateOne(long position_id, string title, string code,long salarycoef)
        {
            if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(code) )
            {
                return false;
            }
            using (DataContext context = new DataContext())
            {
                SqlPosition? position = context.positions!.Where(s => s.ID == position_id&& s.isDeleted == false).FirstOrDefault();
                if (position == null)
                {
                    return false;
                }
                else
                {
                    position.code = code;
                    position.title = title;
                    position.salary_coeffcient = salarycoef;
                    await context.SaveChangesAsync();
                }
                return true;
            }
        }
        public async Task<bool> deleteOne(long id)
        {
            using (DataContext context = new DataContext())
            {
                SqlPosition? position = context.positions!.Where(s => s.ID == id && s.isDeleted == false).FirstOrDefault();
                if (position == null)
                {
                    return false;
                }
                else
                {
                    position.isDeleted = true;
                    await context.SaveChangesAsync();
                    return true;
                }
            }
        }
    }
}
