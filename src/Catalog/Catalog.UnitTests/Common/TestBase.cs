using System;
using Catalog.Api.Data.Context;

namespace Catalog.UnitTests.Common
{
    public class TestBase : IDisposable
    {
        protected readonly CatalogDbContext _context;

        public TestBase()
        {
            _context = CatalogDbContextFactory.Create();
        }

        public void Dispose()
        {
            CatalogDbContextFactory.Destroy(_context);
        }
    }
}