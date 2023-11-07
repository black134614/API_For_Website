
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("ProductCategory")]
    public partial class ProductCategory
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        public int ProductCategoryID { get; set; }
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
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public int? ProductMainCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.ProductCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(ProductMainCategoryID))]
        [InverseProperty("ProductCategories")]
        public virtual ProductMainCategory ProductMainCategory { get; set; }
        [InverseProperty(nameof(Product.ProductCategory))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
