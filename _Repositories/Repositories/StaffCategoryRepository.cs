
using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class StaffCategoryRepository : Repository<StaffCategory>, IStaffCategoryRepository
    {
        public StaffCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}