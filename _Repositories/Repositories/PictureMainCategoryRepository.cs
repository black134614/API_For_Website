

using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class PictureMainCategoryRepository : Repository<PictureMainCategory>, IPictureMainCategoryRepository
    {
        public PictureMainCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}