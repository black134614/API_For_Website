using System.Linq;
using API._Repositories;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Params;
using API.Helpers.Utilities;
using API.Models;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class StatiticService : IStatiticService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;
        public StatiticService(
            IRepositoryAccessor repository,
            IMapper mapper,
            MapperConfiguration mapperConfiguration,
            IFunctionUtility functionUtility)
        {
            _repository = repository;
            _mapper = mapper;
            _mapperConfiguration = mapperConfiguration;
            _functionUtility = functionUtility;
        }

        public Task<OperationResult> GetDataStatitic()
        {

            var countProductMainCate = _repository.ProductMainCategory.FindAll().ToList().Count;
            var countProductCate = _repository.ProductCategory.FindAll().ToList().Count;
            var countProduct = _repository.Product.FindAll().ToList().Count;
            var countActiveProduct = _repository.Product.FindAll(x => x.Status == true).ToList().Count;

            var countArticleMainCate = _repository.ArticleMainCategory.FindAll().ToList().Count;
            var countArticleCate = _repository.ArticleCategory.FindAll().ToList().Count;
            var countArticle = _repository.Article.FindAll().ToList().Count;
            var countActiveArticle = _repository.Article.FindAll(x => x.Status == true).ToList().Count;

            var countClient = _repository.Client.FindAll().ToList().Count;

            var countOrder = _repository.Order.FindAll(x => x.Email != "admin@gmail.com").ToList().Count;
            var CountOderStatus = _repository.Order.FindAll(x => x.Email != "admin@gmail.com" && x.OrderStatus == true && x.DeliverStatus != true && x.ChargeStatus != true).ToList().Count;
            var CountOderDeliverStatus = _repository.Order.FindAll(x => x.Email != "admin@gmail.com" && x.OrderStatus == true && x.DeliverStatus == true && x.ChargeStatus != true).ToList().Count;
            var CountOderChargeStatus = _repository.Order.FindAll(x => x.Email != "admin@gmail.com" && x.OrderStatus == true && x.DeliverStatus == true && x.ChargeStatus == true).ToList().Count;

            var data = new
            {
                countProductMainCate,
                countProductCate,
                countProduct,
                countActiveProduct,

                countArticleMainCate,
                countArticleCate,
                countArticle,
                countActiveArticle,

                countClient,

                countOrder,
                CountOderStatus,
                CountOderDeliverStatus,
                CountOderChargeStatus
            };

            return Task.FromResult(new OperationResult { IsSuccess = true, Data = data });
        }
    }
}