
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IArticleService
    {
        Task<OperationResult> Create(Article_Dto dto);
        Task<OperationResult> Update(Article_Dto dto);
        Task<OperationResult> Delete(Article_Dto dto);
        Task<PaginationUtility<Article_Dto>> GetDataPagination(PaginationParam pagination, ArticleParam param, bool isPaging = true);
        Task<List<Article_Dto>> GetDataPaginationNotPagination(ArticleParam param);
        Task<Article_Dto> GetDetail(int id);
    }
}