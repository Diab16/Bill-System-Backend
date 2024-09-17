using Bill_system_API.IRepositories;
using Bill_system_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bill_system_API.DTOs;


namespace Bill_system_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UnitController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Unit
        [HttpGet]
        public ActionResult<IEnumerable<Unit>> GetUnits()
        {
            var units = _unitOfWork.Units.GetAll();
            return Ok(units);
        }

        // GET: api/Unit/5
        [HttpGet("{id}")]
        public ActionResult<UnitDTO> GetUnit(int id)
        {
            var unit = _unitOfWork.Units.GetAll().FirstOrDefault(u => u.Id == id);

            if (unit == null)
            {
                return NotFound();
            }

            var items = _unitOfWork.Items.GetAll().Where(i => i.UnitId == id).ToList();

            var unitDto = new UnitDTO
            {
                Id = unit.Id,
                Name = unit.Name,
                Notes = unit.Notes
            };

            

            return Ok(unitDto);
        }

        // POST: api/Unit
        [HttpPost]
        public ActionResult<Unit> PostUnit([FromBody] UnitDTO unitDto)
        {
            if (unitDto == null)
            {
                return BadRequest("Unit data is required.");
            }

            var unit = new Unit
            {
                Name = unitDto.Name,
                Notes = unitDto.Notes
            };

            _unitOfWork.Units.add(unit);
            _unitOfWork.Complete();

            return CreatedAtAction(nameof(GetUnit), new { id = unit.Id }, unit);
        }

        // PUT: api/Unit/5
        [HttpPut("{id}")]
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

            var existingUnit = _unitOfWork.Units.GetAll().FirstOrDefault(u => u.Id == id);
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
        public IActionResult DeleteUnit(int id)
        {
            var unit = _unitOfWork.Units.GetAll().FirstOrDefault(u => u.Id == id);

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
