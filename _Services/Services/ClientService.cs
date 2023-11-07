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
    public class ClientService : IClientService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ClientService(
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
        public async Task<OperationResult> Create(Client_Dto dto)
        {
            //check email đã tồn tại
            if (_mapper.Map<Client_Dto>(await _repository.Client.FindSingle((x => x.Email == dto.Email))) != null)
            {
                return new OperationResult { IsSuccess = false, Error = "Email đã tồn tại!" };
            }
            List<string> images = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\clients\\", $"Client_Thumb_{Guid.NewGuid().ToString()}"));
                }
                dto.Thumb = string.Join(";", images);
            }
            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\clients\\", $"Client_{Guid.NewGuid().ToString()}");
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<Client>(dto);
            _repository.Client.Add(data);
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

        public async Task<OperationResult> Delete(Client_Dto dto)
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

        public async Task<PaginationUtility<Client_Dto>> GetDataPagination(PaginationParam pagination, ClientParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Client>(true);
            if (param.ClientCategoryID != null)
            {
                param.ClientCategoryID = param.ClientCategoryID;
                predicate = predicate.And(x => x.ClientCategoryID == param.ClientCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Email.ToLower().Contains(param.Keyword) ||
                                      x.Mobi.ToLower().Contains(param.Keyword) || x.Address.ToLower().Contains(param.Keyword));
            }

            var data = _repository.Client.FindAll(predicate)
                     .Select(x => new Client_Dto()
                     {
                         ClientID = x.ClientID,
                         Avatar = x.Avatar,
                         Email = x.Email,
                         Address = x.Address,
                         ClientCategoryID = x.ClientCategoryID,
                         CreateTime = x.CreateTime,
                         FullName = x.FullName,
                         Gender = x.Gender,
                         Mobi = x.Mobi,
                         Thumb = x.Thumb,
                         Status = x.Status,
                         ClientCategory_Title = x.ClientCategory.Title
                     }).AsNoTracking();
            return await PaginationUtility<Client_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<Client_Dto> GetDetail(string email)
        {
            return _mapper.Map<Client_Dto>(await _repository.Client.FindSingle((x => x.Email == email)));
        }

        public async Task<OperationResult> Update(ClientSercure_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"uploaded\\images\\clients\\{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\clients\\", $"Client_Thumb_{Guid.NewGuid().ToString()}"));
                }
                if (images.Any())
                    dto.Thumb = string.Join(";", images);
            }

            if (dto.FileUpload != null)
            {
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\clients\\", $"Client_{Guid.NewGuid().ToString()}");
            }

            var data = _mapper.Map<Client>(dto);
            _repository.Client.Update(data);
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