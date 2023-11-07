

using API.Models;

namespace API.Dtos
{
    public class OrderStatus_Dto
    {
        public int? OrderID { get; set; }
        public bool? OrderStatus { get; set; }
        public bool? DeliverStatus { get; set; }
        public bool? ChargeStatus { get; set; }
    }
}