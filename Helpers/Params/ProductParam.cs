
namespace API.Helpers.Params
{
    public class ProductParam
    {
        public int? ProductCategoryID { get; set; }
        public string Keyword { get; set; }

        public int? Price { get; set; }
        public bool? isOrderByDescending { get; set; }
        public int? ProductMainCategoryID { get; set; }
    }
}