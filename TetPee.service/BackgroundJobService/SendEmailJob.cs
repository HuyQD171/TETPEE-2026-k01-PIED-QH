/*using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Quartz;
using TetPee.Repository;
using TetPee.service.MailService;


namespace TetPee.service.BackgroundJobService;

[DisallowConcurrentExecution]
public class SendEmailJob : IJob
{
    private readonly IService _mailService;
    private readonly AppDbContext _dbContext;

    public SendEmailJob(IService mailService, AppDbContext dbContext)
    {
        _mailService = mailService;
        _dbContext = dbContext;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var query = _dbContext.Users.Where(u => true);
        
        var listUser = await query.ToListAsync();

        foreach (var user in listUser)
        {
            await _mailService.SendMail(new MailContext()
            {
                To = user.Email,
                Subject = "✨ Welcome to TetPee",
                Body = @"
            <!DOCTYPE html>
            <html>
            <head>
            <meta charset='UTF-8'>
            <meta name='viewport' content='width=device-width, initial-scale=1.0'>
            <title>Welcome</title>
            </head>

            <body style='margin:0; padding:0; background:#0f172a; font-family:Segoe UI, Arial, sans-serif;'>

            <table width='100%' cellpadding='0' cellspacing='0' style='padding:40px 0;'>
            <tr>
            <td align='center'>

            <table width='600' cellpadding='0' cellspacing='0' style='background:#ffffff; border-radius:16px; overflow:hidden; box-shadow:0 10px 30px rgba(0,0,0,0.3);'>

            <!-- Header -->
            <tr>
            <td style='background:linear-gradient(135deg,#4f46e5,#06b6d4); padding:40px; text-align:center; color:white;'>
                <h1 style='margin:0; font-size:32px;'>TetPee 🚀</h1>
                <p style='margin-top:10px; font-size:16px; opacity:0.9;'>Welcome to your new experience</p>
            </td>
            </tr>

            <!-- Body -->
            <tr>
            <td style='padding:40px; color:#1e293b;'>

                <h2 style='margin-top:0;'>Hi there 👋</h2>

                <p style='font-size:16px; line-height:1.6;'>
                    We're super excited to have you join <strong>TetPee</strong>.
                </p>

                <p style='font-size:16px; line-height:1.6;'>
                    Your account is ready. You can now explore everything and enjoy a smooth experience with us.
                </p>

                <!-- Card -->
                <div style='background:#f1f5f9; padding:20px; border-radius:12px; margin:25px 0;'>
                    <p style='margin:0; font-size:14px; color:#475569;'>
                        💡 Tip: Start by exploring your dashboard and customizing your profile.
                    </p>
                </div>

                <!-- Button -->
                <div style='text-align:center; margin:35px 0;'>
                    <a href='#'
                       style='background:linear-gradient(135deg,#4f46e5,#06b6d4);
                              color:white;
                              padding:14px 30px;
                              text-decoration:none;
                              border-radius:999px;
                              font-weight:bold;
                              font-size:16px;
                              display:inline-block;
                              box-shadow:0 5px 15px rgba(79,70,229,0.4);'>
                        Get Started ✨
                    </a>
                </div>

                <p style='font-size:14px; color:#64748b;'>
                    If you have any questions, just reply to this email — we’re here for you.
                </p>

                <p style='margin-top:30px; font-size:14px;'>
                    Cheers,<br/>
                    <strong>TetPee Team</strong>
                </p>

            </td>
            </tr>

            <!-- Footer -->
            <tr>
            <td style='background:#0f172a; color:#94a3b8; text-align:center; padding:20px; font-size:12px;'>
                © 2026 TetPee. All rights reserved.
            </td>
            </tr>

            </table>

            </td>
            </tr>
            </table>

            </body>
            </html>
            "
            });
        }
    }
}*/