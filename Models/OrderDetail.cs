using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace API.Models
{
    [Table("OrderDetail")]
    public partial class OrderDetail
    {
        [Key]
        public int OrderID { get; set; }
        [Key]
        public int ProductID { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        [ForeignKey(nameof(OrderID))]
        [InverseProperty("OrderDetails")]
        public virtual Order Order { get; set; }
        [ForeignKey(nameof(ProductID))]
        [InverseProperty("OrderDetails")]
        public virtual Product Product { get; set; }
    }
}
