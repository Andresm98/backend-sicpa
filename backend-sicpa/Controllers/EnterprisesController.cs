using AutoMapper;
using backend_sicpa.Dto;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;
using backend_sicpa.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend_sicpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        private readonly SicpaDbContext dbContext;
        private readonly IMapper _mapper;
        private readonly IEnterpriseRepository _enterpriseRepository;
        private readonly IDepartmentRepository _departmentRepository;

        public EnterprisesController(
            SicpaDbContext dbContext,
            IMapper mapper,
            IEnterpriseRepository enterpriseRepository,
            IDepartmentRepository departmentRepository
            )
        {
            this.dbContext = dbContext;
            _mapper = mapper;
            _enterpriseRepository = enterpriseRepository;
            _departmentRepository = departmentRepository;
        }

        // GET: api/<EnterprisesController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Enterprise>))]
        public IActionResult GetEnterprises()
        {
            var enterprises = _mapper.Map<List<EnterpriseDto>>(_enterpriseRepository.GetEnterprises());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(enterprises);
        }

        // GET: api/<EnterpriseController>/departments/4

        [HttpGet("departments/{enterpriseId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Enterprise>))]
        [ProducesResponseType(400)]
        public IActionResult GetDepartmentsByEnterprise(int enterpriseId)
        {
            var enterprisesByDepartment = _mapper.Map<List<DepartmentDto>>(_departmentRepository.GetDepartmentsOfAEnterprise(enterpriseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(enterprisesByDepartment);
        }

            // GET api/<EnterprisesController>/5
        [HttpGet("{enterpriseId}")]
        [ProducesResponseType(200, Type = typeof(Enterprise))]
        [ProducesResponseType(400)]
        public IActionResult GetEnterpriseDetails(int enterpriseId)
        {

            if (!_enterpriseRepository.EnterpriseExists(enterpriseId))
                return NotFound();

            var enterprise = _mapper.Map<EnterpriseDto>(_enterpriseRepository.GetEnterprise(enterpriseId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(enterprise);
        }

        // POST api/<EnterprisesController>
        [HttpPost]
        public IActionResult CreateEnterprise([FromBody] EnterpriseDto enterprise)
        {
            if (enterprise == null)
                return BadRequest(ModelState);
            
            var enterprises = _enterpriseRepository.GetEnterprises()
               .Where(c => c.Name.Trim().ToUpper() == enterprise.Name.TrimEnd().ToUpper())
               .FirstOrDefault();
            
            if (enterprises != null)
            {
                ModelState.AddModelError("", "Enterprise already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var enterpriseMap = _mapper.Map<Enterprise>(enterprise);

            if (!_enterpriseRepository.CreateEnterprise(enterpriseMap))
            {
                ModelState.AddModelError("", "Something went bad, review your data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success");
        }

        // PUT api/<EnterprisesController>/5
        [HttpPut("{enterpriseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEnterprise( int enterpriseId, [FromBody] EnterpriseDto enterprise)
        {
            if (enterprise == null)
                return BadRequest(ModelState);

            if (enterpriseId != enterprise.Id)
                return BadRequest(ModelState);

            if (!_enterpriseRepository.EnterpriseExists(enterpriseId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();
 
            var enterpriseMap = _mapper.Map<Enterprise>(enterprise);

            if (!_enterpriseRepository.UpdateEnterprise(enterpriseMap))
            {
                ModelState.AddModelError("", "Something went bad, review your data.");
                return StatusCode(500, ModelState);
            }

            return Ok("Success Updated");
        }

        // DELETE api/<EnterprisesController>/5
        [HttpDelete("{enterpriseId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteEnterprise(int enterpriseId)
        {
            if (!_enterpriseRepository.EnterpriseExists(enterpriseId))
            {
                return NotFound();
            }

            var enterpriseToDelete = _enterpriseRepository.GetEnterprise(enterpriseId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_enterpriseRepository.DeleteEnterprise(enterpriseToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return Ok("Success Deletion");
        }
    }
}