using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OfficeOpenXml;
using TileShop.API.Order.Responses;
using TileShop.Application.Services.Interfaces;
using TileShop.Domain.Dtos;

namespace TileShop.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController
{
    private readonly IOrderService _orderService;
    private readonly IMapper _mapper;

    public OrdersController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetUserOrders()
    {
        var orders = await _orderService.GetUserOrdersAsync(int.Parse(UserId));
        return Ok(orders);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> DoCheckout()
    {
        var result = await _orderService.DoCheckoutAsync(int.Parse(UserId));
        return CreatedAtAction(nameof(DoCheckout), result);
    }

    [Authorize(Policy = "RequireOwner")]
    [HttpGet]
    [Route("average")]
    public IActionResult GetAverageOrderPrice()
    {
        var average = _orderService.GetAverageOrderPrice();
        return Ok(average);
    }

    [Authorize(Policy = "RequireOwner")]
    [HttpGet]
    [Route("total")]
    public IActionResult GetTotalOrder()
    {
        var total = _orderService.GetTotalOrder();
        return Ok(total);
    }

    [Authorize(Policy = "RequireOwner")]
    [HttpGet]
    [Route("count")]
    public IActionResult GetOrderCountByMonth()
    {
        var orders = _orderService.GetOrderCountForCurrentYearByMonth();
        return Ok(orders);
    }

    [Authorize(Policy = "RequireOwner")]
    [HttpGet]
    [Route("last")]
    public async Task<IActionResult> GetOrdersForLast30DaysAsync()
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        var orders = await _orderService.GetOrdersForLast30DaysAsync();
        var ordersResponse = _mapper.Map<List<OrderResponse>>(orders);


        using (var excelPackage = new ExcelPackage())
        {
            var worksheet = excelPackage.Workbook.Worksheets.Add("Orders");
            worksheet.Cells[1, 1].Value = "ID";
            worksheet.Cells[1, 2].Value = "Created date";
            worksheet.Cells[1, 3].Value = "Total price";
            worksheet.Cells[1, 4].Value = "Products";

            int row = 2;

            foreach (var order in ordersResponse)
            {
                if (order.Details.Count() > 0)
                {
                    worksheet.Cells[row, 1].Value = order.Id;
                    worksheet.Cells[row, 2].Value = order.CreatedDate.GetDateTimeFormats();
                    worksheet.Cells[row, 3].Value = order.TotalPrice;
                    var productNames = new List<string>();
                    foreach (var details in order.Details)
                    {
                        productNames.Add(details.Product.Name);
                    }

                    worksheet.Cells[row, 4].Value = string.Join(',', productNames);

                    row++;
                }
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            using (var memoryStream = new MemoryStream())
            {
                excelPackage.SaveAs(memoryStream);

                var fileContent = memoryStream.ToArray();
                var mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                return File(fileContent, mimeType, "orders.xlsx");
            }

        }
    }
}
