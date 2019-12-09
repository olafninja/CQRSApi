using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CrudApi.Logics.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}
