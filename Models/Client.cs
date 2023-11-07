
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int ClientID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        [StringLength(100)]
        public string Password { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string Avatar { get; set; }
        [StringLength(255)]
        public string Thumb { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public bool? Status { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        [StringLength(50)]
        public string ApproveBy { get; set; }
        public int? ClientCategoryID { get; set; }

        [ForeignKey(nameof(ApproveBy))]
        [InverseProperty(nameof(Account.Clients))]
        public virtual Account ApproveByNavigation { get; set; }
        [ForeignKey(nameof(ClientCategoryID))]
        [InverseProperty("Clients")]
        public virtual ClientCategory ClientCategory { get; set; }
        [InverseProperty(nameof(Order.Client))]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
