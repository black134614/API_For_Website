
using API._Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public interface IRepositoryAccessor
    {
        public IAccountRepository Account { get; }
        public IAccountCategoryRepository AccountCategory { get; }
        public IArticleMainCategoryRepository ArticleMainCategory { get; }
        public IArticleCategoryRepository ArticleCategory { get; }
        public IArticleRepository Article { get; }
        public IClientRepository Client { get; }
        public IClientCategoryRepository ClientCategory { get; }
        public IOrderRepository Order { get; }
        public IOrderDetailRepository OrderDetail { get; }
        public IPictureMainCategoryRepository PictureMainCategory { get; }
        public IPictureCategoryRepository PictureCategory { get; }
        public IPictureRepository Picture { get; }
        public IProductMainCategoryRepository ProductMainCategory { get; }
        public IProductCategoryRepository ProductCategory { get; }
        public IProductRepository Product { get; }
        public IStaffMainCategoryRepository StaffMainCategory { get; }
        public IStaffCategoryRepository StaffCategory { get; }
        public IStaffRepository Staff { get; }
        Task<bool> SaveChangesAsync();
        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}