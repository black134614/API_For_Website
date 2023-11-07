
namespace API.Dtos
{
    public class Product_Dto
    {
        public int ProductID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Thumb { get; set; }
        public string Content { get; set; }
        public string Keyword { get; set; }
        public string ImageList { get; set; }
        public string GalleryImageList { get; set; }
        public string SourcePage { get; set; }
        public string SourceLink { get; set; }
        public int? ViewTime { get; set; }
        public string AttachmentFile { get; set; }
        public double? Price { get; set; }
        public double? OldPrice { get; set; }
        public string Promotions { get; set; }
        public string WarrantyPolicy { get; set; }
        public string Specifications { get; set; }
        public string Accessories { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateBy { get; set; }
        public int? ProductCategoryID { get; set; }

        public string FileUpload { get; set; }
        public List<IFormFile> ListThumb { get; set; }
        public List<string> ListImages { get; set; }
        public List<IFormFile> ListGalleryImages { get; set; }
        public string ProductCategory_Title { get; set; }
        public int ProductMainCategoryID { get; set; }
    }
}