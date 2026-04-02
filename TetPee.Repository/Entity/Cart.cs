using System;
using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class Cart: BaseEntity<Guid>, IAuditableEntity
{
    
    
    public Guid UserId { get; set; } 
    public User User { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; } 
    public DateTimeOffset? UpdatedAt { get; set; } 
}