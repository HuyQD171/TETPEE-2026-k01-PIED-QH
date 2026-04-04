using System;
using System.Collections.Generic;
using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    public required string Email { get; set; }
    public string FirstName  { get; set; }
    public string LastName { get; set; }
    public string? ImageUrl { get; set; } = null;
    public string? PhoneNumber { get; set; }
    public required string HashedPassword { get; set; }
    public string? Address { get; set; }
    public string Role { get; set; } = "user";// User, Seller, Admin
    public bool IsVerify { get; set; } = false; // khi user register thì phải verify email hợp lệ
    public int VerifyCode  { get; set; } // Mã verify gửi về email
    //(down)thuật ngữ con bot: trong 5p hay 10p thằng nào chưa verify thì con bot sẽ dọn dẹp
    // 👉 Đây là background job
    // Hangfire 
    // Quartz
    // Cron job
    
    public Seller? Seller { get; set; }
    public ICollection<Order>  Orders { get; set; } = new List<Order>();
    //mối qh một nhiều cho ORM nó generate xuống    db
    
    public Cart? Cart { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }//nullable vì chưa update lần nào
    // tách ra những field trong interface để tái sử dụng, 
    // còn kế thừa thì bắt buộc phải có các field đó
    // BaseEntity:
    // Dùng cho Id + Soft Delete
    // Entity nào cũng cần
    // Interface:
    // Entity nào cần audit thì implement
    // Không bắt buộc tất cả
    // Soft Delete // Tránh xung đột khóa ngoại (FK Constraint)
}