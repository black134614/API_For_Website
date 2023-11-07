using API._Repositories.Interfaces;
using API._Repositories.Repositories;
using API.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace API._Repositories
{
    public class RepositoryAccessor : IRepositoryAccessor
    {
        private DBContext _context;

        public RepositoryAccessor(DBContext context)
        {
            _context = context;
            Account = new AccountRepository(_context);
            AccountCategory = new AccountCategoryRepository(_context);
            ArticleCategory = new ArticleCategoryRepository(_context);
            Article = new ArticleRepository(_context);
            ArticleMainCategory = new ArticleMainCategoryRepository(_context);
            ClientCategory = new ClientCategoryRepository(_context);
            Client = new ClientRepository(_context);
            Order = new OrderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
            PictureMainCategory = new PictureMainCategoryRepository(_context);
            PictureCategory = new PictureCategoryRepository(_context);
            Picture = new PictureRepository(_context);
            ProductMainCategory = new ProductMainCategoryRepository(_context);
            ProductCategory = new ProductCategoryRepository(_context);
            Product = new ProductRepository(_context);
            StaffMainCategory = new StaffMainCategoryRepository(_context);
            StaffCategory = new StaffCategoryRepository(_context);
            Staff = new StaffRepository(_context);
        }

        public IAccountRepository Account { get; private set; }

        public IAccountCategoryRepository AccountCategory { get; private set; }

        public IArticleCategoryRepository ArticleCategory { get; private set; }

        public IArticleRepository Article { get; private set; }

        public IArticleMainCategoryRepository ArticleMainCategory { get; private set; }

        public IClientRepository Client { get; private set; }

        public IClientCategoryRepository ClientCategory { get; private set; }

        public IOrderRepository Order { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }

        public IPictureMainCategoryRepository PictureMainCategory { get; private set; }

        public IPictureCategoryRepository PictureCategory { get; private set; }

        public IPictureRepository Picture { get; private set; }

        public IProductMainCategoryRepository ProductMainCategory { get; private set; }

        public IProductCategoryRepository ProductCategory { get; private set; }

        public IProductRepository Product { get; private set; }

        public IStaffMainCategoryRepository StaffMainCategory { get; private set; }

        public IStaffCategoryRepository StaffCategory { get; private set; }

        public IStaffRepository Staff { get; private set; }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
    }
}