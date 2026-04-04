using System;

namespace TetPee.service.Categogy;

public abstract class Response
{
    public class GetCategoriesResponse
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }
}