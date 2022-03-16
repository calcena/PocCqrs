

namespace PocCqrs.Application.Products.Commands
{
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    public class UpdateProductCommand : IRequest<ProductDto>
    {
        public int Id { get;  set; }
        public ProductDto Product { get;  set; }
    }
}
