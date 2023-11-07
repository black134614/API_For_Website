
using API.Dtos;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IAccountCategoryService
    {
        Task<OperationResult> Create(AccountCategory_Dto dto);
        Task<OperationResult> Update(AccountCategory_Dto dto);
        Task<OperationResult> Delete(AccountCategory_Dto dto);
        Task<PaginationUtility<AccountCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true);
        Task<List<AccountCategory_Dto>> GetListAccountCategory();
        Task<AccountCategory_Dto> GetDetail(string id);
    }
}