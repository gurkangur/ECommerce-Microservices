using System;
using Catalog.Api.Data.Context;

namespace Catalog.UnitTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly CatalogDbContext _context;

        public CommandTestBase()
        {
            _context = CatalogDbContextFactory.Create();
        }

        public void Dispose()
        {
            CatalogDbContextFactory.Destroy(_context);
        }
    }
}