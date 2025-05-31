using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Product.Domain;

namespace Product.Application.GetProducts
{
    public class GetProductsHandler(IProductRepository productRepository) : IRequestHandler<GetProductsRequest, IEnumerable<Domain.Product>>
    {
        private readonly IProductRepository _productRepository = productRepository;

        public async Task<IEnumerable<Domain.Product>> Handle(GetProductsRequest request, CancellationToken cancellationToken)
        {
            var prod = await _productRepository.GetAll();
            if (!prod.Any())
                throw new Exception("No products");
            return prod;
        }
    }
}
