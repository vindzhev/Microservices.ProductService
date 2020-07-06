namespace ProductService.Infrastructure.Persistance.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using ProductService.Domain.Entities;

    public class CoverEntityConfiguration : IEntityTypeConfiguration<Cover>
    {
        public void Configure(EntityTypeBuilder<Cover> builder)
        {
            builder.ToTable("Covers");

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.TotalInsured).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Product).WithMany(x => x.Covers);
        }
    }
}
