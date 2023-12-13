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
            public long ID { get; set; }
            public string name { get; set; } = "";
            public string code { get; set; } = "";
            public long idBoss { get; set; } = -1;
            public string nameBoss { get; set; } = "";
            public int numberEmployee { get; set; } = 0;
        }
        //public  List<Department_DTO_Response> getAll()
        //{
        //    using(DataContext context = new DataContext())
        //    {
        //        List<SqlDepartment> list_departments = context.departments.Where(s=>s.isDeleted==false).ToList();
        //        List<Department_DTO_Response> repsonse = new List<Department_DTO_Response>();
        //        if(list_departments.Count > 0)
        //        {
        //            foreach( SqlDepartment department in list_departments)
        //            {
        //                SqlEmployee? head = context.employees!.Include(s=>s.department).Where(s=>s.isDeleted == false && s.department==department).FirstOrDefault();
        //                Department_DTO_Response item = new Department_DTO_Response();
        //                item.ID = department.ID;
        //                item.name = department.name;
        //                item.code = department.code;
        //                if(head != null)
        //                {
        //                    item.idBoss = head.ID;
        //                    item.nameBoss = head.fullName;
        //                }
        //                int count_employee = context.employees!.Include(s => s.department).Where(s => s.isDeleted == false && s.department == department).Count();
        //                item.numberEmployee = count_employee; 
        //                //SqlEmployee
        //                repsonse.Add(item);
        //            }
        //        }
        //        return repsonse;
        //    }
        //}
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
