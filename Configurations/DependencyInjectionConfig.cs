using API._Repositories;
using API._Services.Interfaces;
using API._Services.Interfaces.Auth;
using API._Services.Services;
using API._Services.Services.Auth;
using API.Helpers.Utilities;

namespace API.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            //Repositories
            services.AddScoped<IRepositoryAccessor, RepositoryAccessor>();


            services.AddScoped<IFunctionUtility, FunctionUtility>();
            services.AddScoped<IFunctionMediaUtility, FunctionMediaUtility>();
            services.AddScoped<IJwtUtility, JwtUtility>();
            services.AddScoped<IMailUtility, MailUtility>();
            services.AddScoped<IMailKitUtility, MailKitUtility>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAccountCategoryService, AccountCategoryService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IArticleService, ArticleService>();
            services.AddScoped<IArticleCategoryService, ArticleCategoryService>();
            services.AddScoped<IArticleMainCategoryService, ArticleMainCategoryService>();
            services.AddScoped<IProductMainCategoryService, ProductMainCategoryService>();
            services.AddScoped<IProductCategoryService, ProductCategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IStatiticService, StatiticService>();
        }
    }
}