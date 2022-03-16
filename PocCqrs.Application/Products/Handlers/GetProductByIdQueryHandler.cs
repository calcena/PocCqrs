namespace PocCqrs.Application.Products.Handlers
{
    using AutoMapper;
    using MediatR;
    using PocCqrs.Application.Models;
    using PocCqrs.Application.Products.Queries;
    using PocCqrs.Domain;
    using PocCqrs.Domain.Interfaces;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {

            return _mapper.Map<ProductDto>(await _productRepository.GetById(request.Id));
        }
    }
}
