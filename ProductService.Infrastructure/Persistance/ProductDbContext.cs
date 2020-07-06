namespace ProductService.Infrastructure.Persistance
{
    using System.Threading;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;
    
    using ProductService.Domain.Entities;

    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Choice> Choices { get; set; }

        public DbSet<Cover> Covers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Question> Questions { get; set; }

        public Task<int> SaveChanges(CancellationToken cancellationToken = new CancellationToken()) => this.SaveChangesAsync(cancellationToken);

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
