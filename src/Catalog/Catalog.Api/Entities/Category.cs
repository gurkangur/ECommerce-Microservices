using System.Collections.Generic;

namespace Catalog.Api.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}