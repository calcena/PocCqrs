namespace PocCqrs.Test
{
    using AutoMapper;
    using MediatR;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Logging.Abstractions;
    using Moq;
    using PocCqrs.Application.Models;
    using PocCqrs.Controllers;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductControllerTests //: IClassFixture<ProductController>
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;


        public ProductControllerTests(ILogger<ProductController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _mediator = mediator;
        }
        [Fact]
        public async Task InsertNewProduct()
        {
            // Arrange
            var _newProductDto = new ProductDto()
            {
                Code = "9999"
            };
            //Act
            var controller = new ProductController(new NullLogger<ProductController>(), new Mock<IMediator>().Object, new Mock<IMapper>().Object );
               var result = await controller.Create(_newProductDto);
            //Assert
         
        }
    }
}
