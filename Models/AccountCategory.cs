
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("AccountCategory")]
    public partial class AccountCategory
    {
        public AccountCategory()
        {
            Accounts = new HashSet<Account>();
        }

        [Key]
        [StringLength(50)]
        public string AccountCategoryID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        [StringLength(4000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        public bool? Status { get; set; }
        public int? Position { get; set; }

        [InverseProperty(nameof(Account.AccountCategory))]
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
