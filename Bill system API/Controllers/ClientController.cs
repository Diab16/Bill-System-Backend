using AutoMapper;
using Bill_system_API.DTOs;
using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        public ClientController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<ClientDTO> GetClients()
        {
            var clients = unitOfWork.Clients.GetAll().ToList();
            List<ClientDTO> clientsMapped = mapper.Map<List<ClientDTO>>(clients);
            return Ok(clientsMapped);
        }

        [HttpGet("{id}")]
        public ActionResult<ClientDTO> GetClient(int id)
        {
            var client = unitOfWork.Clients.getById(id);
            if (client == null)
            {
                return NotFound();
            }
            ClientDTO clientMapped = mapper.Map<ClientDTO>(client);
            return Ok(clientMapped);
        }

        [HttpPost]
        public ActionResult<ClientDTO> PostClient(ClientDTO clientDTO)
        {
            if (clientDTO == null)
            {
                return BadRequest();
            }
            Client clientNew = mapper.Map<Client>(clientDTO);
            unitOfWork.Clients.add(clientNew);
            unitOfWork.Complete();
            return CreatedAtAction("GetClient", new { id = clientNew.Id }, clientNew);
        }

        [HttpPut("{id}")]
        public IActionResult PutClient(int id, ClientDTO clientDTO)
        {
            if (id != clientDTO.Id)
            {
                return BadRequest();
            }
            Client clientDB = unitOfWork.Clients.getById(id);
            if (clientDB == null)
            {
                return NotFound();
            }
            clientDB.Name = clientDTO.Name;
            clientDB.Phone = clientDTO.Phone;
            clientDB.Address = clientDTO.Address;
            unitOfWork.Clients.update(clientDB);
            unitOfWork.Complete();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var clientDB = unitOfWork.Clients.getById(id);
            if (clientDB == null)
            {
                return NotFound();
            }

            unitOfWork.Clients.delete(clientDB);
            unitOfWork.Complete();
            return NoContent();
        }

    }
}
