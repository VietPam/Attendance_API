using Microsoft.EntityFrameworkCore;
using se100_cs.Model;
using System.Collections.Generic;

namespace se100_cs.APIs
{
    public class MyPayroll
    {
        public MyPayroll() { }
        public class Payroll_DTO
        {
            public string department_Code { get; set; } = "";
            public string emp_full_name { get; set; } = "";
            public string avatar { get; set; } = "";
            public bool gender { get; set; } = false;
            public string position { get; set; } = "";
            public long he_so_luong { get; set; } = 0;
            public int so_ngay_di_lam { get; set; } = 0;


            public long tam_tinh { get; set; } = 0;



            public long bonus_penalty { get; set; } = 0;
        }
        public List<Payroll_DTO> get_payroll_department(string department_code, int start_month, int start_day, int end_month,int end_day, int year)
        {
            List<Payroll_DTO> response = new List<Payroll_DTO>();
            using (DataContext context = new DataContext())
            {
                SqlDepartment? department = context.departments!.Where(s => s.code.CompareTo(department_code) == 0).Include(s => s.employees!).ThenInclude(s => s.position).AsNoTracking().FirstOrDefault();
                if (department == null) { return response; }

                List<SqlEmployee>? emps = department.employees!.Where(s => s.isDeleted == false).ToList();

                int salary_per_coef = context.settings.Select(s=>s.salary_per_coef).FirstOrDefault();
                foreach (SqlEmployee employee in emps)
                {
                    // làm sai rồi, tự nhiên tách cái detail ra làm chi
                    // giờ phải gộp vô lại
                    // tính số ngày đi làm
                    //int sndl = context.attendances!.Include(s=>s.list_attendance).Where(s=> s.year==year && 
                    //    (s.day>=start_day && s.month==start_month) || (s.day<=end_day && s.month==end_month)
                       
                    //    ).ToList().Count();


                    Payroll_DTO item = new Payroll_DTO();
                    item.avatar = employee.avatar;
                    item.emp_full_name = employee.fullName;
                    item.gender = employee.gender;
                    item.department_Code = department_code;
                    if (employee.position == null)
                    {
                        item.position = "Trống";
                        item.he_so_luong = -1;
                    }
                    else
                    {
                        item.position = employee.position.title!;
                        item.he_so_luong = employee.position.salary_coeffcient;
                    }

                    //tinh so ngay di lam
                    //tam tinh luong


                    response.Add(item);
                }
            }
            return response;
        }
    }
}
