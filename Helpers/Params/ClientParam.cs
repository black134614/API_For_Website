using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers.Params
{
    public class ClientParam
    {
        public int? ClientCategoryID { get; set; }
        public string Keyword { get; set; } = null;
    }
}