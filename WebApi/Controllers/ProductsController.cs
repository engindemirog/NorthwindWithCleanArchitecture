using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Dtos;
using Application.Features.Products.Models;
using Application.Features.Products.Queries;
using Core.Application.Requests;
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

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetProductListQuery getProductListQuery = new GetProductListQuery { PageRequest=pageRequest};
            ProductListModel result = await _mediator.Send(getProductListQuery);
            return Ok(result);
        }
    }
}


//Bir müşteriye ait siparişleri listeleyiniz.
//ContactName