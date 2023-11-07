using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("StaffMainCategory")]
    public partial class StaffMainCategory
    {
        public StaffMainCategory()
        {
            StaffCategories = new HashSet<StaffCategory>();
        }

        [Key]
        public int StaffMainCategoryID { get; set; }
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
        [InverseProperty(nameof(Account.StaffMainCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty(nameof(StaffCategory.StaffMainCategory))]
        public virtual ICollection<StaffCategory> StaffCategories { get; set; }
    }
}
