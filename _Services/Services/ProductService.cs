
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
    public class ProductService : IProductService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public ProductService(
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
        public async Task<OperationResult> Create(Product_Dto dto)
        {
            List<string> images = new();
            List<string> imagesList = new();
            List<string> imagesListGallery = new();
            if (dto.ListThumb != null)
            {
                foreach (var image in dto.ListThumb)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    images.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\products\\listThumb\\", filename));
                }
                images = images.Select(img => "/uploaded/images/products/listThumb/" + img).ToList();
                dto.Thumb = string.Join(";", images);
            }
            if (dto.ListImages != null)
            {
                foreach (var image in dto.ListImages)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    imagesList.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\products\\listImages\\", filename));
                }
                imagesList = imagesList.Select(img => "/uploaded/images/products/listImages/" + img).ToList();
                dto.ImageList = string.Join(";", imagesList);
            }
            if (dto.ListGalleryImages != null)
            {
                foreach (var image in dto.ListGalleryImages)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    imagesListGallery.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\products\\listGalleryImages\\", filename));
                }
                imagesListGallery = imagesListGallery.Select(img => "/uploaded/images/products/listGalleryImages/" + img).ToList();
                dto.GalleryImageList = string.Join(";", imagesListGallery);
            }
            if (dto.FileUpload != null)
            {
                string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\products\\avatar\\", filename);
                dto.Avatar = "/uploaded/images/products/avatar/" + dto.Avatar;
            }
            else
            {
                dto.Avatar = null;
            }
            var data = _mapper.Map<Product>(dto);
            _repository.Product.Add(data);
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

        public async Task<OperationResult> Delete(Product_Dto dto)
        {
            var data = _mapper.Map<Product>(dto);
            // Xóa hết ảnh cũ có trong thư mục đã lưu
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
            if (dto.GalleryImageList != null)
            {
                var imageOlds = data.GalleryImageList.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
            }
            var listOderDetail = await _repository.OrderDetail.FindAll(x => x.ProductID == dto.ProductID).ToListAsync();
            foreach (var orderDetail in listOderDetail)
            {
                _repository.OrderDetail.Remove(orderDetail);
            }
            _repository.Product.Remove(data);
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

        public async Task<PaginationUtility<Product_Dto>> GetDataPagination(PaginationParam pagination, ProductParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            if (param.ProductCategoryID > 0)
            {
                predicate = predicate.And(x => x.ProductCategoryID == param.ProductCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword));
            }
            if (param.Price > 0)
            {
                predicate = predicate.And(x => x.Price >= 0 && x.Price <= param.Price);
            }
            var predicateSub = PredicateBuilder.New<ProductMainCategory>(true);
            if(param.ProductMainCategoryID > 0)
            {
                predicateSub = predicateSub.And(x => x.ProductMainCategoryID == param.ProductMainCategoryID);
            }
            var data = _repository.ProductMainCategory.FindAll(predicateSub)
                        .Join(_repository.ProductCategory.FindAll(),
                        x => x.ProductMainCategoryID,
                        y => y.ProductMainCategoryID,
                        (x, y) => new { ProductMainCategory = x, ProductCategory = y })
                        .Join(_repository.Product.FindAll(predicate),
                        x => x.ProductCategory.ProductCategoryID,
                        y => y.ProductCategoryID,
                        (x, y) => new { x.ProductMainCategory, x.ProductCategory, Product = y })
                        .Select(y => new Product_Dto()
                        {
                            ProductID = y.Product.ProductID,
                            Title = y.Product.Title,
                            ProductCategory_Title = y.Product.ProductCategory.Title,
                            ProductCategoryID = y.ProductCategory.ProductCategoryID,
                            Avatar = y.Product.Avatar,
                            Code = y.Product.Code,
                            CreateBy = y.Product.CreateBy,
                            CreateTime = y.Product.CreateTime,
                            Description = y.Product.Description,
                            Position = y.Product.Position,
                            Status = y.Product.Status,
                            Thumb = y.Product.Thumb,
                            Accessories = y.Product.Accessories,
                            AttachmentFile = y.Product.AttachmentFile,
                            Content = y.Product.Content,
                            GalleryImageList = y.Product.GalleryImageList,
                            Keyword = y.Product.Keyword,
                            ImageList = y.Product.ImageList,
                            OldPrice = y.Product.OldPrice,
                            Price = y.Product.Price,
                            Promotions = y.Product.Promotions,
                            Quantity = y.Product.Quantity,
                            SourceLink = y.Product.SourceLink,
                            SourcePage = y.Product.SourcePage,
                            Specifications = y.Product.Specifications,
                            ViewTime = y.Product.ViewTime,
                            WarrantyPolicy = y.Product.WarrantyPolicy,
                        }).OrderByDescending(x => x.CreateTime).AsNoTracking();
            if (param.isOrderByDescending != null)
            {
                data = param.isOrderByDescending == false ? data.OrderBy(x => x.Price) : data.OrderByDescending(x => x.Price);
            }
            return await PaginationUtility<Product_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<PaginationUtility<Product_Dto>> UserGetDataPagination(PaginationParam pagination, ProductParam param, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Product>(true);
            predicate = predicate.And(x => x.Status == true);
            if (param.ProductCategoryID > 0)
            {
                predicate = predicate.And(x => x.ProductCategoryID == param.ProductCategoryID);
            }
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                param.Keyword = param.Keyword.ToLower();
                predicate = predicate.And(x => x.Title.ToLower().Contains(param.Keyword) || x.Description.ToLower().Contains(param.Keyword));
            }
            if (param.Price > 0)
            {
                predicate = predicate.And(x => x.Price >= 0 && x.Price <= param.Price);
            }
            var data = _repository.Product.FindAll(predicate)
                        .Select(x => new Product_Dto()
                        {
                            ProductID = x.ProductID,
                            Title = x.Title,
                            ProductCategory_Title = x.ProductCategory.Title,
                            ProductCategoryID = x.ProductCategory.ProductCategoryID,
                            Avatar = x.Avatar,
                            Code = x.Code,
                            CreateBy = x.CreateBy,
                            CreateTime = x.CreateTime,
                            Description = x.Description,
                            Position = x.Position,
                            Status = x.Status,
                            Thumb = x.Thumb,
                            Accessories = x.Accessories,
                            AttachmentFile = x.AttachmentFile,
                            Content = x.Content,
                            GalleryImageList = x.GalleryImageList,
                            Keyword = x.Keyword,
                            ImageList = x.ImageList,
                            OldPrice = x.OldPrice,
                            Price = x.Price,
                            Promotions = x.Promotions,
                            Quantity = x.Quantity,
                            SourceLink = x.SourceLink,
                            SourcePage = x.SourcePage,
                            Specifications = x.Specifications,
                            ViewTime = x.ViewTime,
                            WarrantyPolicy = x.WarrantyPolicy,
                        }).OrderByDescending(x => x.CreateTime).AsNoTracking();
            if (param.isOrderByDescending != null)
            {
                data = param.isOrderByDescending == false ? data.OrderBy(x => x.Price) : data.OrderByDescending(x => x.Price);
            }


            return await PaginationUtility<Product_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }
        public async Task<List<Product_Dto>> GetDataByMainCategory(int? mainCateID, int? quantity)
        {
            var predicate = PredicateBuilder.New<ProductMainCategory>(true);
            predicate = predicate.And(x => x.Status == true);
            if (mainCateID > 0)
            {
                predicate = predicate.And(x => x.ProductMainCategoryID == mainCateID);
            }
            quantity = quantity ?? 52;
            var data = await _repository.ProductMainCategory.FindAll(predicate)
                        .Join(_repository.ProductCategory.FindAll(),
                        x => x.ProductMainCategoryID,
                        y => y.ProductMainCategoryID,
                        (x, y) => new { ProductMainCategory = x, ProductCategory = y })
                        .Join(_repository.Product.FindAll(),
                        x => x.ProductCategory.ProductCategoryID,
                        y => y.ProductCategoryID,
                        (x, y) => new { x.ProductMainCategory, x.ProductCategory, Product = y })
                        .Select(x => new Product_Dto()
                        {
                            ProductID = x.Product.ProductID,
                            Title = x.Product.Title,
                            ProductCategory_Title = x.ProductCategory.Title,
                            ProductCategoryID = x.ProductCategory.ProductMainCategoryID,
                            Avatar = x.Product.Avatar,
                            Code = x.Product.Code,
                            CreateBy = x.Product.CreateBy,
                            CreateTime = x.Product.CreateTime,
                            Description = x.Product.Description,
                            Position = x.Product.Position,
                            Status = x.Product.Status,
                            Thumb = x.Product.Thumb,
                            Accessories = x.Product.Accessories,
                            AttachmentFile = x.Product.AttachmentFile,
                            Content = x.Product.Content,
                            GalleryImageList = x.Product.GalleryImageList,
                            Keyword = x.Product.Keyword,
                            ImageList = x.Product.ImageList,
                            OldPrice = x.Product.OldPrice,
                            Price = x.Product.Price,
                            Promotions = x.Product.Promotions,
                            Quantity = x.Product.Quantity,
                            SourceLink = x.Product.SourceLink,
                            SourcePage = x.Product.SourcePage,
                            Specifications = x.Product.Specifications,
                            ViewTime = x.Product.ViewTime,
                            WarrantyPolicy = x.Product.WarrantyPolicy,
                            ProductMainCategoryID = x.ProductMainCategory.ProductMainCategoryID
                        }).Take(quantity.ToInt()).ToListAsync();
            return data;
        }

        public async Task<Product_Dto> GetDetail(int id)
        {
            var data = await _repository.ProductMainCategory.FindAll()
                      .Join(_repository.ProductCategory.FindAll(),
                      x => x.ProductMainCategoryID,
                      y => y.ProductMainCategoryID,
                      (x, y) => new { ProductMainCategory = x, ProductCategory = y })
                      .Join(_repository.Product.FindAll(x => x.ProductID == id),
                      x => x.ProductCategory.ProductCategoryID,
                      y => y.ProductCategoryID,
                      (x, y) => new { x.ProductMainCategory, x.ProductCategory, Product = y })
                      .Select(x => new Product_Dto()
                      {
                          ProductID = x.Product.ProductID,
                          Title = x.Product.Title,
                          ProductCategory_Title = x.ProductCategory.Title,
                          ProductCategoryID = x.ProductCategory.ProductCategoryID,
                          Avatar = x.Product.Avatar,
                          Code = x.Product.Code,
                          CreateBy = x.Product.CreateBy,
                          CreateTime = x.Product.CreateTime,
                          Description = x.Product.Description,
                          Position = x.Product.Position,
                          Status = x.Product.Status,
                          Thumb = x.Product.Thumb,
                          Accessories = x.Product.Accessories,
                          AttachmentFile = x.Product.AttachmentFile,
                          Content = x.Product.Content,
                          GalleryImageList = x.Product.GalleryImageList,
                          Keyword = x.Product.Keyword,
                          ImageList = x.Product.ImageList,
                          OldPrice = x.Product.OldPrice,
                          Price = x.Product.Price,
                          Promotions = x.Product.Promotions,
                          Quantity = x.Product.Quantity,
                          SourceLink = x.Product.SourceLink,
                          SourcePage = x.Product.SourcePage,
                          Specifications = x.Product.Specifications,
                          ViewTime = x.Product.ViewTime,
                          WarrantyPolicy = x.Product.WarrantyPolicy,
                          ProductMainCategoryID = x.ProductMainCategory.ProductMainCategoryID
                      }).FirstOrDefaultAsync();
            return data;
        }

        public PaginationUtility<Product_Dto> TopProducts(PaginationParam pagination, ProductParam param, bool isPaging = true)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Update(Product_Dto dto)
        {
            if (dto.ListThumb != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.Thumb.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> images = new();
                foreach (var image in dto.ListThumb)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    string filepath = await _functionUtility.UploadAsync(image, "uploaded/images/products/listThumb/", filename);
                }
                images = images.Select(img => "/uploaded/images/products/listThumb/" + img).ToList();
                dto.Thumb = string.Join(";", images);
            }
            if (dto.ListImages != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.ImageList.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> imagesList = new();
                foreach (var image in dto.ListImages)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    imagesList.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\products\\listImages\\", filename));
                }
                imagesList = imagesList.Select(img => "/uploaded/images/products/listImages/" + img).ToList();
                dto.ImageList = string.Join(";", imagesList);
            }
            if (dto.ListGalleryImages != null)
            {
                // Xóa hết ảnh cũ có trong thư mục đã lưu
                var imageOlds = dto.GalleryImageList.Split(";");
                foreach (var item in imageOlds)
                {
                    _functionUtility.DeleteFile($"{item}");
                }
                // Thêm ảnh mới vào thư mục
                List<string> imagesListGallery = new();
                foreach (var image in dto.ListGalleryImages)
                {
                    string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                    string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                    imagesListGallery.Add(await _functionUtility.UploadAsync(image, "uploaded\\images\\products\\listGalleryImages\\", filename));
                }
                imagesListGallery = imagesListGallery.Select(img => "/uploaded/images/products/listGalleryImages/" + img).ToList();
                dto.GalleryImageList = string.Join(";", imagesListGallery);
            }
            if (dto.FileUpload != null)
            {
                _functionUtility.DeleteFile($"{dto.Avatar}");
                string uniqueId = RandomNumber.GenerateUniqueIdentifier();
                string filename = $"{ConvertUtility.ConvertStringToUnsignLetter(dto.Title)}_{uniqueId}";
                dto.Avatar = await _functionUtility.UploadAsync(dto.FileUpload, "uploaded\\images\\products\\avatar\\", filename);
                dto.Avatar = "/uploaded/images/products/avatar/" + dto.Avatar;
            }
            var data = _mapper.Map<Product>(dto);
            _repository.Product.Update(data);
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

        public async Task<List<Product_Dto>> GetDataNewProduct()
        {
            var data = await _repository.Product.FindAll(x => x.Status == true)
                       .Select(x => new Product_Dto()
                       {
                           ProductID = x.ProductID,
                           Title = x.Title,
                           ProductCategory_Title = x.ProductCategory.Title,
                           ProductCategoryID = x.ProductCategory.ProductCategoryID,
                           Avatar = x.Avatar,
                           Code = x.Code,
                           CreateBy = x.CreateBy,
                           CreateTime = x.CreateTime,
                           Description = x.Description,
                           Position = x.Position,
                           Status = x.Status,
                           Thumb = x.Thumb,
                           Accessories = x.Accessories,
                           AttachmentFile = x.AttachmentFile,
                           Content = x.Content,
                           GalleryImageList = x.GalleryImageList,
                           Keyword = x.Keyword,
                           ImageList = x.ImageList,
                           OldPrice = x.OldPrice,
                           Price = x.Price,
                           Promotions = x.Promotions,
                           Quantity = x.Quantity,
                           SourceLink = x.SourceLink,
                           SourcePage = x.SourcePage,
                           Specifications = x.Specifications,
                           ViewTime = x.ViewTime,
                           WarrantyPolicy = x.WarrantyPolicy,
                       }).OrderByDescending(x => x.CreateTime).Take(20).ToListAsync();
            return data;
        }

        public async Task<List<Product_Dto>> Top20ViewProduct()
        {
            var data = await _repository.Product.FindAll(x => x.Status == true)
                       .Select(x => new Product_Dto()
                       {
                           ProductID = x.ProductID,
                           Title = x.Title,
                           ProductCategory_Title = x.ProductCategory.Title,
                           ProductCategoryID = x.ProductCategory.ProductCategoryID,
                           Avatar = x.Avatar,
                           Code = x.Code,
                           CreateBy = x.CreateBy,
                           CreateTime = x.CreateTime,
                           Description = x.Description,
                           Position = x.Position,
                           Status = x.Status,
                           Thumb = x.Thumb,
                           Accessories = x.Accessories,
                           AttachmentFile = x.AttachmentFile,
                           Content = x.Content,
                           GalleryImageList = x.GalleryImageList,
                           Keyword = x.Keyword,
                           ImageList = x.ImageList,
                           OldPrice = x.OldPrice,
                           Price = x.Price,
                           Promotions = x.Promotions,
                           Quantity = x.Quantity,
                           SourceLink = x.SourceLink,
                           SourcePage = x.SourcePage,
                           Specifications = x.Specifications,
                           ViewTime = x.ViewTime,
                           WarrantyPolicy = x.WarrantyPolicy,
                       }).OrderByDescending(x => x.ViewTime).Take(20).ToListAsync();
            return data;
        }

         public async Task<List<Product_Dto>> Top20ViewProduct1()
        {
            var data = await _repository.Product.FindAll(x => x.Status == true)
                       .Select(x => new Product_Dto()
                       {
                           ProductID = x.ProductID,
                           Title = x.Title,
                           ProductCategory_Title = x.ProductCategory.Title,
                           ProductCategoryID = x.ProductCategory.ProductCategoryID,
                           Avatar = x.Avatar,
                           Code = x.Code,
                           CreateBy = x.CreateBy,
                           CreateTime = x.CreateTime,
                           Description = x.Description,
                           Position = x.Position,
                           Status = x.Status,
                           Thumb = x.Thumb,
                           Accessories = x.Accessories,
                           AttachmentFile = x.AttachmentFile,
                           Content = x.Content,
                           GalleryImageList = x.GalleryImageList,
                           Keyword = x.Keyword,
                           ImageList = x.ImageList,
                           OldPrice = x.OldPrice,
                           Price = x.Price,
                           Promotions = x.Promotions,
                           Quantity = x.Quantity,
                           SourceLink = x.SourceLink,
                           SourcePage = x.SourcePage,
                           Specifications = x.Specifications,
                           ViewTime = x.ViewTime,
                           WarrantyPolicy = x.WarrantyPolicy,
                       }).OrderByDescending(x => x.ViewTime).Take(20).ToListAsync();
            return data;
        }
    }
}