using API._Repositories.Interfaces;
using API.Data;
using API.Models;

namespace API._Repositories.Repositories
{
    public class ArticleMainCategoryRepository : Repository<ArticleMainCategory>, IArticleMainCategoryRepository
    {
        public ArticleMainCategoryRepository(DBContext context) : base(context)
        {
        }
    }
}