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
    public class ArticleService : IArticleService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ArticleService(
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
        public async Task<OperationResult> Create(Article_Dto dto)
        {
            List<string> images = new();
            List<string> listImages = new();

            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\Article\\ListThumb\\", $"Article_Thumb_{Guid.NewGuid().ToString()}"));
                }
                images = images.Select(img => "/uploaded/images/Article/listThumb/" + img).ToList();
                dto.Thumb = string.Join(";", images);
            }

            if (dto.ListImages != null)
            {
                foreach (var image in dto.ListImages)
                {
                    listImages.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\Article\\ImageList\\", $"Article_List_{Guid.NewGuid().ToString()}"));
                }
                listImages = listImages.Select(img => "/uploaded/images/Article/ImageList/" + img).ToList();
                dto.ImageList = string.Join(";", listImages);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\Article\\Avatar\\", $"Article_{Guid.NewGuid().ToString()}");
                dto.Avatar = "/uploaded/images/Article/Avatar/" + dto.Avatar;
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<Article>(dto);
            _repository.Article.Add(data);
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

        public async Task<OperationResult> Delete(Article_Dto dto)
        {

            var data = _mapper.Map<Article>(dto);
            if (dto.Avatar != null)
            {
                _functionUtility.DeleteFile($"{data.Avatar}");
            }
            if (dto.ImageList != null)
            {
                var imageOlds = data.ImageList.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
            }
            if (dto.Thumb != null)
            {
                var imageOlds = data.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
            }
            _repository.Article.Remove(data);
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

        public async Task<PaginationUtility<Article_Dto>> GetDataPagination(PaginationParam pagination, ArticleParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Article>(true);
            if (param.ArticleCategoryID > 0)
            {
                predicate = predicate.And(x => x.ArticleCategoryID == param.ArticleCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword) ||
                                     x.Keyword.ToLower().Contains(param.Keyword));
            }
            var data = _repository.Article.FindAll(predicate)
                        .Select(x => new Article_Dto()
                        {
                            ArticleID = x.ArticleID,
                            Title = x.Title,
                            ArticleCategory_Title = x.ArticleCategory.Title,
                            ArticleCategoryID = x.ArticleCategory.ArticleCategoryID,
                            AttachmentFile = x.AttachmentFile != null ? x.AttachmentFile : "",
                            Avatar = x.Avatar,
                            Code = x.Code != null ? x.Code : "",
                            Content = x.Content,
                            CreateBy = x.CreateBy,
                            CreateTime = x.CreateTime,
                            Description = x.Description,
                            ImageList = x.ImageList,
                            Keyword = x.Keyword,
                            Position = x.Position,
                            SourceLink = x.SourceLink,
                            SourcePage = x.SourcePage,
                            Status = x.Status,
                            Thumb = x.Thumb,
                            ViewTime = x.ViewTime
                        }).AsNoTracking();
            return await PaginationUtility<Article_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<List<Article_Dto>> GetDataPaginationNotPagination(ArticleParam param)
        {
            var predicate = PredicateBuilder.New<Article>(true);
            if (param.ArticleCategoryID > 0)
            {
                predicate = predicate.And(x => x.ArticleCategoryID == param.ArticleCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword) ||
                                     x.Keyword.ToLower().Contains(param.Keyword) || x.Content.ToLower().Contains(param.Keyword));
            }
            var data = await _repository.Article.FindAll(predicate)
                        .Join(_repository.ArticleCategory.FindAll(),
                        x => x.ArticleCategoryID,
                        y => y.ArticleCategoryID,
                        (x, y) => new { Article = x, ArticleCategory = y })
                        .Select(x => new Article_Dto()
                        {
                            ArticleID = x.Article.ArticleID,
                            Title = x.Article.Title,
                            ArticleCategory_Title = x.ArticleCategory.Title,
                            ArticleCategoryID = x.ArticleCategory.ArticleCategoryID,
                            AttachmentFile = x.Article.AttachmentFile != null ? x.Article.AttachmentFile : "",
                            Avatar = x.Article.Avatar,
                            Code = x.Article.Code != null ? x.Article.Code : "",
                            Content = x.Article.Content,
                            CreateBy = x.Article.CreateBy,
                            CreateTime = x.Article.CreateTime,
                            Description = x.Article.Description,
                            ImageList = x.Article.ImageList,
                            Keyword = x.Article.Keyword,
                            Position = x.Article.Position,
                            SourceLink = x.Article.SourceLink,
                            SourcePage = x.Article.SourcePage,
                            Status = x.Article.Status,
                            Thumb = x.Article.Thumb,
                            ViewTime = x.Article.ViewTime
                        }).ToListAsync();
            return data;
        }

        public async Task<Article_Dto> GetDetail(int id)
        {
            return _mapper.Map<Article_Dto>(await _repository.Article.FindSingle((x => x.ArticleID == id)));
        }

        public async Task<OperationResult> Update(Article_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb!.Split(";");
                foreach (var item in imageOlds!)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb!)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\Article\\ListThumb\\", $"Article_Thumb_{Guid.NewGuid().ToString()}"));
                }
                images = images.Select(img => "/uploaded/images/Article/listThumb/" + img).ToList();
                dto.Thumb = string.Join(";", images);
            }

            if (dto.ListImages != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.ImageList!.Split(";");
                foreach (var item in imageOlds!)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> listImages = new();
                foreach (var image in dto.ListImages!)
                {
                    listImages.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\Article\\ImageList\\", $"Article_List_{Guid.NewGuid().ToString()}"));
                }
                listImages = listImages.Select(img => "/uploaded/images/Article/ImageList/" + img).ToList();
                dto.ImageList = string.Join(";", listImages);
            }

            if (dto.FileUpload != null)
            {
                _functionUtility.DeleteFile($"{dto.Avatar}");
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\Article\\Avatar\\", $"Article_{Guid.NewGuid().ToString()}");
                dto.Avatar = "/uploaded/images/Article/Avatar/" + dto.Avatar;
            }


            var data = _mapper.Map<Article>(dto);
            _repository.Article.Update(data);
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