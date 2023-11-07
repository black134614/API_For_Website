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
    public class ProductMainCategoryService : IProductMainCategoryService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ProductMainCategoryService(
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
        public async Task<OperationResult> Create(ProductMainCategory_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\productMainCategories\\", $"ProductMainCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\productMainCategories\\", $"ProductMainCategory_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }

            var data = _mapper.Map<ProductMainCategory>(dto);
            _repository.ProductMainCategory.Add(data);
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

        public async Task<OperationResult> Delete(ProductMainCategory_Dto dto)
        {
            var data = _mapper.Map<ProductMainCategory>(dto);
            var listProductCategory = await _repository.ProductCategory.FindAll(x => x.ProductMainCategoryID == dto.ProductMainCategoryID).ToListAsync();
            foreach (var productCategory in listProductCategory)
            {
                var listProduct = await _repository.Product
                    .FindAll(x => x.ProductCategoryID == productCategory.ProductCategoryID)
                    .ToListAsync();
                foreach (var product in listProduct)
                {
                    _repository.Product.Remove(product);
                }
                _repository.ProductCategory.Remove(productCategory);
            }
            _repository.ProductMainCategory.Remove(data);
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

        public async Task<PaginationUtility<ProductMainCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<ProductMainCategory>(true);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(keyword));
            }

            var data = _repository.ProductMainCategory.FindAll(predicate).ProjectTo<ProductMainCategory_Dto>(_mapperConfiguration).AsNoTracking();
            return await PaginationUtility<ProductMainCategory_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<ProductMainCategory_Dto> GetDetail(int id)
        {
            return _mapper.Map<ProductMainCategory_Dto>(await _repository.ProductMainCategory.FindSingle((x => x.ProductMainCategoryID == id)));

        }

        public async Task<List<KeyValueUtility>> GetListProductMainCategory()
        {
            return await _repository.ProductMainCategory
                         .FindAll(x => x.Status == true)
                         .Select(x => new KeyValueUtility()
                         {
                             Key = x.ProductMainCategoryID,
                             Value = x.Title
                         }).ToListAsync();
        }

        public async Task<OperationResult> Update(ProductMainCategory_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\productMainCategories\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\productMainCategories\\", $"ProductMainCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\productMainCategories\\", $"ProductMainCategory_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<ProductMainCategory>(dto);
            _repository.ProductMainCategory.Update(data);
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