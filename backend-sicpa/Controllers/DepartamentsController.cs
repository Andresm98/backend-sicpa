using AutoMapper;
using backend_sicpa.Dto;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend_sicpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEnterpriseRepository _enterpriseRepository;

        public DepartamentsController(
                IDepartmentRepository departmentRepository,
                IMapper mapper,
                IEnterpriseRepository enterpriseRepository
            )
        {
            _departmentRepository= departmentRepository;
            _mapper = mapper;
            _enterpriseRepository = enterpriseRepository;
        }


        [HttpGet]
        [ProducesResponseType(200, Type= typeof(IEnumerable<Department>))]
        public IActionResult GetDepartments()
        {
            var employees = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartments());

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(employees);
        }


        [HttpGet("{departmentId}")]
        [ProducesResponseType(200, Type = typeof(Department))]
        [ProducesResponseType(400)]
        public IActionResult GetEnterpriseDetails(int departmentId)
        {

            if (!_departmentRepository.DepartmentExists(departmentId))
                return NotFound();

            var department = _mapper.Map<DepartmentDto>(_departmentRepository.GetDepartment(departmentId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(department);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEnterprise([FromQuery] int enterpriseId, [FromBody] DepartmentDto departmentCreate)
        {
            if (departmentCreate == null)
                return BadRequest(ModelState);

            var departments = _departmentRepository.GetDepartments()
                .Where(c => c.Name.Trim().ToUpper() == departmentCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (departments != null)
            {
                ModelState.AddModelError("", "Department already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentMap = _mapper.Map<Department>(departmentCreate);

            departmentMap.Enterprises = _enterpriseRepository.GetEnterprise(enterpriseId); 

            if (!_departmentRepository.CreateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }


        [HttpPut("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartment(int departmentId, [FromBody] DepartmentDto updatedDepartment)
        {
            if (updatedDepartment == null)
            { 
                return BadRequest(ModelState);
            }

            if (departmentId != updatedDepartment.Id)
            { 
                return BadRequest(ModelState);
            }

            if (!_departmentRepository.DepartmentExists(departmentId))
            { 
                return NotFound();
            }

            if (!ModelState.IsValid)
            { 
                return BadRequest();
            }

            var departmentMap = _mapper.Map<Department>(updatedDepartment);

            if (!_departmentRepository.UpdateDepartment(departmentMap))
            {
                ModelState.AddModelError("", "Something went wrong updating deparment!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }



        [HttpDelete("{departmentId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteReview(int departmentId)
        {
            if (!_departmentRepository.DepartmentExists(departmentId))
            {
                return NotFound();
            }

            var departmentToDelete = _departmentRepository.GetDepartment(departmentId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentRepository.DeleteDepartment(departmentToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting department");
            }

            return NoContent();
        }


    }
}
