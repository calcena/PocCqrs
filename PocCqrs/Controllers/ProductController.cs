using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PocCqrs.Application.Products.Commands;
using PocCqrs.Application.Models;
using PocCqrs.Application.Products.Queries;
using System;
using System.Threading.Tasks;
using PocCqrs.Api.Attributes;
using MassTransit;
using Newtonsoft.Json;

namespace PocCqrs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiKey]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IMediator _mediator;       
        private readonly IMapper _mapper;


        public ProductController(ILogger<ProductController> logger,IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _mediator.Send(new GetAllProductsQuery());
                _logger.LogInformation("ProductController",result.ToString());
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: { JsonConvert.SerializeObject(e.Message)}");
                return BadRequest(e);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _mediator.Send(new GetProductByIdQuery { Id = id });
                _logger.LogInformation($"ProductController: { JsonConvert.SerializeObject(result)}");
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: { JsonConvert.SerializeObject(e.Message)}");
                return BadRequest(e);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductDto product)
        {
            try
            {
                if ( product.Code != null) { 
                var command = new CreateProductCommand() { NewProduct = product };
                var result = await _mediator.Send(command);
                _logger.LogInformation($"ProductController: { JsonConvert.SerializeObject(result)}");
                return Ok("Product has ben created");
                }
                else
                {
                    _logger.LogWarning($" { JsonConvert.SerializeObject("Warning: Code is mandatory")}");
                    return BadRequest( "Code is mandatory");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error: { JsonConvert.SerializeObject(e.Message)}");
                return BadRequest(e);
            }

        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductDto product)
        {
            try
            {
                if (product != null)
                {
                    var command = new UpdateProductCommand() {Id = id, Product = product };
                var result = await _mediator.Send(command);
                return Ok("Product has ben updated");
                }
                else
                {
                    _logger.LogWarning($" { JsonConvert.SerializeObject("Warning: Code is mandatory")}");
                    return BadRequest("Data of product is mandatory");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var command = new DeleteProductCommand() { Id = id };
                var result = await _mediator.Send(command);
                if (command != null)
                {
                    return Ok("Product has ben deleted");
                }
                else
                {
                    _logger.LogWarning($" { JsonConvert.SerializeObject("Warning: Product not be remove")}");
                    return BadRequest("Product not be remove");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
