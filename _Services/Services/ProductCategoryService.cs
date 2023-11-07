
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
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ProductCategoryService(
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
        public async Task<OperationResult> Create(ProductCategory_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\productCategories\\", $"ProductCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\productCategories\\", $"ProductCategory_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<ProductCategory>(dto);
            _repository.ProductCategory.Add(data);
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

        public async Task<OperationResult> Delete(ProductCategory_Dto dto)
        {
            var data = _mapper.Map<ProductCategory>(dto);
            var listProduct = await _repository.Product.FindAll(x => x.ProductCategoryID == dto.ProductCategoryID).ToListAsync();
            foreach (var product in listProduct)
            {
                _repository.Product.Remove(product);
            }
            _repository.ProductCategory.Remove(data);
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

        public async Task<PaginationUtility<ProductCategory_Dto>> GetDataPagination(PaginationParam pagination, ProductCategoryParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<ProductCategory>(true);
            if (param.ProductMainCategoryID > 0)
            {
                predicate = predicate.And(x => x.ProductMainCategoryID == param.ProductMainCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword));
            }
            var data = _repository.ProductCategory.FindAll(predicate)
                        .Select(x => new ProductCategory_Dto()
                        {
                            ProductCategoryID = x.ProductCategoryID,
                            Title = x.Title,
                            ProductMainCategory_Title = x.ProductMainCategory.Title,
                            ProductMainCategoryID = x.ProductMainCategory.ProductMainCategoryID,
                            Avatar = x.Avatar,
                            Code = x.Code,
                            CreateBy = x.CreateBy,
                            CreateTime = x.CreateTime,
                            Description = x.Description,
                            Position = x.Position,
                            Status = x.Status,
                            Thumb = x.Thumb
                        }).AsNoTracking();
            return await PaginationUtility<ProductCategory_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<ProductCategory_Dto> GetDetail(int id)
        {
            var predicate = PredicateBuilder.New<ProductCategory>(true);
            if(id > 0)
            {
                predicate = predicate.And(x => x.ProductCategoryID == id);
            }
            var data = await _repository.ProductMainCategory.FindAll()
                        .Join(_repository.ProductCategory.FindAll(predicate),
                        x => x.ProductMainCategoryID,
                        y => y.ProductMainCategoryID,
                        (x, y) => new { ProductMainCategory = x, ProductCategory = y })
                        .Select(x => new ProductCategory_Dto()
                        {
                            Avatar = x.ProductCategory.Avatar,
                            Code = x.ProductCategory.Code,
                            CreateBy = x.ProductCategory.CreateBy,
                            CreateTime = x.ProductCategory.CreateTime,
                            Description = x.ProductCategory.Description,
                            Position = x.ProductCategory.Position,
                            ProductCategoryID = x.ProductCategory.ProductCategoryID,
                            ProductMainCategoryID = x.ProductCategory.ProductMainCategoryID,
                            ProductMainCategory_Title = x.ProductMainCategory.Title,
                            Status = x.ProductCategory.Status,
                            Title = x.ProductCategory.Title,
                            Thumb = x.ProductCategory.Thumb
                        }).FirstOrDefaultAsync();
            return data;
        }

        public async Task<List<KeyValueUtility>> GetListProductCategory()
        {
            return await _repository.ProductCategory
                          .FindAll(x => x.Status == true)
                          .Select(x => new KeyValueUtility()
                          {
                              Subkey = x.ProductMainCategoryID,
                              Key = x.ProductCategoryID,
                              Value = x.Title
                          }).ToListAsync();
        }

        public async Task<OperationResult> Update(ProductCategory_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\productCategories\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\productCategories\\", $"ProductCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\productCategories\\", $"ProductCategory_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<ProductCategory>(dto);
            _repository.ProductCategory.Update(data);
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