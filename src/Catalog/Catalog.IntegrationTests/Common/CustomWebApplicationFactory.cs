using System;
using System.Net.Http;
using System.Threading.Tasks;
using Catalog.Api.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.IntegrationTests.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .ConfigureServices(services =>
                {
                    // Create a new service provider.
                    var serviceProvider = new ServiceCollection()
                                .AddEntityFrameworkInMemoryDatabase()
                                .BuildServiceProvider();

                    // Add a database context using an in-memory 
                    // database for testing.
                    services.AddDbContext<CatalogDbContext>(options =>
                            {
                                options.UseInMemoryDatabase("InMemoryDbForTesting");
                                options.UseInternalServiceProvider(serviceProvider);
                            });


                    var sp = services.BuildServiceProvider();

                    // Create a scope to obtain a reference to the database
                    using var scope = sp.CreateScope();
                    var scopedServices = scope.ServiceProvider;
                    var context = scopedServices.GetRequiredService<CatalogDbContext>();

                    // Ensure the database is created.
                    context.Database.EnsureCreated();
                    try
                    {
                        Utilities.InitializeDbForTests(context);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                })
                .UseEnvironment("Test");
        }
        public HttpClient GetAnonymousClient()
        {
            return CreateClient();
        }
    }
}