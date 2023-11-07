
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("Contact")]
    public partial class Contact
    {
        [Key]
        public int ContactID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [Column(TypeName = "ntext")]
        public string Content { get; set; }
        [StringLength(4000)]
        public string AttachmentFile { get; set; }
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string ApproveBy { get; set; }
        public int? ContactCategoryID { get; set; }

        [ForeignKey(nameof(ApproveBy))]
        [InverseProperty(nameof(Account.Contacts))]
        public virtual Account ApproveByNavigation { get; set; }
        [ForeignKey(nameof(ContactCategoryID))]
        [InverseProperty("Contacts")]
        public virtual ContactCategory ContactCategory { get; set; }
    }
}
