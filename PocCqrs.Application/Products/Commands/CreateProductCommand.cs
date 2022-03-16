

namespace PocCqrs.Application.Products.Commands
{
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    public class CreateProductCommand : IRequest<ProductDto>
    {
        public ProductDto NewProduct { get; set; }
    }
}
