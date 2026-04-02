
using System;
using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class ProductCategory: BaseEntity<Guid>, IAuditableEntity
{
    public Guid CategoryId { get; set; } // FK
    public Category Category { get; set; }
    
    public Guid ProductID { get; set; } // FK
    public Product Product { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}