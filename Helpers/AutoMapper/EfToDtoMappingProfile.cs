using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Helpers.AutoMapper
{
    public class EfToDtoMappingProfile : Profile
    {
        public EfToDtoMappingProfile()
        {
            CreateMap<AccountCategory, AccountCategory_Dto>();
            CreateMap<Account, Account_Dto>();
            CreateMap<Article, Article_Dto>();
            CreateMap<ArticleCategory, ArticleCategory_Dto>();
            CreateMap<ArticleMainCategory, ArticleMainCategory_Dto>();
            CreateMap<ProductMainCategory, ProductMainCategory_Dto>();
            CreateMap<ProductCategory, ProductCategory_Dto>();
            CreateMap<Product, Product_Dto>();
            CreateMap<ClientCategory, ClientCategory_Dto>();
            CreateMap<Order, Order_Dto>();
            CreateMap<OrderDetail, OrderDetail_Dto>();
            CreateMap<Order, OrderStatus_Dto>();
            CreateMap<Client, Client_Dto>();
            CreateMap<Client, ClientSercure_Dto>();
        }
    }
}