

using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TetPee.Repository;
using TetPee.service.MailService;

namespace TetPee.service.Seller;

public class Service : IService
{
    private readonly MailService.IService _mailService;

    private readonly AppDbContext _dbContext;

    public Service(MailService.IService mailService, AppDbContext dbContext)
    {
        _mailService = mailService;
        _dbContext = dbContext;
    }

    public async Task<Base.Response.PageResult<Response.GetSellerResponse>> GetAllSeller(string? searchTerm, int pageSize, int pageIndex)
    {
        var query = _dbContext.Sellers.Where(x => true);
        
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.User.FirstName.Contains(searchTerm)
                || x.User.LastName.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.User.FirstName);

        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query
            .Select(x => new Response.GetSellerResponse()
            {
                Id = x.Id,
                Email = x.User.Email,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                ImageUrl = x.User.ImageUrl,
                Role = x.User.Role,
                CompanyName = x.CompanyName,
                TaxCode =  x.TaxCode

            });


        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new Base.Response.PageResult<Response.GetSellerResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        return result;
        /*var query = _dbContext.Users.Where(x => x.Role == "Seller");
        
        /*
        if (searchTerm != null)
        {
            query = query.Where(x =>
                x.FirstName.Contains(searchTerm)
                || x.LastName.Contains(searchTerm)
                || x.Email.Contains(searchTerm));
        }
        #1#

        query = query.OrderBy(x => x.Email);

        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);

        var selectedQuery = query
            .Select(x => new Response.GetSellerResponse()
            {
                Id = x.Id,
                Email = x.Email,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ImageUrl = x.ImageUrl,
                Role = x.Role,
                CompanyName = x.Seller!.CompanyName,
                TaxCode =  x.Seller.TaxCode

            });


        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();

        var result = new service.Base.Response.PageResult<Response.GetSellerResponse>()
        {
            Items = listResult,
            PageIndex = pageIndex,
            PageSize = pageSize,
            TotalItems = totalItems,
        };
        Console.WriteLine(result);
        return result;*/
    }

    public async Task<Response.GetSellerByIdResponse> GetSellerById(Guid id)
    {
        var query = _dbContext.Sellers.Where(x => x.Id == id);

        var selectedQuery = query
            .Select(x => new Response.GetSellerByIdResponse()
            {
                Id =  x.Id,
                Email = x.User.Email,
                FirstName = x.User.FirstName,
                LastName = x.User.LastName,
                ImageUrl = x.User.ImageUrl,
                PhoneNumber = x.User.PhoneNumber,
                Address = x.User.Address,
                CompanyName = x.CompanyName,
                TaxCode =  x.TaxCode,
                CompanyAddress =  x.CompanyAddress,
                

            });
        var result = await selectedQuery.FirstOrDefaultAsync();

        return result;    
    }


    public async Task<string> CreateSeller(Request.CreateSellerRequest request)
    {
        var existingUserQuery = _dbContext.Users.Where(x => x.Email == request.Email);
        
        bool isExistUser = await existingUserQuery.AnyAsync();
        
        if(isExistUser)
        {
            throw new Exception(Message.UserExistWithMail);
        }
        
        var user = new Repository.Entity.User()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            HashedPassword = request.HashedPassword,
            Role = "Seller"
        };

        _dbContext.Add(user);

        var result = await _dbContext.SaveChangesAsync();
        
        if (result > 0)
        {
            var seller = new Repository.Entity.Seller()
            {
                CompanyAddress = request.CompanyAddress,
                CompanyName = request.CompanyName,
                TaxCode = request.TaxCode,
                UserId = user.Id,
            };
            
            _dbContext.Add(seller);
            
            var sellerResult = await _dbContext.SaveChangesAsync();

            await _mailService.SendMail(new MailContext()
            {
                To = request.Email,
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
            if (sellerResult > 0) return "Add Seller successfully";
            
            return Message.FailToAddSeller;
        }
        
        return Message.FailToAddSeller;
    }

}