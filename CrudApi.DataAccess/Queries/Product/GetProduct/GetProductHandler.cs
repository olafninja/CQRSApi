using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrudApi.DataAccess.ViewModels;
using CrudApi.Logics;
using CrudApi.Logics.Repositories;
using FluentValidation;
using Mapster;
using MediatR;

namespace CrudApi.DataAccess.Queries.Product.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductQuery, Result<ProductViewModel>>
    {
        private readonly Lazy<IProductRepository> _repository;

        protected IProductRepository Repository => _repository.Value;

        public readonly Lazy<IValidator<Models.Product>> _validator;
        protected IValidator<Models.Product> Validator => _validator.Value;

        public GetProductHandler(Lazy<IProductRepository> repository, Lazy<IValidator<Models.Product>> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<ProductViewModel>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var product = Repository.GetById(request.Id);

            if (product == null)
            {
                return Result.Error<ProductViewModel>("Product with that id does not exist");
            }

            return Result.Ok(product.Adapt<ProductViewModel>());
        }
    }
}
