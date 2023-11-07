
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IArticleCategoryService
    {
        Task<OperationResult> Create(ArticleCategory_Dto dto);
        Task<OperationResult> Update(ArticleCategory_Dto dto);
        Task<OperationResult> Delete(ArticleCategory_Dto dto);
        Task<PaginationUtility<ArticleCategory_Dto>> GetDataPagination(PaginationParam pagination, ArticleCategoryParam param, bool isPaging = true);
        Task<ArticleCategory_Dto> GetDetail(int id);
        Task<List<KeyValueUtility>> GetListArticleCategory();

    }
}