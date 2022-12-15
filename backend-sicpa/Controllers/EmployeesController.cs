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
    public class EmployeesController : ControllerBase
    {
        private readonly SicpaDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(
            SicpaDbContext dbContext,
            IMapper mapper,
            IEmployeeRepository employeeRepository
            )
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _employeeRepository = employeeRepository;
        }

        // GET: api/<EmployeesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployees()
        {
            var employees = _mapper.Map<List<EmployeeDto>>(_employeeRepository.GetEmployees());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employees);
        }

        // GET api/<EmployeesController>/5
        [HttpGet("{employeeId}")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        [ProducesResponseType(400)]
        public IActionResult GetEmployeeDetails(int employeeId)
        {

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            var employee = _mapper.Map<EmployeeDto>(_employeeRepository.GetEmployee(employeeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(employee);
        }

        // POST api/<EmployeesController>
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            var employees = _employeeRepository.GetEmployees()
               .Where(c => c.Name.Trim().ToUpper() == employee.Name.TrimEnd().ToUpper())
               .FirstOrDefault();

            if (employees != null)
            {
                ModelState.AddModelError("", "Employee already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var employeeMap = _mapper.Map<Employee>(employee);

            if (!_employeeRepository.CreateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went bad, review your data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        // PUT api/<EmployeesController>/5
        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(int employeeId, [FromBody] EmployeeDto employee)
        {
            if (employee == null)
                return BadRequest(ModelState);

            if (employeeId != employee.Id)
                return BadRequest(ModelState);

            if (!_employeeRepository.EmployeeExists(employeeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var employeeMap = _mapper.Map<Employee>(employee);

            if (!_employeeRepository.UpdateEmployee(employeeMap))
            {
                ModelState.AddModelError("", "Something went bad, review your data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success Updated");
        }

        // DELETE api/<EmployeesController>/5
        [HttpDelete("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEnterprise(int employeeId)
        {
            if (!_employeeRepository.EmployeeExists(employeeId))
            {
                return NotFound();
            }

            var employeeToDelete = _employeeRepository.GetEmployee(employeeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_employeeRepository.DeleteEmployee(employeeToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Success Deletion");
        }
    }
}