using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using se100_cs.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace se100_cs.APIs
{
    public class MyAttendance
    {
        public MyAttendance() { }
        

        //public async Task<List<Employees_Today>> getEmployees_ByWeek()
        //{
        //    List<Employees_Today> response = new List<Employees_Today>();

        //    int ngay_hom_nay = DateTime.Now.Day;
        //    int thang = DateTime.Now.Month;
        //    int thang_truoc = DateTime.Now.Month - 1;
        //    int hom_nay_la_thu = (int)DateTime.Now.DayOfWeek + 1; // vi du thu 5, thứ 2 là bằng 2
        //    if (hom_nay_la_thu == 1) { hom_nay_la_thu = 8; }
        //    for (int i = hom_nay_la_thu; i >= 2; i--)
        //    {
        //        Employees_Today today = new Employees_Today();
        //        using (DataContext context = new DataContext())
        //        {
        //            if (ngay_hom_nay - (hom_nay_la_thu - i) < 1)
        //            {
        //                if (thang_truoc % 2 == 1)
        //                {
        //                    ngay_hom_nay = 30;
        //                }
        //                else
        //                {
        //                    ngay_hom_nay = 31;
        //                }
        //                hom_nay_la_thu = i;
        //                thang -= 1;
        //            }
        //            SqlAttendance? atd_today = context.attendances.Include(s => s.list_attendance).Where(s => s.day == ngay_hom_nay - (hom_nay_la_thu - i) && s.month == thang && s.year == DateTime.Now.Year).FirstOrDefault();
        //            if (atd_today == null || !atd_today.list_attendance.Any())
        //            {
        //                await init_attendance_today_async(ngay_hom_nay - (hom_nay_la_thu - i), thang, DateTime.Now.Year);
        //            }
        //            atd_today = context.attendances.Include(s => s.list_attendance).Where(s => s.day == ngay_hom_nay - (hom_nay_la_thu - i) && s.month == thang && s.year == DateTime.Now.Year).FirstOrDefault();
        //            int on_time = atd_today.list_attendance.Where(s => s.status == 0).Count();
        //            int late_coming = atd_today.list_attendance.Where(s => s.status == 1).Count();
        //            int absent = atd_today.list_attendance.Where(s => s.status == 2).Count();

        //            today.thu = i;
        //            today.ngay = ngay_hom_nay - (hom_nay_la_thu - i);
        //            today.on_time = on_time;
        //            today.absent = absent;
        //            today.late_coming = late_coming;
        //            response.Add(today);
        //        }
        //    }
        //    response = response.OrderBy(s => s.thu).ToList();
        //    return response;
        //}

        //public bool getListNotiSignalR(string idHub)
        //{
        //    DateTime now = DateTime.UtcNow;
        //    using (DataContext context = new DataContext())
        //    {
        //        SqlEmployee? user = context.employees.Where(s => s.IdHub.CompareTo(idHub) == 0).FirstOrDefault();
        //        if (user == null)
        //        {
        //            return false;
        //        }
        //        List<Attendance_DTO_Response>? list = Program.api_attendance.getListByDate(now.Day, now.Month, now.Year).Take(5).ToList();
        //        try
        //        {
        //            string data = JsonConvert.SerializeObject(list);
        //            Program.notiHub?.Clients.Client(user.IdHub).SendCoreAsync("GetListNoti", new object[] { data });
        //        }
        //        catch (Exception ex)
        //        {
        //            Log.Error(ex.Message);
        //            return false;
        //        }
        //    }
            
        //    return false;
        //}
    }
}