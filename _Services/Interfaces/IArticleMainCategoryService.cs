
using API.Dtos;
using API.Helpers.Utilities;

namespace API._Services.Interfaces
{
    public interface IArticleMainCategoryService
    {
        Task<OperationResult> Create(ArticleMainCategory_Dto dto);
        Task<OperationResult> Update(ArticleMainCategory_Dto dto);
        Task<OperationResult> Delete(ArticleMainCategory_Dto dto);
        Task<PaginationUtility<ArticleMainCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true);
        Task<ArticleMainCategory_Dto> GetDetail(int id);
        Task<List<KeyValueUtility>> GetListArticleMainCategory();
    }
}