namespace ProductService.Application.Products.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    
    using ProductService.Application.Common.Interfaces;

    public class DiscontinueInsuranceCommand : IRequest<Unit>
    {
        public DiscontinueInsuranceCommand(Guid id) => 
            this.Id = id != Guid.Empty ? id : throw new ArgumentNullException(nameof(id));

        public Guid Id { get; set; }

        public class DiscontinueInsuranceCommandHandler : IRequestHandler<DiscontinueInsuranceCommand, Unit>
        {
            private readonly IProductRepository _productRepository;

            public DiscontinueInsuranceCommandHandler(IProductRepository productRepository)
            {
                this._productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<Unit> Handle(DiscontinueInsuranceCommand request, CancellationToken cancellationToken)
            {
                var product = await this._productRepository.FindById(request.Id, cancellationToken);

                product.Discontinue();

                await this._productRepository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
