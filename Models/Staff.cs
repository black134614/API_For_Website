
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    public partial class Staff
    {
        public Staff()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int StaffID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string AcademicTitles { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(50)]
        public string Specialize { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public bool? Gender { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        [StringLength(255)]
        public string WorkSchedule { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string CreateBy { get; set; }
        public int? StaffCategoryID { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(Account.Staff))]
        public virtual Account CreateByNavigation { get; set; }
        [ForeignKey(nameof(StaffCategoryID))]
        [InverseProperty("Staff")]
        public virtual StaffCategory StaffCategory { get; set; }
        [InverseProperty(nameof(Order.Staff))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
