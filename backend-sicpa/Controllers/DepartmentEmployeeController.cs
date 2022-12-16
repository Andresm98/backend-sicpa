using AutoMapper;
using backend_sicpa.Dto;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;
using backend_sicpa.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend_sicpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentEmployeeController : ControllerBase
    {

        private readonly SicpaDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IDepartmentEmployeeRepository _departmentEmployeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public DepartmentEmployeeController(
                SicpaDbContext dbContext,
                IMapper mapper,
                IDepartmentEmployeeRepository departmentEmployeeRepository,
                IDepartmentRepository departmentRepository,
                IEmployeeRepository employeeRepository
            )
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _departmentEmployeeRepository = departmentEmployeeRepository;
            _departmentRepository = departmentRepository;
            _employeeRepository = employeeRepository;
        }

        // GET: api/<DepartmentEmployeeController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DepartmentsEmployee>))]
        public IActionResult GetDepartmentsEmployees()
        {
            var departmentsEmployees = _mapper.Map<List<DepartmentEmployeeDto>>(_departmentEmployeeRepository.GetDepartmentsEmployees());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(departmentsEmployees);
        }

        // GET api/<DepartmentEmployeeController>/5
        [HttpGet("{departmentEmployeeId}")]
        [ProducesResponseType(200, Type = typeof(DepartmentsEmployee))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentEmployeeDetails(int departmentEmployeeId)
        {

            if (!_departmentEmployeeRepository.DepartmentEmployeeExists(departmentEmployeeId))
                return NotFound();

            var departmentEmployee = _mapper.Map<DepartmentEmployeeDto>(_departmentEmployeeRepository.GetDepartmentEmployee(departmentEmployeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(departmentEmployee);
        }

        // POST api/<DepartmentEmployeeController> 

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateDepartmentEmployee([FromQuery] int departmentId, [FromQuery] int employeeId, [FromBody] DepartmentEmployeeDto departmentEmployeeCreate)
        {
            if (departmentEmployeeCreate == null)
                return BadRequest(ModelState);

           
            var departmentEmployee = _departmentEmployeeRepository.GetDepartmentsEmployees()
                .Where(c => c.DepartmentsId == departmentId && c.EmployeesId == employeeId && c.Status == 1).FirstOrDefault();

            if (departmentEmployee != null)
            {
                if (departmentEmployeeCreate.Status == 1)
                {
                    ModelState.AddModelError("", "Contract already exists");
                    return StatusCode(422, ModelState);
                }
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var departmentEmployeeMap = _mapper.Map<DepartmentsEmployee>(departmentEmployeeCreate);

            departmentEmployeeMap.Departments = _departmentRepository.GetDepartment(departmentId);
            departmentEmployeeMap.Employees = _employeeRepository.GetEmployee(employeeId);

            if (!_departmentEmployeeRepository.CreateDepartmentEmployee(departmentEmployeeMap))
            {
                ModelState.AddModelError("", "Something went bad, review your data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        // PUT api/<DepartmentEmployeeController>/5
        [HttpPut("{departmentEmployeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDepartmentEmployee(
            int departmentEmployeeId,
            [FromQuery] int departmentId, 
            [FromQuery] int employeeId, 
            [FromBody] DepartmentEmployeeDto updatedDepartmentEmployee)
        {
            if (updatedDepartmentEmployee == null)
            { 
                return BadRequest(ModelState);
            }

            if (departmentId != updatedDepartmentEmployee.DepartmentsId || employeeId != updatedDepartmentEmployee.EmployeesId)
            { 
                return BadRequest(ModelState);
            }

            if (!_departmentEmployeeRepository.DepartmentEmployeeExists(departmentEmployeeId))
            { 
                return NotFound();
            }

            if (!ModelState.IsValid)
            { 
                return BadRequest();
            }

            var departmentEmployeeMap = _mapper.Map<DepartmentsEmployee>(updatedDepartmentEmployee);

            departmentEmployeeMap.Departments = _departmentRepository.GetDepartment(departmentId);
            departmentEmployeeMap.Employees = _employeeRepository.GetEmployee(employeeId);

            if (!_departmentEmployeeRepository.UpdateDepartmentEmployee(departmentEmployeeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating deparment!");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        // DELETE api/<DepartmentEmployeeController>/5
        [HttpDelete("{departmentEmployeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDepartmentEmployee(int departmentEmployeeId)
        {
            if (!_departmentEmployeeRepository.DepartmentEmployeeExists(departmentEmployeeId))
            {
                return NotFound();
            }

            var departmentEmployeeToDelete = _departmentEmployeeRepository.GetDepartmentEmployee(departmentEmployeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_departmentEmployeeRepository.DeleteDepartmentEmployee(departmentEmployeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Success Deletion");
        }
    }
}
