using System;

namespace Catalog.Api.Entities
{
    public class Image
    {
        public Guid Id { get; set; }
        public string AltText { get; set; }
        public string Format { get; set; }
        public int GalleryIndex { get; set; }
        public string Url { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public Guid ProductId { get; set; }
    }
}