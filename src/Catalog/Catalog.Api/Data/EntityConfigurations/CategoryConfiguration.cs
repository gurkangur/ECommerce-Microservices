using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.EntityConfigurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(_=>_.Id);
            builder.Property(_=>_.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}