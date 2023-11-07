
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class PictureCategoryRepository : Repository<PictureCategory>, IPictureCategoryRepository
    {
        public PictureCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}