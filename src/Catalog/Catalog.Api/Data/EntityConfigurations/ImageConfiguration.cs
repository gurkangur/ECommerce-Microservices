using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.EntityConfigurations
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(_=>_.Id);
            builder.HasOne(_=>_.Product)
                .WithMany(_=>_.Images)
                .HasForeignKey(_=>_.ProductId);
        }
    }
}