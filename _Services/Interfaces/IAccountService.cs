using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IAccountService
    {
        Task<OperationResult> Create(Account_Dto dto);
        Task<OperationResult> Update(Account_Dto dto);
        Task<OperationResult> Delete(Account_Dto dto);
        Task<PaginationUtility<Account_Dto>> GetDataPagination(PaginationParam pagination, AccountParam param, bool isPaging = true);
        Task<Account_Dto> GetDetail(string username);
    }
}