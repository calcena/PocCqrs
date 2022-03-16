namespace PocCqrs.Application.Products.Handlers
{
    using AutoMapper;
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Application.Products.Queries;
    using PocCqrs.Domain;
    using PocCqrs.Domain.Interfaces;
    using PocCqrs.Infrastructure.EFCore;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper, EFDataContext context)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<ProductDto>>(await _productRepository.GetAll());
        }
    }
}
