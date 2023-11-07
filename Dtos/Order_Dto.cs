

using API.Models;

namespace API.Dtos
{
    public class Order_Dto
    {
        public int? OrderID { get; set; }
        public string Code { get; set; }
        public double? Total { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Mobi { get; set; }
        public string Mobi2 { get; set; }
        public string Address { get; set; }
        public bool? Gender { get; set; }
        public int? PaymentMethod { get; set; }
        public string Comment { get; set; }
        public bool? OrderStatus { get; set; }
        public bool? DeliverStatus { get; set; }
        public bool? ChargeStatus { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? ClientID { get; set; }
        public int? StaffID { get; set; }
        public List<ProductOrder_Dto> ListProducts { get; set; }
    }
}