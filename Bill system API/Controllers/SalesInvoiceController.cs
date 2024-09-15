using Bill_system_API.DTOs;
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private readonly IGenericRepository<Invoice> _invoiceRepository;
        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IGenericRepository<Employee> _employeeRepository;

        public SalesInvoiceController(
            IGenericRepository<Invoice> invoiceRepository,
            IGenericRepository<Item> itemRepository,
            IGenericRepository<Client> clientRepository,
            IGenericRepository<Employee> employeeRepository)
        {
            _invoiceRepository = invoiceRepository;
            _itemRepository = itemRepository;
            _clientRepository = clientRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost]
        public IActionResult AddInvoice([FromBody] InvoiceDTO invoiceDTO)
        {
            // Validate Date
            if (invoiceDTO.Date == default)
            {
                return BadRequest("Date is required");
            }

            // Validate Start Time and End Time
            if (invoiceDTO.EndTime <= invoiceDTO.StartTime)
            {
                return BadRequest("End Time must be greater than Start Time");
            }

            // Validate Client and Employee
            var client = _clientRepository.getById(invoiceDTO.ClientId);
            var employee = _employeeRepository.getById(invoiceDTO.EmployeeId);

            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }
            if (employee == null)
            {
                return BadRequest("Invalid Employee ID");
            }

            // Create a new Invoice
            Invoice invoice = new Invoice
            {
                BillNumber = GenerateBillNumber(),
                Date = DateOnly.FromDateTime(invoiceDTO.Date),
                StartTime = TimeOnly.FromDateTime(DateTime.Today.Add(invoiceDTO.StartTime)),
                EndTime = TimeOnly.FromDateTime(DateTime.Today.Add(invoiceDTO.EndTime)),
                Client = client,
                Employee = employee,
                PercentageDiscount = invoiceDTO.PercentageDiscount,
                ValueDiscount = invoiceDTO.ValueDiscount,
                PaidUp = invoiceDTO.PaidUp,
                InvoiceItems = new List<InvoiceItem>()
            };

            // Add Invoice Items and Validate each item
            foreach (var itemDto in invoiceDTO.InvoiceItems)
            {
                var item = _itemRepository.getById(itemDto.ItemId);
                if (item == null)
                {
                    return BadRequest($"Item with ID {itemDto.ItemId} not found");
                }

                // Validate Quantity
                if (itemDto.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero");
                }

                // Calculate TotalValue for each item
                double totalValue = (itemDto.SellingPrice * itemDto.Quantity) - itemDto.Discount;
                var invoiceItem = new InvoiceItem
                {
                    item = item,
                    SellingPrice = itemDto.SellingPrice,
                    Quantity = itemDto.Quantity,
                    Discount = itemDto.Discount,
                    TotalValue = totalValue,
                    Invoice = invoice
                };
                invoice.InvoiceItems.Add(invoiceItem);
            }

            // Calculate Derived Attributes
            double billTotal = invoice.InvoiceItems.Sum(i => i.TotalValue);
            double netTotal = billTotal - invoice.ValueDiscount - (invoice.PercentageDiscount / 100) * billTotal;
            double theRest = netTotal - invoice.PaidUp;

            // Save the Invoice
            _invoiceRepository.add(invoice);
            _invoiceRepository.save();

            // Return the calculated fields to the frontend
            var response = new
            {
                InvoiceId = invoice.Id,
                BillTotal = billTotal,
                NetTotal = netTotal,
                TheRest = theRest
            };

            return Ok(response);
        }




        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var invoices = _invoiceRepository.GetAll();
                return Ok(invoices);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        // Delete Invoice (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteInvoice(int id)
        {
            var existingInvoice = _invoiceRepository.getById(id);
            if (existingInvoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }

            _invoiceRepository.delete(existingInvoice);
            _invoiceRepository.save();

            return NoContent();
        }


        // Update Invoice (PUT)
        [HttpPut("{id}")]
        public IActionResult UpdateInvoice(int id, [FromBody] InvoiceDTO invoiceDTO)
        {
            var existingInvoice = _invoiceRepository.getById(id);
            if (existingInvoice == null)
            {
                return NotFound($"Invoice with ID {id} not found.");
            }

            // Validate Date
            if (invoiceDTO.Date == default)
            {
                return BadRequest("Date is required");
            }

            // Validate Start Time and End Time
            if (invoiceDTO.EndTime <= invoiceDTO.StartTime)
            {
                return BadRequest("End Time must be greater than Start Time");
            }

            // Validate Client and Employee
            var client = _clientRepository.getById(invoiceDTO.ClientId);
            var employee = _employeeRepository.getById(invoiceDTO.EmployeeId);
            if (client == null)
            {
                return BadRequest("Invalid Client ID");
            }
            if (employee == null)
            {
                return BadRequest("Invalid Employee ID");
            }

            // Update invoice properties
            existingInvoice.Date = DateOnly.FromDateTime(invoiceDTO.Date);
            existingInvoice.StartTime = TimeOnly.FromDateTime(DateTime.Today.Add(invoiceDTO.StartTime));
            existingInvoice.EndTime = TimeOnly.FromDateTime(DateTime.Today.Add(invoiceDTO.EndTime));
            existingInvoice.Client = client;
            existingInvoice.Employee = employee;
            existingInvoice.PercentageDiscount = invoiceDTO.PercentageDiscount;
            existingInvoice.ValueDiscount = invoiceDTO.ValueDiscount;
            existingInvoice.PaidUp = invoiceDTO.PaidUp;

            // Update Invoice Items
            existingInvoice.InvoiceItems.Clear();
            foreach (var itemDto in invoiceDTO.InvoiceItems)
            {
                var item = _itemRepository.getById(itemDto.ItemId);
                if (item == null)
                {
                    return BadRequest($"Item with ID {itemDto.ItemId} not found");
                }

                if (itemDto.Quantity <= 0)
                {
                    return BadRequest("Quantity must be greater than zero");
                }

                double totalValue = (itemDto.SellingPrice * itemDto.Quantity) - itemDto.Discount;
                var invoiceItem = new InvoiceItem
                {
                    item = item,
                    SellingPrice = itemDto.SellingPrice,
                    Quantity = itemDto.Quantity,
                    Discount = itemDto.Discount,
                    TotalValue = totalValue,
                    Invoice = existingInvoice
                };
                existingInvoice.InvoiceItems.Add(invoiceItem);
            }

            // Calculate Derived Attributes
            double billTotal = existingInvoice.InvoiceItems.Sum(i => i.TotalValue);
            double netTotal = billTotal - existingInvoice.ValueDiscount - (existingInvoice.PercentageDiscount / 100) * billTotal;
            double theRest = netTotal - existingInvoice.PaidUp;

            // Save the updated invoice
            _invoiceRepository.update(existingInvoice);
            _invoiceRepository.save();

            var response = new
            {
                InvoiceId = existingInvoice.Id,
                BillTotal = billTotal,
                NetTotal = netTotal,
                TheRest = theRest
            };

            return Ok(response);
        }

        //  method to generate the Bill Number
        private int GenerateBillNumber()
        {
            // Get the last invoice's BillNumber, if exists, and increment it.
            var lastInvoice = _invoiceRepository.GetAll().OrderByDescending(i => i.BillNumber).FirstOrDefault();
            return lastInvoice != null ? lastInvoice.BillNumber + 1 : 1;
        }
    }

}
