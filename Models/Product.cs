using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("Product")]
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int ProductID { get; set; }
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
        [StringLength(4000)]
        public string GalleryImageList { get; set; }
        [StringLength(50)]
        public string SourcePage { get; set; }
        [StringLength(255)]
        public string SourceLink { get; set; }
        public int? ViewTime { get; set; }
        [StringLength(4000)]
        public string AttachmentFile { get; set; }
        public double? Price { get; set; }
        public double? OldPrice { get; set; }
        [StringLength(4000)]
        public string Promotions { get; set; }
        [StringLength(4000)]
        public string WarrantyPolicy { get; set; }
        [StringLength(4000)]
        public string Specifications { get; set; }
        [StringLength(4000)]
        public string Accessories { get; set; }
        public int? Quantity { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public int? ProductCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.Products))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(ProductCategoryID))]
        [InverseProperty("Products")]
        public virtual ProductCategory ProductCategory { get; set; }
        [InverseProperty(nameof(OrderDetail.Product))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
