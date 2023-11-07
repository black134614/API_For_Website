
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IProductCategoryService
    {
        Task<OperationResult> Create(ProductCategory_Dto dto);
        Task<OperationResult> Update(ProductCategory_Dto dto);
        Task<OperationResult> Delete(ProductCategory_Dto dto);
        Task<PaginationUtility<ProductCategory_Dto>> GetDataPagination(PaginationParam pagination, ProductCategoryParam param, bool isPaging = true);
        Task<List<KeyValueUtility>> GetListProductCategory();
        Task<ProductCategory_Dto> GetDetail(int id);
    }
}