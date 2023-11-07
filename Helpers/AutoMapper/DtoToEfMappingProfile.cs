
using API.Dtos;
using API.Models;
using AutoMapper;

namespace API.Helpers.AutoMapper
{
    public class DtoToEfMappingProfile : Profile
    {
        public DtoToEfMappingProfile()
        {
            CreateMap<AccountCategory_Dto, AccountCategory>();
            CreateMap<Account_Dto, Account>();
            CreateMap<Article_Dto, Article>();
            CreateMap<ArticleCategory_Dto, ArticleCategory>();
            CreateMap<ArticleMainCategory_Dto, ArticleMainCategory>();
            CreateMap<ProductMainCategory_Dto, ProductMainCategory>();
            CreateMap<ProductCategory_Dto, ProductCategory>();
            CreateMap<Product_Dto, Product>();
            CreateMap<Client_Dto, Client>();
            CreateMap<ClientSercure_Dto, Client>();
            CreateMap<Order_Dto, Order>();
            CreateMap<OrderDetail_Dto, OrderDetail>();
            CreateMap<OrderStatus_Dto, Order>();
            CreateMap<ClientCategory_Dto, ClientCategory>();
        }
    }
}