namespace PocCqrs.Application.Products.Handlers
{
    using AutoMapper;
    using MassTransit;
    using MediatR;
    using PocCqrs.Aplication.MessageBroker.Model;
    using PocCqrs.Application.Products.Commands;
    using PocCqrs.Application.Models;
    using PocCqrs.Domain;
    using PocCqrs.Infrastructure.EFCore;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Linq;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDto>
    {
        private readonly EFDataContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _busService;

        public UpdateProductCommandHandler(EFDataContext context, IMapper mapper, IBus busService)
        {
            _context = context;
            _mapper = mapper;
            _busService = busService;
        }
        public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var productFound = _context.Products.SingleOrDefault(x => x.Id == request.Id);
            if (productFound != null)
            {
                productFound.Code = request.Product.Code;
                await _context.SaveChangesAsync();
            }         
            #region Call RmQ
            await _busService.Publish<ProductMessage>(request.Product.Code);
            #endregion
            return _mapper.Map<ProductDto>(request.Product);
        }
    }
}
