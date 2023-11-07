using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class ClientCategoryRepository : Repository<ClientCategory>, IClientCategoryRepository
    {
        public ClientCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}