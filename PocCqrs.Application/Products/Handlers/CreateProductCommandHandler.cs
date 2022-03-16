namespace PocCqrs.Application.Products.Handlers
{
    using AutoMapper;
    using MassTransit;
    using MediatR;
    using PocCqrs.Aplication.MessageBroker.Model;
    using PocCqrs.Application.Models;
    using PocCqrs.Application.Products.Commands;
    using PocCqrs.Domain;
    using PocCqrs.Infrastructure.EFCore;
    using System.Threading;
    using System.Threading.Tasks;

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDto>
    {
        private readonly EFDataContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _busService;

        public CreateProductCommandHandler(EFDataContext context, IMapper mapper, IBus busService)
        {
            _context = context;
            _mapper = mapper;
            _busService = busService;
        }
        public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var newProduct = _context.Products.Add(_mapper.Map<Product>(request.NewProduct));
            await _context.SaveChangesAsync();
            #region Call RmQ
            await _busService.Publish<ProductMessage>(newProduct);
            #endregion
            return _mapper.Map<ProductDto>(request.NewProduct);
        }
    }
}
