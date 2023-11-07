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
    public class AccountService : IAccountService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public AccountService(
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
        public async Task<OperationResult> Create(Account_Dto dto)
        {
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\accounts\\", $"Account_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\accounts\\", $"Account_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<Account>(dto);
            _repository.Account.Add(data);
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

        public async Task<OperationResult> Delete(Account_Dto dto)
        {
            var data = _mapper.Map<Account>(dto);
            _repository.Account.Remove(data);
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

        public async Task<PaginationUtility<Account_Dto>> GetDataPagination(PaginationParam pagination, AccountParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Account>(true);
            if (!string.IsNullOrWhiteSpace(param.AccountCategoryID))
            {
                param.AccountCategoryID = param.AccountCategoryID.ToLower();
                predicate = predicate.And(x => x.AccountCategoryID.ToLower().Contains(param.AccountCategoryID));
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Username.ToLower().Contains(param.Keyword) || x.Email.ToLower().Contains(param.Keyword) ||
                                      x.Mobi.ToLower().Contains(param.Keyword) || x.Address.ToLower().Contains(param.Keyword));
            }

            var data = _repository.Account.FindAll(predicate)
                     .Select(x => new Account_Dto()
                     {
                         Username = x.Username,
                         Avatar = x.Avatar,
                         Email = x.Email,
                         Address = x.Address,
                         AccountCategoryID = x.AccountCategoryID,
                         CreateTime = x.CreateTime,
                         FullName = x.FullName,
                         Gender = x.Gender,
                         Mobi = x.Mobi,
                         Password = x.Password,
                         Thumb = x.Thumb,
                         Status = x.Status,
                         AccountCategory_Title = x.AccountCategory.Title
                     }).AsNoTracking();
            return await PaginationUtility<Account_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<Account_Dto> GetDetail(string username)
        {
            return _mapper.Map<Account_Dto>(await _repository.Account.FindSingle((x => x.Username == username)));
        }

        public async Task<OperationResult> Update(Account_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\accounts\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\accounts\\", $"Account_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\accounts\\", $"Account_{Guid.NewGuid().ToString()}");
            }


            var data = _mapper.Map<Account>(dto);
            _repository.Account.Update(data);
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