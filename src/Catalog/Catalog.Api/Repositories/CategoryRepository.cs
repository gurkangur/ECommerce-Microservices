using Catalog.Api.Data.Context;
using Catalog.Api.Entities;
using Catalog.Api.Repositories.Interfaces;

namespace Catalog.Api.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(CatalogDbContext context) : base(context)
        {
        }
    }
}