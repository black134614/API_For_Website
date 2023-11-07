using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;   
namespace API._Services.Interfaces
{
    public interface IStatiticService
    {
        Task<OperationResult> GetDataStatitic();
    }
}