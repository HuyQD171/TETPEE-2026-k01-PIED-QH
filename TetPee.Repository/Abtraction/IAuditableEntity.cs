using System;

namespace TetPee.Repository.Abtraction;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedAt { get; set; }//dòng dữ liêu janyf dc tạo ra khi nào
    public DateTimeOffset? UpdatedAt { get; set; }//dong ữ lệu này dc cập nhật lần cối khi nào

}
