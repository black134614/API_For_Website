using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class AccountCategoryRepository : Repository<AccountCategory>, IAccountCategoryRepository
    {
        public AccountCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}