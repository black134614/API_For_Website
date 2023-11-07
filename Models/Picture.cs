
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("Picture")]
    public partial class Picture
    {
        [Key]
        public int PictureID { get; set; }
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
        public int? ViewTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public int? PictureCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.Pictures))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(PictureCategoryID))]
        [InverseProperty("Pictures")]
        public virtual PictureCategory PictureCategory { get; set; }
    }
}
