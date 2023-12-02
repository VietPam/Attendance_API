using MessagePack.Formatters;
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
        }
        public  List<Department_DTO_Response> getAll()
        {
            using(DataContext context = new DataContext())
            {
                List<SqlDepartment> list_departments = context.departments.ToList();
                List<Department_DTO_Response> repsonse = new List<Department_DTO_Response>();
                if(list_departments.Count > 0)
                {
                    foreach( SqlDepartment department in list_departments)
                    {
                        Department_DTO_Response item = new Department_DTO_Response();
                        item.ID = department.ID;
                        item.name = department.name;
                        item.code = department.code;
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
                if(string.IsNullOrEmpty(name)|| string.IsNullOrEmpty(code))
                {
                    return false;
                }                
                SqlDepartment department= new SqlDepartment();
                department.name = name;
                department.code = code;
                context.departments.Add(department);
                await context.SaveChangesAsync();
                return true;
            }
        }
        public async Task<bool> updateOne(long id, string name, string code) 
        {
            if(string.IsNullOrEmpty (name)|| string.IsNullOrEmpty(code)) 
            {
                return false;
            }
            using(DataContext context = new DataContext()) { 
                SqlDepartment department= context.departments!.Where(s=>s.ID==id).FirstOrDefault();
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
    }
}
