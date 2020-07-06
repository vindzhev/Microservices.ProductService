namespace ProductService.Infrastructure.Persistance.Repositories
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Microsoft.EntityFrameworkCore;
    
    using ProductService.Domain.Entities;
    using ProductService.Application.Common.Interfaces;

    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context) => 
            this._context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<Product> Add(Product product, CancellationToken cancellationToken)
        {
            await this._context.AddAsync(product, cancellationToken);
            return product;
        }

        public async Task<bool> CheckExists(Guid id, CancellationToken cancellationToken) =>
            await this._context.Products.AnyAsync(x => x.Id == id, cancellationToken);

        public async Task<ICollection<Product>> FindAllActive(CancellationToken cancellationToken) =>
            await this._context.Products
                .Include(x => x.Covers)
                .Include("Questions.Choices")
                .Where(x => x.Status == ProductStatus.Active)
                .ToArrayAsync(cancellationToken);

        public async Task<Product> FindById(Guid id, CancellationToken cancellationToken)
        {
            var a = await this._context.Products
                .Include(x => x.Covers)
                .Include("Questions.Choices")
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return a;
        }

        public async Task<Product> FindOne(string productCode, CancellationToken cancellationToken) =>
            await this._context.Products
                .Include(x => x.Covers)
                .Include("Questions.Choices")
                .FirstOrDefaultAsync(x => x.Code.Equals(productCode, StringComparison.InvariantCultureIgnoreCase), cancellationToken);

        public async Task SaveChangesAsync() => await this._context.SaveChangesAsync();
    }
}
