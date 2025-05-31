using Cart.Application.Commands.AddProductToCart;
using Cart.Application.Commands.CreateCart;
using Cart.Application.Commands.FinalizeCart;
using Cart.Application.Commands.RemoveProductFromCart;
using Cart.Application.Queries;
using Cart.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cart.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("{cartId}")]
        public async Task<IActionResult> GetCartById(Guid cartId)
        {
            var cart = await _mediator.Send(new GetCartByIdRequest(cartId));

            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCart(CreateCartRequest createCartRequest)
        {
            //var createCartRequest = new CreateCartRequest(Guid.CreateVersion7());

            var c = await _mediator.Send(createCartRequest);

            return Ok(c);
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProdToCart(AddProductToCartRequest addProductToCartRequest)
        {
            await _mediator.Send(addProductToCartRequest);

            return Ok();
        }

        [HttpPost("deleteProduct")]
        public async Task<IActionResult> DeleteProdFromCart(RemoveProductFromCartRequest removeProductFromCartRequest)
        {
            await _mediator.Send(removeProductFromCartRequest);

            return Ok();
        }

        [HttpPost("finalizeCart")]
        public async Task<IActionResult> FinalizeCart(Guid cartId)
        {
            await _mediator.Send(new FinalizeCartRequest(cartId));

            return Ok();
        }
    }
}
