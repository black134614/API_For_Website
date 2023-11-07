
using API.Dtos;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IOrderService
    {
        Task<OperationResult> Create(Order_Dto dto);
        Task<OperationResult> Update(Order_Dto dto);
        Task<OperationResult> UpdateStatus(OrderStatus_Dto dto);
        Task<OperationResult> Delete(Order_Dto dto);
        Task<PaginationUtility<Order_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true);
        Task<List<OrderDetail_Dto>> GetDetail(int id);
        Task<List<Order_Dto>> GetOderNotConfirm();
        Task<bool> MinusProduct(int productID);
        Task<OperationResult> GetOrderImport(); 
    }
}