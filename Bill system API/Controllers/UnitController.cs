using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bill_system_API.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;


namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public UnitController(IUnitOfWork unitOfWork , IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: api/Unit
        [HttpGet]

        public ActionResult<IEnumerable<UnitDTO>> GetUnits()
        {
            var units = _unitOfWork.Units.GetAll().ToList();
            List<UnitDTO> unitsMapped = mapper.Map<List<UnitDTO>>(units);
            return Ok(unitsMapped);
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        [Authorize]

        public ActionResult<UnitDTO> GetUnit(int id)
        {
            var unit = _unitOfWork.Units.getById(id);

            if (unit == null)
            {
                return NotFound();
            }
            UnitDTO unitDto = mapper.Map<UnitDTO>(unit);

            return Ok(unitDto);
        }

        // POST: api/Unit
        [HttpPost]
        [Authorize]

        public ActionResult PostUnit([FromBody] UnitDTO unitDto)
        {
            if (unitDto == null)
            {
                return BadRequest("Unit data is required.");
            }

            Unit unit = mapper.Map<Unit>(unitDto);

            _unitOfWork.Units.add(unit);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
        }

        // PUT: api/Unit/5
        [HttpPut("{id}")]
        [Authorize]

        public IActionResult PutUnit(int id, [FromBody] UnitDTO unitDto)
        {
            if (unitDto == null)
            {
                return BadRequest("Unit data is required.");
            }

            if (id != unitDto.Id)
            {
                return BadRequest("Unit ID mismatch.");
            }

            var existingUnit = _unitOfWork.Units.getById(id);
            if (existingUnit == null)
            {
                return NotFound();
            }

            // Update only the fields of the unit entity
            existingUnit.Name = unitDto.Name;
            existingUnit.Notes = unitDto.Notes;

            // Save the updated unit
            _unitOfWork.Units.update(existingUnit);
            _unitOfWork.Complete();

            return NoContent();
        }


        // DELETE: api/Unit/5
        [HttpDelete("{id}")]
        [Authorize]

        public IActionResult DeleteUnit(int id)
        {
            var unit = _unitOfWork.Units.getById(id);

            if (unit == null)
            {
                return NotFound();
            }

            // Check if the unit has related items
            if (_unitOfWork.Items.GetAll().Any(i => i.UnitId == id))
            {
                return Conflict("Cannot delete unit because it has related items.");
            }

            _unitOfWork.Units.delete(unit);
            _unitOfWork.Complete();

            return NoContent();
        }
    }

}
