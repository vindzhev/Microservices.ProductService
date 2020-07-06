namespace ProductService.Application.Products.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using AutoMapper;
    
    using ProductService.Application.Common.Interfaces;
    using ProductService.Application.Common.Models.Product;

    public class FindProductByIdQuery : IRequest<ProductDTO>
    {
        public FindProductByIdQuery(Guid id) => 
            this.Id = Guid.Empty != id ? id : throw new ArgumentNullException(nameof(id));

        public Guid Id { get; }

        public class FindAllProductsQueryHandler : IRequestHandler<FindProductByIdQuery, ProductDTO>
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

            public async Task<ProductDTO> Handle(FindProductByIdQuery request, CancellationToken cancellationToken) =>
                this._mapper.Map<ProductDTO>(await this._productRepository.FindById(request.Id, cancellationToken));
        }
    }
}
