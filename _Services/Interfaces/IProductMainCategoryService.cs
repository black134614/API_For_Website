
using API.Dtos;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IProductMainCategoryService
    {
        Task<OperationResult> Create(ProductMainCategory_Dto dto);
        Task<OperationResult> Update(ProductMainCategory_Dto dto);
        Task<OperationResult> Delete(ProductMainCategory_Dto dto);
        Task<PaginationUtility<ProductMainCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true);
        Task<List<KeyValueUtility>> GetListProductMainCategory();
        Task<ProductMainCategory_Dto> GetDetail(int id);
    }
}