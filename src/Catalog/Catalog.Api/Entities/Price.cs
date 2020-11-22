using System;

namespace Catalog.Api.Entities
{
    public class Price
    {
        public Guid Id { get; set; }
        public string FormattedValue { get; set; }
        public decimal MaxQuantity { get; set; }
        public decimal MinQuantity { get; set; }
        public decimal Value { get; set; }
    }
}