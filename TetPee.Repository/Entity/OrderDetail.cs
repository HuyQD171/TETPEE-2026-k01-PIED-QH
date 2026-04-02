
using System;
using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class OrderDetail:  BaseEntity<Guid>, IAuditableEntity
{
    public Guid OrderID { get; set; } // FK
    public Order Order { get; set; }
    
    public Guid ProductID { get; set; } // FK
    public Product Product { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }//giá tiền mua tại thời điểm đó
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}