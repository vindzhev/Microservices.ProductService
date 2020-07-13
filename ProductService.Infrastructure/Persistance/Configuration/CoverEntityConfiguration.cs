namespace ProductService.Infrastructure.Persistance.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using ProductService.Domain.Entities;

    public class CoverEntityConfiguration : IEntityTypeConfiguration<Cover>
    {
        public void Configure(EntityTypeBuilder<Cover> builder)
        {
            builder.ToTable("Covers").HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).HasMaxLength(1000);
            builder.Property(x => x.Optional).IsRequired();
            builder.Property(x => x.TotalInsured).HasColumnType("decimal(18,2)");

            builder.HasOne(x => x.Product).WithMany(x => x.Covers);
        }
    }
}
