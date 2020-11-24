using System;
using Catalog.Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Catalog.UnitTests.Common
{
    public class CatalogDbContextFactory
    {
        public static CatalogDbContext Create()
        {
            var options = new DbContextOptionsBuilder<CatalogDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new CatalogDbContext(options);

            context.Categories.Add(new Api.Entities.Category()
            {
                Id = 1,
                Code = "001",
                Name = "Lenovo Thinkpad"
            });
            context.SaveChanges();




            context.Database.EnsureCreated();

            return context;
        }

        public static void Destroy(CatalogDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}