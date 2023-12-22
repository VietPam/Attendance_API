using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit;
using MimeKit;
namespace se100_cs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailController : ControllerBase
    {
        [HttpPost]
        [Route("reset_password")]
        public async Task<IActionResult> reset_password(long emp_id)
        {
            string new_pasword= await Program.api_employee.reset_password(emp_id);

            string email_Emp = Program.api_employee.get_email(emp_id);

            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Hệ thống E-Management", "my.damn.poor.soul@gmail.com"));
            email.To.Add(new MailboxAddress("Receiver Name", email_Emp));
            email.Subject = "Công Ty SE100 - Thầy Trọng";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = "Mật khẩu mới của bạn là " + new_pasword
            };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 587, false);

                // Note: only needed if the SMTP server requires authentication
                smtp.Authenticate("my.damn.poor.soul@gmail.com", "clwwgqbaiathlmxb");

                smtp.Send(email);
                smtp.Disconnect(true);
                return Ok();
            }
        }
    }
}
