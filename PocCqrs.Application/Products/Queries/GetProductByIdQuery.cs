namespace PocCqrs.Application.Products.Queries
{
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    using System.Collections.Generic;
    public class GetProductByIdQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }

    }
}
