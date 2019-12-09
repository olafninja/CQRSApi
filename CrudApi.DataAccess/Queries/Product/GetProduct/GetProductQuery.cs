using System;
using System.Collections.Generic;
using System.Text;
using CrudApi.DataAccess.ViewModels;
using CrudApi.Logics;
using MediatR;

namespace CrudApi.DataAccess.Queries.Product.GetProduct
{
    public class GetProductQuery : IRequest<Result<ProductViewModel>>
    {
        public int Id { get; set; }
    }
}
