
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IProductService
    {
        Task<OperationResult> Create(Product_Dto dto);
        Task<OperationResult> Update(Product_Dto dto);
        Task<OperationResult> Delete(Product_Dto dto);
        Task<PaginationUtility<Product_Dto>> GetDataPagination(PaginationParam pagination, ProductParam param, bool isPaging = true);
        Task<PaginationUtility<Product_Dto>> UserGetDataPagination(PaginationParam pagination, ProductParam param, bool isPaging = true);
        Task<List<Product_Dto>> GetDataByMainCategory(int? mainCateID, int? quantity);
        Task<Product_Dto> GetDetail(int id);
        Task<List<Product_Dto>> GetDataNewProduct();

        Task<List<Product_Dto>> Top20ViewProduct();
    }
}