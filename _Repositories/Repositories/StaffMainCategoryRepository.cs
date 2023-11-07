using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class StaffMainCategoryRepository : Repository<StaffMainCategory>, IStaffMainCategoryRepository
    {
        public StaffMainCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}