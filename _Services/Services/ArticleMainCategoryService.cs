
using API._Repositories;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Utilities;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class ArticleMainCategoryService : IArticleMainCategoryService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ArticleMainCategoryService(
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
        public async Task<OperationResult> Create(ArticleMainCategory_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\articleMainCategories\\", $"ArticleMainCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\articleMainCategories\\", $"ArticleMainCategory_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<ArticleMainCategory>(dto);
            _repository.ArticleMainCategory.Add(data);
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

        public async Task<OperationResult> Delete(ArticleMainCategory_Dto dto)
        {
            var data = _mapper.Map<ArticleMainCategory>(dto);
            _repository.ArticleMainCategory.Remove(data);
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

        public async Task<PaginationUtility<ArticleMainCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<ArticleMainCategory>(true);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(keyword));
            }

            var data = _repository.ArticleMainCategory.FindAll(predicate).ProjectTo<ArticleMainCategory_Dto>(_mapperConfiguration).AsNoTracking();
            return await PaginationUtility<ArticleMainCategory_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<ArticleMainCategory_Dto> GetDetail(int id)
        {
            return _mapper.Map<ArticleMainCategory_Dto>(await _repository.ArticleMainCategory.FindSingle(x => x.ArticleMainCategoryID == id));

        }

        public async Task<List<KeyValueUtility>> GetListArticleMainCategory()
        {
            return await _repository.ArticleMainCategory
                         .FindAll(x => x.Status == true)
                         .Select(x => new KeyValueUtility()
                         {
                             Key = x.ArticleMainCategoryID,
                             Value = x.Title
                         }).ToListAsync();
        }

        public async Task<OperationResult> Update(ArticleMainCategory_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb!.Split(";");
                foreach (var item in imageOlds!)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\articleMainCategories\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb!)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\articleMainCategories\\", $"ArticleMainCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\articleMainCategories\\", $"ArticleMainCategory_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<ArticleMainCategory>(dto);
            _repository.ArticleMainCategory.Update(data);
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