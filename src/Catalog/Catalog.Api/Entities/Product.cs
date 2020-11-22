using System;
using System.Collections.Generic;

namespace Catalog.Api.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid PriceId { get; set; }
        public Price Price { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string NameHtml { get; set; }
        public string Summary { get; set; }
        public string Url { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}