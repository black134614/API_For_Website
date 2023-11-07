using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class OrderDetail_Dto
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }

        public string ProductName { get; set; }
    }
}