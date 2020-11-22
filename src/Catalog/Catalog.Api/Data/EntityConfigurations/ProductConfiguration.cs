using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.EntityConfigurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(_ => _.Id);
            builder.HasOne(_ => _.Category)
                .WithMany(_ => _.Products)
                .HasForeignKey(_ => _.CategoryId);
            builder.Property(_ => _.Code)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(_ => _.Name)
                  .IsRequired()
                  .HasMaxLength(100);
        }
    }
}