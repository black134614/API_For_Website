using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class ProductOrder_Dto
    {
        public int ProductID { get; set; }
        public double? Price { get; set; }
        public int? Quantity { get; set; }
    }
}