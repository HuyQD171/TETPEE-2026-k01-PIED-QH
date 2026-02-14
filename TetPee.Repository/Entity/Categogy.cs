

using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class Category: BaseEntity<Guid>, IAuditableEntity
{
    public required string Name  { get; set; }
    
    public Guid? ParentId { get; set; } //FK, trỏ tới CategoryId
    //Category này là con của Category nào?
    public Category? Parent { get; set; } 
    //ORM: hiểu được cate có mối quan hệ với cate
    //Nếu là null, thì nó là thằng cha cao nhất
    //Nếu có giá trị thì nó là thằng con của ParentId
    
    public ICollection<Category>  Children { get; set; } = new List<Category>();
    //những category có parentId
    public ICollection<ProductCategory>  ProductCategory { get; set; } = new List<ProductCategory>();
    
    //Ý nghĩa của đệ quy
    //Category chứa children
    //Children lại chứa Category
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}