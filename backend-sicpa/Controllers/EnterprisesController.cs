using backend_sicpa.DbRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend_sicpa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnterprisesController : ControllerBase
    {
        private readonly ScottDbContext dbContext;

        public EnterprisesController(ScottDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        // GET: api/<EnterprisesController>
        [HttpGet]
        public IEnumerable<Enterprise> Get()
        {
            return dbContext.Enterprises.ToList();
        }

        // GET api/<EnterprisesController>/5
        [HttpGet("{id}")]
        public IEnumerable<Enterprise> GetEnterpriseDetails(int? id)
        {

            if (id == null)
            {
                return (IEnumerable<Enterprise>)NotFound();
            }

            return dbContext.Enterprises.Where(enterpr => enterpr.Id == id);
        }

        // POST api/<EnterprisesController>
        [HttpPost]
        public async Task<IActionResult> CreateEnterprise([FromBody] Enterprise enterprise)
        {
            if (enterprise == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.Enterprises.Add(enterprise);

            var created = dbContext.SaveChanges();

            return Created("created", created);
        }

        // PUT api/<EnterprisesController>/5
        [HttpPut]
        public async Task<IActionResult> UpdateEnterprise([FromBody] Enterprise enterprise)
        {
            if (enterprise == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.Enterprises.Update(enterprise);

            dbContext.SaveChanges();

            return NoContent();
        }

        // DELETE api/<EnterprisesController>/5
        [HttpDelete]
        public async Task<IActionResult> DeleteEnterprise(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var enterprise =  dbContext.Enterprises.Find(id);

            if (enterprise == null)
                return BadRequest();

            dbContext.Enterprises.Remove(enterprise);

            dbContext.SaveChanges();

            return NoContent();
        }
    }
}
