

using API.Models;

namespace API.Dtos
{
    public class OrderImport_Dto
    {
        public int? OrderID { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public int? ProductID { get; set; }
        public string ProductName { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}