using Catalog.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Api.Data.EntityConfigurations
{
    public class PriceConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.HasKey(_=>_.Id);
        }
    }
}