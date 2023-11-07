
namespace API.Dtos
{
    public class ProductCategory_Dto
    {
        public int? ProductCategoryID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Thumb { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        public DateTime? CreateTime { get; set; }
        public string CreateBy { get; set; }
        public int? ProductMainCategoryID { get; set; }

        public string FileUpload { get; set; }
        public List<IFormFile> ListThumb { get; set; }
        public string ProductMainCategory_Title { get; set; }
    }
}