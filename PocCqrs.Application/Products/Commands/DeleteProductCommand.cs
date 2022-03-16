

namespace PocCqrs.Application.Products.Commands
{
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    public class DeleteProductCommand : IRequest<ProductDto>
    {
      public int Id { get; set; }
    }
}
