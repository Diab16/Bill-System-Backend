using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using Type = Bill_system_API.Models.Type;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        public ItemsController(ApplicationDbContext dbContext , IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }


        // GET method to retrieve data for the form
        [HttpGet("FormData")]
        public ActionResult<ItemDto> GetRelatedData()
        {
            var companies = dbContext.Companies
                .Select(c => new CompanyDTO
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();

            var types = dbContext.Types
                .Select(t => new TypeDTO
                {
                    Id = t.Id,
                    TypeName = t.Name
                }).ToList();

            var units = dbContext.Units
                .Select(u => new UnitDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();

            var formDto = new FormDto
            {
                Companies = companies,
                Types = types,
                Units = units
            };

            return Ok(formDto);
        }


        [HttpGet("AllItems")]
        public ActionResult<ItemDto> getAllItems()
        {
            List<Item> items = dbContext.Items.ToList();
           List<ItemDto> itemsmaped = mapper.Map<List<Item>,List<ItemDto>>(items);
            return Ok(itemsmaped);
        }

        // POST method to add a new item
        [HttpPost]
        public ActionResult<Item> AddItem([FromBody] ItemDto itemDto)
        { 

            var company = dbContext.Companies.Find(itemDto.CompanyId);
            var type = dbContext.Types.Find(itemDto.TypeId);
            var unit = dbContext.Units.Find(itemDto.UnitId);

            if (company == null || type == null || unit == null)
            {
                return BadRequest("Invalid company, type, or unit ID.");
            }

             var item = mapper.Map<ItemDto ,Item>(itemDto);
             item.Id = Guid.NewGuid().ToString(); 
             item.CompanyId = company.Id;
             item.TypeId = type.Id;
             item.UnitId = unit.Id;
       
            dbContext.Items.Add(item);
            dbContext.SaveChanges();

            return Ok(itemDto);
        }




    }
}
