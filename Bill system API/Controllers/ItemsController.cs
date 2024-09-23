using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Bill_system_API.Repositories;
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
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ItemsController( IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }


        // GET method to retrieve data for the form
        [HttpGet("FormData")]
        public ActionResult<ItemDto> GetRelatedData()
        {
           

            var companies = unitOfWork.Companies.GetAll()
           .Select(c => new ItemCompanyDto
           {
           Id = c.Id,
           Name = c.Name,
           Types = c.Types.Select(t => new itemTypeDto
           {
               Id = t.Id,
               Name = t.Name
           }).ToList() // Include related types for each company
           }).ToList();

     

            var units = unitOfWork.Units.GetAll()
                .Select(u => new itemUnitDto
                {
                    Id = u.Id,
                    Name = u.Name
                }).ToList();

            var formDto = new FormDto
            {
                Companies = companies,
                Units = units
            };

            return Ok(formDto);
        }


        [HttpGet("AllItems")]
        public ActionResult<ItemDto> getAllItems()
        {
            var items = unitOfWork.Items.GetAll().ToList();
            IEnumerable<ItemDto> itemmaped = mapper.Map<IEnumerable<ItemDto>>(items);
            foreach (var item in items)
            {
                var itemDto = itemmaped.First(dto => dto.Id == item.Id);

                // Manually map related entities
                itemDto.Company = new ItemCompanyDto
                {
                    Id = item.Company.Id,
                    Name = item.Company.Name
                };
                itemDto.Unit = new itemUnitDto
                {
                    Id = item.Unit.Id,
                    Name = item.Unit.Name
                };
                itemDto.Type = new itemTypeDto
                {
                    Id = item.Type.Id,
                    Name = item.Type.Name
                };
            }
            return Ok(itemmaped);
        }



        [HttpPost]
        public ActionResult<Item> AddItem([FromBody] ItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var company = unitOfWork.Companies.getById(itemDto.CompanyId);
                var type = unitOfWork.Types.getById(itemDto.TypeId);
                var unit = unitOfWork.Units.getById(itemDto.UnitId);

                if (company == null || type == null || unit == null)
                {
                    return BadRequest("Invalid company, type, or unit ID.");
                }

                var item = mapper.Map<ItemDto, Item>(itemDto);
                item.CompanyId = company.Id;
                item.TypeId = type.Id;
                item.UnitId = unit.Id;

                unitOfWork.Items.add(item);

                unitOfWork.Complete();

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }


        [HttpGet("GetById/{id}")]
        public ActionResult<Item> GetItemById(int id)
        {
            Item item = unitOfWork.Items.getById(id);
            if (item == null) return NotFound();
            ItemDto itemDto = mapper.Map<Item, ItemDto>(item); 
            return Ok(item);
        }

        [HttpDelete("{id}")]
       public ActionResult<Item> DeleteItem(int id )
        {
            Item item = unitOfWork.Items.getById(id);
            if (item == null) return NotFound();
            unitOfWork.Items.delete(item);
            unitOfWork.Complete();
            return Ok(item);

        }



        [HttpPut]
        
        public ActionResult<ItemDto> EditItem([FromBody] ItemDto itemDto)
        {
            if (itemDto.Id == 0) return BadRequest("Invalid ID.");

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                return BadRequest(new { errors = errors.Select(e => e.ErrorMessage) });
            }

            try
            {
                Item itemMapped = mapper.Map<ItemDto, Item>(itemDto);
                unitOfWork.Items.update(itemMapped);
                unitOfWork.Complete();
                return Ok(itemMapped);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
