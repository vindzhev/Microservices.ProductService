namespace ProductService.Infrastructure.Persistance.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using ProductService.Domain.Entities;

    public class ChoiceEntityConfiguration : IEntityTypeConfiguration<Choice>
    {
        public void Configure(EntityTypeBuilder<Choice> builder)
        {
            builder.ToTable("Choices").HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Label).IsRequired():

            builder.HasOne(x => x.Question).WithMany(x => x.Choices);
        }
    }
}
