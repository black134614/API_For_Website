
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("PictureMainCategory")]
    public partial class PictureMainCategory
    {
        public PictureMainCategory()
        {
            PictureCategories = new HashSet<PictureCategory>();
        }

        [Key]
        public int PictureMainCategoryID { get; set; }
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
        [InverseProperty(nameof(Account.PictureMainCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty(nameof(PictureCategory.PictureMainCategory))]
        public virtual ICollection<PictureCategory> PictureCategories { get; set; }
    }
}
