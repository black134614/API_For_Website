using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Order")]
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        [Key]
        public int OrderID { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        public double? Total { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [StringLength(20)]
        public string Mobi { get; set; }
        [StringLength(20)]
        public string Mobi2 { get; set; }
        [StringLength(200)]
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public int? PaymentMethod { get; set; }
        [StringLength(4000)]
        public string Comment { get; set; }
        public bool? OrderStatus { get; set; }
        public bool? DeliverStatus { get; set; }
        public bool? ChargeStatus { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreateTime { get; set; }
        public int? ClientID { get; set; }
        public int? StaffID { get; set; }

        [ForeignKey(nameof(ClientID))]
        [InverseProperty("Orders")]
        public virtual Client Client { get; set; }
        [ForeignKey(nameof(StaffID))]
        [InverseProperty("Orders")]
        public virtual Staff Staff { get; set; }
        [InverseProperty(nameof(OrderDetail.Order))]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
