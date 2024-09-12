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
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;

        public ItemsController(ApplicationDbContext dbContext , IMapper mapper , IUnitOfWork unitOfWork)
        {
            this.dbContext = dbContext;
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
                    Name = c.Name
                }).ToList();

            var types = unitOfWork.Types.GetAll()
                .Select(t => new itemTypeDto
                {
                    Id = t.Id,
                    Name = t.Name
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
                Types =types,
                Units = units
            };

            return Ok(formDto);
        }


        [HttpGet("AllItems")]
        public ActionResult<ItemDto> getAllItems()
        {
            List<Item> items = unitOfWork.Items.GetAll().ToList();
           List<ItemDto> itemsmaped = mapper.Map<List<Item>,List<ItemDto>>(items);
            return Ok(itemsmaped);
        }

        // POST method to add a new item
        [HttpPost]
        public ActionResult<Item> AddItem([FromBody] ItemDto itemDto)
        {

            var company = unitOfWork.Companies.getById(itemDto.CompanyId);
            var type = unitOfWork.Types.getById(itemDto.TypeId);
            var unit = unitOfWork.Units.getById(itemDto.UnitId);

            if (company == null || type == null || unit == null)
            {
                return BadRequest("Invalid company, type, or unit ID.");
            }

            var item = mapper.Map<ItemDto, Item>(itemDto);
            item.Id = Guid.NewGuid().ToString();
            item.CompanyId = company.Id;
            item.TypeId = type.Id;
            item.UnitId = unit.Id;

            unitOfWork.Items.add(item);
            unitOfWork.Complete();

            return Ok(itemDto);
        }


        //[HttpPost]
        //public ActionResult<Item> AddItem([FromBody] ItemDto itemDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    try
        //    {
        //        var company = unitOfWork.Companies.getById(itemDto.CompanyId);
        //        var type = unitOfWork.Types.getById(itemDto.TypeId);
        //        var unit = unitOfWork.Units.getById(itemDto.UnitId);

        //        if (company == null || type == null || unit == null)
        //        {
        //            return BadRequest("Invalid company, type, or unit ID.");
        //        }

        //        var item = mapper.Map<ItemDto, Item>(itemDto);
        //        item.Id = Guid.NewGuid().ToString();
        //        item.CompanyId = company.Id;
        //        item.TypeId = type.Id;
        //        item.UnitId = unit.Id;

        //        unitOfWork.Items.add(item);

        //        unitOfWork.Complete();

        //        return Ok(item);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, "An error occurred while processing your request.");
        //    }
        //}


    }
}
