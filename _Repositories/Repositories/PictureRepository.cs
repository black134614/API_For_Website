
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class PictureRepository : Repository<Picture>, IPictureRepository
    {
        public PictureRepository(DBContext context) : base(context)
        {
        }
    }
}