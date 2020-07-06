namespace ProductService.Application.Products.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using AutoMapper;
    
    using ProductService.Application.Common.Interfaces;
    using ProductService.Application.Common.Models.Product;

    public class FindProductByCodeQuery : IRequest<ProductDTO>
    {
        public FindProductByCodeQuery(string code) => 
            this.Code = !string.IsNullOrEmpty(code) ? 
                code : throw new ArgumentNullException(nameof(code));

        public string Code { get; }

        public class FindAllProductsQueryHandler : IRequestHandler<FindProductByCodeQuery, ProductDTO>
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

            public async Task<ProductDTO> Handle(FindProductByCodeQuery request, CancellationToken cancellationToken) =>
                this._mapper.Map<ProductDTO>(await this._productRepository.FindOne(request.Code, cancellationToken));
        }
    }
}
