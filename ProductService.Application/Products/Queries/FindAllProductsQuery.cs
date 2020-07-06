namespace ProductService.Application.Products.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using MediatR;
    using AutoMapper;
    
    using ProductService.Application.Common.Interfaces;
    using ProductService.Application.Common.Models.Product;

    public class FindAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public class FindAllProductsQueryHandler : IRequestHandler<FindAllProductsQuery, IEnumerable<ProductDTO>>
        {
            private readonly IProductRepository _productRepository;
            private readonly IMapper _mapper;

            public FindAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
            {
                this._mapper = mapper ?? 
                    throw new ArgumentNullException(nameof(mapper));

                this._productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<IEnumerable<ProductDTO>> Handle(FindAllProductsQuery request, CancellationToken cancellationToken) =>
                this._mapper.Map<IEnumerable<ProductDTO>>(await this._productRepository.FindAllActive(cancellationToken));
        }
    }
}
