using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CrudApi.Logics.Repositories;
using CrudApi.Models;
using FluentValidation;
using Mapster;
using MediatR;

namespace CrudApi.Logics.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, Result>
    {
        private readonly Lazy<IProductRepository> _repository;

        protected IProductRepository Repository => _repository.Value;

        public readonly Lazy<IValidator<Product>> _validator;
        protected IValidator<Product> Validator => _validator.Value;

        public CreateProductHandler(Lazy<IProductRepository> repository, Lazy<IValidator<Product>> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = request.Adapt<Product>();

            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            var validatorResult = Validator.Validate(product);

            if (validatorResult.IsValid == false)
            {
                return Result.Error<Product>(validatorResult.Errors);
            }

            Repository.Add(product);
            Repository.SaveChanges();

            return Result.Ok(product);
        }
    }
}
