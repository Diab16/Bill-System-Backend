using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bill_system_API.Models;
using AutoMapper;
using Bill_system_API.IRepositories;
using Bill_system_API.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public CompanyController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        // GET: api/Company
        [HttpGet]
        public ActionResult<CompanyDTO> GetCompanies()
        {
            var companies = unitOfWork.Companies.GetAll().ToList();
            IEnumerable<CompanyDTO> companiesMapped = mapper.Map<IEnumerable<CompanyDTO>>(companies);
            return Ok(companiesMapped);
        }
        // GET: api/Company/5
        [HttpGet("{id}")]
        public ActionResult<CompanyDTO> GetCompany(int id)
        {
            var company = unitOfWork.Companies.getById(id);
            if (company == null)
            {
                return NotFound();
            }
            CompanyDTO companyMapped = mapper.Map<CompanyDTO>(company);
            return Ok(companyMapped);
        }

        // PUT: api/Company/5
        [Authorize]

        [HttpPut("{id}")]
        public IActionResult PutCompany(int id, CompanyDTO companyDTO)
        {
            if (id != companyDTO.Id)
            {
                return BadRequest();
            }
            Company companyDB = unitOfWork.Companies.getById(id);
            if (companyDB == null)
            {
                return NotFound();
            }
            companyDB.Name = companyDTO.Name;
            companyDB.Notes = companyDTO.Notes;
            unitOfWork.Companies.update(companyDB);
            unitOfWork.Complete();

            return NoContent();
        }

        // POST: api/Company
        [Authorize]

        [HttpPost]
        public ActionResult<CompanyDTO> PostCompany(CompanyDTO companyDTO)
        {
            if (companyDTO == null)
            {
                return BadRequest();
            }
            Company companyNew = mapper.Map<Company>(companyDTO);
            unitOfWork.Companies.add(companyNew);
            unitOfWork.Complete();
            return CreatedAtAction("GetCompany", new { id = companyNew.Id }, companyNew);
        }

        // DELETE: api/Company/5
        [Authorize]

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var companyDB = unitOfWork.Companies.getById(id);
            if (companyDB == null)
            {
                return NotFound();
            }

            unitOfWork.Companies.delete(companyDB);
            unitOfWork.Complete();
            return NoContent();
        }
    }
}
