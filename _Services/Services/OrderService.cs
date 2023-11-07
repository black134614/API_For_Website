
using API._Repositories;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Utilities;
using API.Models;
using AutoMapper;
using LinqKit;
using Microsoft.EntityFrameworkCore;

namespace API._Services.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepositoryAccessor _repository;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;
        private readonly IFunctionUtility _functionUtility;

        public OrderService(
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
        public async Task<OperationResult> Create(Order_Dto dto)
        {
            var data = _mapper.Map<Order>(dto);
            _repository.Order.Add(data);
            await _repository.SaveChangesAsync();
            foreach (var item in dto.ListProducts)
            {
                OrderDetail orderDetail = new()
                {
                    OrderID = data.OrderID,
                    ProductID = item.ProductID,
                    Price = item.Price,
                    Quantity = item.Quantity
                };
                _repository.OrderDetail.Add(orderDetail);
            }
            try
            {
                await _repository.SaveChangesAsync();
                var orderDto = _mapper.Map<Order_Dto>(data);
                return new OperationResult { IsSuccess = true, Data = orderDto };
            }
            catch (System.Exception ex)
            {
                return new OperationResult { IsSuccess = false, Error = ex.Message };
            }
        }

        public async Task<OperationResult> Delete(Order_Dto dto)
        {
            if (dto.OrderID != null)
            {
                var listOderDetail = await _repository.OrderDetail.FindAll(x => x.OrderID == dto.OrderID).ToListAsync();
                foreach (var orderDetail in listOderDetail)
                {
                    _repository.OrderDetail.Remove(orderDetail);
                }
            }
            var data = await _repository.Order.FindAll(x => x.OrderID == dto.OrderID).FirstOrDefaultAsync();
            _repository.Order.Remove(data);
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

        public async Task<PaginationUtility<Order_Dto>> GetDataPagination(PaginationParam pagination, string keyword, bool isPaging = true)
        {
            var predicate = PredicateBuilder.New<Order>(true);

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                predicate = predicate.And(x => x.Email.ToLower().Contains(keyword) || x.Mobi.ToLower().Contains(keyword) ||
                                     x.FullName.ToLower().Contains(keyword));
            }
            var data = _repository.Order.FindAll(predicate).OrderByDescending(x => x.CreateTime)
                        .Select(x => new Order_Dto()
                        {
                            Email = x.Email,
                            Address = x.Address,
                            Code = x.Code,
                            ClientID = x.ClientID,
                            Comment = x.Comment,
                            CreateTime = x.CreateTime,
                            FullName = x.FullName,
                            ChargeStatus = x.ChargeStatus,
                            DeliverStatus = x.DeliverStatus,
                            Gender = x.Gender,
                            Mobi = x.Mobi,
                            OrderID = x.OrderID,
                            Total = x.Total,
                            OrderStatus = x.OrderStatus,
                            StaffID = x.StaffID,
                            PaymentMethod = x.PaymentMethod,
                        }).AsNoTracking();
            return await PaginationUtility<Order_Dto>.CreateAsync(data, pagination.PageNumber, pagination.PageSize, isPaging);
        }

        public async Task<List<OrderDetail_Dto>> GetDetail(int id)
        {
            return await _repository.OrderDetail.FindAll(x => x.OrderID == id).Select(x => new OrderDetail_Dto()
            {
                ProductName = x.Product.Title,
                OrderID = x.OrderID,
                Price = x.Price,
                ProductID = x.ProductID,
                Quantity = x.Quantity
            }).ToListAsync();

        }

        public async Task<List<Order_Dto>> GetOderNotConfirm()
        {
            return await _repository.Order.FindAll(x => x.ChargeStatus != true && x.Email != "admin@gmail.com")
                        .Select(x => new Order_Dto()
                        {
                            Email = x.Email,
                            Address = x.Address,
                            Code = x.Code,
                            ClientID = x.ClientID,
                            Comment = x.Comment,
                            CreateTime = x.CreateTime,
                            FullName = x.FullName,
                            ChargeStatus = x.ChargeStatus,
                            DeliverStatus = x.DeliverStatus,
                            Gender = x.Gender,
                            Mobi = x.Mobi,
                            OrderID = x.OrderID,
                            Total = x.Total,
                            OrderStatus = x.OrderStatus,
                            StaffID = x.StaffID,
                            PaymentMethod = x.PaymentMethod,
                        }).OrderByDescending(x => x.CreateTime).ToListAsync();
        }

        public async Task<bool> MinusProduct(int productID)
        {

            var listProductOrders = await _repository.OrderDetail.FindAll(x => x.OrderID == productID).Select(x => new OrderDetail_Dto()
            {
                OrderID = x.OrderID,
                Price = x.Price,
                ProductID = x.ProductID,
                Quantity = x.Quantity
            }).ToListAsync();

            foreach (var item in listProductOrders)
            {
                var product = await _repository.Product.FindAll(x => x.ProductID == item.ProductID).FirstOrDefaultAsync();
                if (product != null)
                {
                    var newProduct = new Product()
                    {
                        Quantity = product.Quantity - item.Quantity
                    };
                    _repository.Product.Update(newProduct);
                    await _repository.SaveChangesAsync();
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        public async Task<OperationResult> Update(Order_Dto dto)
        {
            var data = await _repository.Order.FindAll(x => x.OrderID == dto.OrderID).FirstOrDefaultAsync();
            _repository.Order.Update(data);
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
        public async Task<OperationResult> UpdateStatus(OrderStatus_Dto dto)
        {
            var item = await _repository.Order.FindSingle(x => x.OrderID == dto.OrderID);
            if (item == null)
            {
                return new OperationResult(false);
            }
            item.OrderStatus = dto.OrderStatus;
            item.DeliverStatus = dto.DeliverStatus;
            item.ChargeStatus = dto.ChargeStatus;
            _repository.Order.Update(item);
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

        public async Task<OperationResult> GetOrderImport()
        {
            var data = await _repository.Order.FindAll(x => x.Email == "admin@gmail.com")
            .Join(_repository.OrderDetail.FindAll(),
            x => x.OrderID,
            y => y.OrderID,
            (x, y) => new { Order = x, OrderDetail = y })
            .Join(_repository.Product.FindAll(),
            x => x.OrderDetail.ProductID,
            y => y.ProductID,
            (x, y) => new { x.OrderDetail, x.Order, Product = y })
            .Select(x => new OrderImport_Dto()
            {
                OrderID = x.OrderDetail.Order.OrderID,
                ProductID = x.Product.ProductID,
                Quantity = x.OrderDetail.Quantity,
                Price = x.OrderDetail.Price,
                ProductName = x.Product.Title,
                CreateTime = x.Order.CreateTime
            }).ToListAsync();
            return new OperationResult { IsSuccess = true, Data = data };
        }

        
    }
}