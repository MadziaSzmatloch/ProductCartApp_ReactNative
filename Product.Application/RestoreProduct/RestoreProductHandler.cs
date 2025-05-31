using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Application.ReserveProduct;
using Product.Domain;

namespace Product.Application.RestoreProduct
{
    public class RestoreProductHandler(IProductRepository productRepository) : IRequestHandler<RestoreProductRequest>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task Handle(RestoreProductRequest request, CancellationToken cancellationToken)
        {
            var prod = await _productRepository.GetAll();
            if (prod.FirstOrDefault(p => p.Id == request.ProductId) == null)
                throw new Exception("No product with given id");

            await _productRepository.RestoreProduct(request.ProductId, request.Quantity);
        }
    }
}
