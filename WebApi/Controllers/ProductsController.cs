using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProductCommand createProductCommand)
        {
          CreatedProductDto createdProductDto =  await _mediator.Send(createProductCommand);
            return Created("",createdProductDto);
        }
    }
}
