namespace ProductService.Infrastructure.Persistance.Configuration
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    
    using ProductService.Domain.Entities;

    public class QuestionEntityConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.ToTable("Questions").HasKey(x => x.Id);

            builder.Property(x => x.Code).IsRequired();
            builder.Property(x => x.Index).IsRequired();
            builder.Property(x => x.Text).IsRequired().HasMaxLength(200);

            builder.HasOne(x => x.Product).WithMany(x => x.Questions);

            builder.HasDiscriminator<int>("QuestionType")
                .HasValue<Question>(0)
                .HasValue<NumericQuestion>(1)
                .HasValue<DateQuestion>(2)
                .HasValue<ChoiceQuestion>(3);
        }
    }
}
