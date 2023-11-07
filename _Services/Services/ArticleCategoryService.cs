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
    public class ArticleCategoryService : IArticleCategoryService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ArticleCategoryService(
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
        public async Task<OperationResult> Create(ArticleCategory_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\articleCategories\\", $"ArticleCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\articleCategories\\", $"ArticleCategory_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<ArticleCategory>(dto);
            _repository.ArticleCategory.Add(data);
            try
            {
                await _repository.SaveChangesAsync();
                return new OperationResult { IsSuccess = true };
            }
            catch (System.Exception ex)
            {
                return new OperationResult { IsSuccess = false, Error = ex.Message };
            }
        }

        public async Task<OperationResult> Delete(ArticleCategory_Dto dto)
        {
            var data = _mapper.Map<ArticleCategory>(dto);
            var listArticle = await _repository.Article.FindAll(x => x.ArticleCategoryID == dto.ArticleCategoryID).ToListAsync();
            foreach (var article in listArticle)
            {
                _repository.Article.Remove(article);
            }
            _repository.ArticleCategory.Remove(data);
            try
            {
                await _repository.SaveChangesAsync();
                return new OperationResult { IsSuccess = true };
            }
            catch (System.Exception ex)
            {
                return new OperationResult { IsSuccess = false, Error = ex.Message };
            }
        }

        public async Task<PaginationUtility<ArticleCategory_Dto>> GetDataPagination(PaginationParam pagination, ArticleCategoryParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<ArticleCategory>(true);
            if (param.ArticleCategoryMainID > 0)
            {
                predicate = predicate.And(x => x.ArticleMainCategoryID == param.ArticleCategoryMainID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword));
            }
            var data = _repository.ArticleCategory.FindAll(predicate)
                        .Select(x => new ArticleCategory_Dto()
                        {
                            ArticleMainCategoryID = x.ArticleMainCategoryID,
                            Title = x.Title,
                            ArticleMainCategory_Title = x.ArticleMainCategory.Title,
                            ArticleCategoryID = x.ArticleCategoryID,
                            Avatar = x.Avatar,
                            Code = x.Code != null ? x.Code : "",
                            CreateBy = x.CreateBy,
                            CreateTime = x.CreateTime,
                            Description = x.Description,
                            Position = x.Position,
                            Status = x.Status,
                            Thumb = x.Thumb,
                        }).AsNoTracking();
            return await PaginationUtility<ArticleCategory_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<ArticleCategory_Dto> GetDetail(int id)
        {
            return _mapper.Map<ArticleCategory_Dto>(await _repository.ArticleCategory.FindSingle(x => x.ArticleCategoryID == id));
        }

        public async Task<List<KeyValueUtility>> GetListArticleCategory()
        {
            return await _repository.ArticleCategory
                          .FindAll(x => x.Status == true)
                          .Select(x => new KeyValueUtility()
                          {
                              Key = x.ArticleCategoryID,
                              Value = x.Title
                          }).ToListAsync();
        }

        public async Task<OperationResult> Update(ArticleCategory_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb!.Split(";");
                foreach (var item in imageOlds!)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\articleCategories\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb!)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\articleCategories\\", $"ArticleCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }


            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\articleCategories\\", $"ArticleCategory_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<ArticleCategory>(dto);
            _repository.ArticleCategory.Update(data);
            try
            {
                await _repository.SaveChangesAsync();
                return new OperationResult { IsSuccess = true };
            }
            catch (System.Exception ex)
            {
                return new OperationResult { IsSuccess = false, Error = ex.Message };
            }
        }
    }
}