using System.Security.Claims;
using API._Services.Interfaces;
using API.Dtos;
using API.Helpers.Enums;
using API.Helpers.SignalRConfig;
using API.Helpers.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IHubContext<NotificationHub> _notificationHubContext;
        private readonly IOrderService _service;

        public OrderController(IHubContext<NotificationHub> notificationHubContext, IOrderService service)
        {
            _notificationHubContext = notificationHubContext;
            _service = service;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Order_Dto dto)
        {
            dto.CreateTime = DateTime.Now;
            var result = await _service.Create(dto);
            if (dto.Email != "admin@gmail.com")
            {
                await _notificationHubContext.Clients.All.SendAsync(NotificationSignalConstant.SEND, result.Data);
            }
            return Ok(result);
        }

        [HttpGet("GetDataPagination")]
        public async Task<IActionResult> GetDataPagination([FromQuery] PaginationParam pagination, string keyword)
        {
            return Ok(await _service.GetDataPagination(pagination, keyword));
        }

        [HttpGet("GetOderNotConfirm")]
        public async Task<IActionResult> GetOderNotConfirm()
        {
            return Ok(await _service.GetOderNotConfirm());
        }

        [HttpGet("GetDetail")]
        public async Task<IActionResult> GetDetail(int id)
        {
            return Ok(await _service.GetDetail(id));
        }
        [HttpPut("Update")]
        [Authorize]
        public async Task<IActionResult> Update([FromForm] Order_Dto dto)
        {
            return Ok(await _service.Update(dto));
        }
        [HttpPut("UpdateStatus")]
        [Authorize]
        public async Task<IActionResult> UpdateStatus(OrderStatus_Dto dto)
        {
            return Ok(await _service.UpdateStatus(dto));
        }
        [HttpPut("UpdateProduct")]
        [Authorize]
        public async Task<IActionResult> UpdateProduct(int productID)
        {
            return Ok(await _service.MinusProduct(productID));
        }
        [HttpDelete("Delete")]
        [Authorize]
        public async Task<IActionResult> Delete(Order_Dto dto)
        {
            return Ok(await _service.Delete(dto));
        }
        [HttpGet("GetOrderImport")]
        [Authorize]
        public async Task<IActionResult> GetOrderImport()
        {
            return Ok(await _service.GetOrderImport());
        }

    }


}