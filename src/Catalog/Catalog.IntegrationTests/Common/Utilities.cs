using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Catalog.Api.Data.Context;
using Catalog.Api.Entities;
using Newtonsoft.Json;

namespace Catalog.IntegrationTests.Common
{
 public class Utilities
    {
        public static StringContent GetRequestContent(object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }

        public static async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            var stringResponse = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<T>(stringResponse);

            return result;
        }

        public static void InitializeDbForTests(CatalogDbContext context)
        {
            context.Categories.Add(new Category()
            {
                Id = 1,
                Code = "001",
                Name = "Lenovo Thinkpad 1"
            });
                       context.Categories.Add(new Category()
            {
                Id = 2,
                Code = "002",
                Name = "Lenovo Thinkpad 2"
            });
                       context.Categories.Add(new Category()
            {
                Id = 3,
                Code = "003",
                Name = "Lenovo Thinkpad 3"
            });
            context.SaveChanges();
        }
    }
}