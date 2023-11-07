using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("StaffCategory")]
    public partial class StaffCategory
    {
        public StaffCategory()
        {
            Staff = new HashSet<Staff>();
        }

        [Key]
        public int StaffCategoryID { get; set; }
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
        public int? StaffMainCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.StaffCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(StaffMainCategoryID))]
        [InverseProperty("StaffCategories")]
        public virtual StaffMainCategory StaffMainCategory { get; set; }
        [InverseProperty("StaffCategory")]
        public virtual ICollection<Staff> Staff { get; set; }
    }
}
