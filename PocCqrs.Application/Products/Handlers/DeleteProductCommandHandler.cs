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
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ProductDto>
    {
        private readonly EFDataContext _context;
        private readonly IMapper _mapper;
        private readonly IBus _busService;

        public DeleteProductCommandHandler(EFDataContext context, IMapper mapper, IBus busService)
        {
            _context = context;
            _mapper = mapper;
            _busService = busService;
        }
        public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var productSearch = _context.Products.Find(request.Id);
            if (productSearch != null)
            {
                 _context.Products.Remove(productSearch);
                await _context.SaveChangesAsync();
                #region Call RmQ
                await _busService.Publish<ProductMessage>(productSearch.Code);
                #endregion
                return _mapper.Map<ProductDto>(request);
            }    
            else
            {
                return null;
            }
        }
    }
}
