using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class CartDetail: BaseEntity<Guid>, IAuditableEntity
{
    public Guid CartId { get; set; } // FK
    public Cart Cart { get; set; }
    
    public Guid ProductID { get; set; } // FK
    public Product Product { get; set; }
    public int Quantity { get; set; }

    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}