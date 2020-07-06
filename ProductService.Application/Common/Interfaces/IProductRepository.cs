namespace ProductService.Application.Common.Interfaces
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using ProductService.Domain.Entities;

    public interface IProductRepository
    {
        Task<Product> Add(Product product, CancellationToken cancellationToken);

        Task<ICollection<Product>> FindAllActive(CancellationToken cancellationToken);

        Task<bool> CheckExists(Guid id, CancellationToken cancellationToken);

        Task<Product> FindOne(string productCode, CancellationToken cancellationToken);

        Task<Product> FindById(Guid id, CancellationToken cancellationToken);

        Task SaveChangesAsync();
    }
}
