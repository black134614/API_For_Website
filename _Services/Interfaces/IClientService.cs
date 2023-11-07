using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IClientService
    {
        Task<OperationResult> Create(Client_Dto dto);
        Task<OperationResult> Update(ClientSercure_Dto dto);
        Task<OperationResult> Delete(Client_Dto dto);
        Task<PaginationUtility<Client_Dto>> GetDataPagination(PaginationParam pagination, ClientParam param, bool isPaging = true);
        Task<Client_Dto> GetDetail(string email);
    }
}