
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class ProductMainCategoryRepository : Repository<ProductMainCategory>, IProductMainCategoryRepository
    {
        public ProductMainCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}