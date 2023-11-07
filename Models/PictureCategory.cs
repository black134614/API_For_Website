
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("PictureCategory")]
    public partial class PictureCategory
    {
        public PictureCategory()
        {
            Pictures = new HashSet<Picture>();
        }

        [Key]
        public int PictureCategoryID { get; set; }
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
        public int? PictureMainCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.PictureCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(PictureMainCategoryID))]
        [InverseProperty("PictureCategories")]
        public virtual PictureMainCategory PictureMainCategory { get; set; }
        [InverseProperty(nameof(Picture.PictureCategory))]
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
