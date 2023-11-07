
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("ProductMainCategory")]
    public partial class ProductMainCategory
    {
        public ProductMainCategory()
        {
            ProductCategories = new HashSet<ProductCategory>();
        }

        [Key]
        public int ProductMainCategoryID { get; set; }
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

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.ProductMainCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty(nameof(ProductCategory.ProductMainCategory))]
        public virtual ICollection<ProductCategory> ProductCategories { get; set; }
    }
}
