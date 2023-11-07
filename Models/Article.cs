﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("Article")]
    public partial class Article
    {
        [Key]
        public int ArticleID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(200)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        [StringLength(4000)]
        public string Keyword { get; set; }
        [StringLength(4000)]
        public string ImageList { get; set; }
        [StringLength(50)]
        public string SourcePage { get; set; }
        [StringLength(255)]
        public string SourceLink { get; set; }
        [StringLength(4000)]
        public string AttachmentFile { get; set; }
        public int? ViewTime { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        public int? ArticleCategoryID { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }

        [ForeignKey(nameof(ArticleCategoryID))]
        [InverseProperty("Articles")]
        public virtual ArticleCategory ArticleCategory { get; set; }
        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.Articles))]
        public virtual Account CreateByNavigation { get; set; }
    }
}
