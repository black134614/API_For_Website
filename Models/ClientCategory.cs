
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("ClientCategory")]
    public partial class ClientCategory
    {
        public ClientCategory()
        {
            Clients = new HashSet<Client>();
        }

        [Key]
        public int ClientCategoryID { get; set; }
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
        [InverseProperty(nameof(Account.ClientCategories))]
        public virtual Account CreateByNavigation { get; set; }
        [InverseProperty(nameof(Client.ClientCategory))]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
