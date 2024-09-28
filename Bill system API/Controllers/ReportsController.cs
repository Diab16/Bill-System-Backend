using Bill_system_API.DTOs;
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.CodeDom;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IGenericRepository<Invoice> _invoiceRepository;
        private readonly IGenericRepository<Item> _itemRepository;
        public ReportsController(
            IGenericRepository<Invoice> invoiceRepository,
            IGenericRepository<Item> itemRepository)
        {
            _invoiceRepository = invoiceRepository;
            _itemRepository = itemRepository;
        }
        [HttpGet("storage")]
        public IActionResult StorageReport()
        {
            try
            {
                List<Item> ItemsInStorage = _itemRepository.GetAll().Where(x => x.AvailableAmount > 0).ToList();
                if (ItemsInStorage.Count == 0) return NotFound();
                else
                {
                    var Items = ItemsInStorage.Select(item => new simpleItemDTO
                    {
                        ItemId = item.Id,
                        ItemName = item.Name,
                        ItemAvailableAmount = item.AvailableAmount
                    }).ToList();
                    return Ok(Items);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("invoice")]
        public IActionResult InvoiceReport([FromBody] DateRangeDTO _date)
        {
            try
            {
                List<Invoice> invoices = _invoiceRepository.GetAll().Where(x=>x.Date>=DateOnly.Parse(_date.first) || x.Date<= DateOnly.Parse(_date.last)).ToList();
                if(invoices.Count == 0) { return NotFound(); }
                else
                {
                    var resultInvoices = invoices.Select(invoice => new simpleInvoiceDTO
                    {
                        Id = invoice.Id,
                        Date=invoice.Date,
                        BillNumber = invoice.BillNumber,
                        BillTotal = invoice.BillTotal,
                        PercentageDiscount = invoice.PercentageDiscount,
                        PaidUp = invoice.PaidUp,
                    }).ToList();
                    return Ok(resultInvoices);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
