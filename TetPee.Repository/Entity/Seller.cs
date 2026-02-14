using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class Seller: BaseEntity<Guid>, IAuditableEntity
{   
    public string TaxCode { get; set; }
    public string CompanyName { get; set; }
    public string CompanyAddress { get; set; }
    
    //tạo FK
    public Guid UserId { get; set; } // FK
    public User User { get; set; }// nói cho ORM: mình muốn có mqh với User
    //thay vì kế thừa, thì chứa field User luôn, như nhau thoi
    
    public ICollection<Product>  Products { get; set; } = new List<Product>();        
            
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; } 
}