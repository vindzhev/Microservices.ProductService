namespace ProductService.Application.Products.Commands
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using MediatR;
    
    using ProductService.Application.Common.Interfaces;

    public class ActivateInsuranceCommand : IRequest<Unit>
    {
        public ActivateInsuranceCommand(Guid id) => 
            this.Id = id != Guid.Empty ? id : throw new ArgumentNullException(nameof(id));

        public Guid Id { get; set; }

        public class ActivateInsuranceCommandHandler : IRequestHandler<ActivateInsuranceCommand, Unit>
        {
            private readonly IProductRepository _productRepository;

            public ActivateInsuranceCommandHandler(IProductRepository productRepository)
            {
                this._productRepository = productRepository ?? 
                    throw new ArgumentNullException(nameof(productRepository));
            }

            public async Task<Unit> Handle(ActivateInsuranceCommand request, CancellationToken cancellationToken)
            {
                var product = await this._productRepository.FindById(request.Id, cancellationToken);

                product.Activate();

                await this._productRepository.SaveChangesAsync();

                return Unit.Value;
            }
        }
    }
}
