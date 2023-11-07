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
    public class AccountCategoryService : IAccountCategoryService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public AccountCategoryService(
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
        public async Task<OperationResult> Create(AccountCategory_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\accountCategories\\", $"AccountCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\accountCategories\\", $"AccountCategory_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<AccountCategory>(dto);
            _repository.AccountCategory.Add(data);
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

        public async Task<OperationResult> Delete(AccountCategory_Dto dto)
        {
            var data = _mapper.Map<AccountCategory>(dto);
            _repository.AccountCategory.Remove(data);
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

        public async Task<PaginationUtility<AccountCategory_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<AccountCategory>(true);
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(keyword));
            }

            var data = _repository.AccountCategory.FindAll(predicate).ProjectTo<AccountCategory_Dto>(_mapperConfiguration).AsNoTracking();
            return await PaginationUtility<AccountCategory_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<AccountCategory_Dto> GetDetail(string id)
        {
            return _mapper.Map<AccountCategory_Dto>(await _repository.AccountCategory.FindSingle((x => x.AccountCategoryID == id.ToString())));
        }

        public async Task<List<AccountCategory_Dto>> GetListAccountCategory()
        {
            return await _repository.AccountCategory
                          .FindAll(x => x.Status == true)
                          .Select(x => new AccountCategory_Dto()
                          {
                              AccountCategoryID = x.AccountCategoryID,
                              Title = x.Title
                          }).ToListAsync();
        }

        public async Task<OperationResult> Update(AccountCategory_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\accountCategories\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\accountCategories\\", $"AccountCategory_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\accountCategories\\", $"AccountCategory_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<AccountCategory>(dto);
            _repository.AccountCategory.Update(data);
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