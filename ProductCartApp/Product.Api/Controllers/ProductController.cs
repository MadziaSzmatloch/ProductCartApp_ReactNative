using MediatR;
using Microsoft.AspNetCore.Mvc;
using Product.Application.GetProducts;
using Product.Application.ReserveProduct;
using Product.Application.RestoreProduct;

namespace Product.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var prod = await _mediator.Send(new GetProductsRequest());
            return Ok(prod);
        }

        [HttpPatch("reserve")]
        public async Task<IActionResult> ReserveProducts(ReserveProductRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpPatch("restore")]
        public async Task<IActionResult> RestoreProducts(RestoreProductRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }
    }
}
