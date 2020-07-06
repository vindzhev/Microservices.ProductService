namespace ProductService.Application.Products.Queries
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    
    using ProductService.Application.Common.Interfaces;

    public class ProductExistsQuery : IRequest<bool>
    {
        public ProductExistsQuery(Guid id) =>
            this.Id = Guid.Empty != id ? id : throw new ArgumentNullException(nameof(id));

        public Guid Id { get; set; }

        public class ProductExistsQueryHandler : IRequestHandler<ProductExistsQuery, bool>
        {
            private readonly IProductRepository _productRepository;

            public ProductExistsQueryHandler(IProductRepository productRepository)
            {
                this._productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<bool> Handle(ProductExistsQuery request, CancellationToken cancellationToken) =>
                await this._productRepository.CheckExists(request.Id, cancellationToken);
        }
    }
}
