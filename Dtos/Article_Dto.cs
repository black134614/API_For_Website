
namespace API.Dtos
{
    public class Article_Dto
    {
        public int? ArticleID { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public string Thumb { get; set; }
        public string Content { get; set; }
        public string Keyword { get; set; }
        public string ImageList { get; set; }
        public string SourcePage { get; set; }
        public string SourceLink { get; set; }
        public string AttachmentFile { get; set; }
        public int? ViewTime { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ArticleCategoryID { get; set; }
        public string CreateBy { get; set; }

        public string FileUpload { get; set; }
        public List<IFormFile> ListThumb { get; set; }
        public List<IFormFile> ListImages { get; set; }
        public string ArticleCategory_Title { get; set; }
    }
}