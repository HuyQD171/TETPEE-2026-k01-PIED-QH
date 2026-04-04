using System;
using System.Collections.Generic;

namespace TetPee.service.Product;

public class Request
{
    public class CreateProductRequest
    {
            public required string Name { get; set; }
            public required string Description { get; set; }
            public required decimal Price { get; set; }
            public List<Guid>? CategoryIds { get; set; }
    }
}