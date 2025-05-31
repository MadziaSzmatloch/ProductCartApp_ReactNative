using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Domain;

namespace Product.Application.ReserveProduct
{
    public class ReserveProductHandler(IProductRepository productRepository) : IRequestHandler<ReserveProductRequest>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task Handle(ReserveProductRequest request, CancellationToken cancellationToken)
        {
            var prod = await _productRepository.GetAll();
            if (prod.FirstOrDefault(p => p.Id == request.ProductId) == null)
                throw new Exception("No product with given id");
            if(prod.FirstOrDefault(p => p.Id == request.ProductId)!.Quantity < request.Quantity)
                throw new Exception("Not enough product");
            await _productRepository.ReserveProduct(request.ProductId, request.Quantity);
        }
    }
}
