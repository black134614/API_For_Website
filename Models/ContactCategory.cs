using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("ContactCategory")]
    public partial class ContactCategory
    {
        public ContactCategory()
        {
            Contacts = new HashSet<Contact>();
        }

        [Key]
        public int ContactCategoryID { get; set; }
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
        [InverseProperty(nameof(Account.ContactCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty(nameof(Contact.ContactCategory))]
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
