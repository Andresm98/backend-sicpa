using AutoMapper;
using backend_sicpa.Interfaces;
using backend_sicpa.Models;

namespace backend_sicpa.Repository
{
    public class EnterpriseRepository : IEnterpriseRepository
    {

        private readonly SicpaDbContext _context;
        private readonly IMapper _mapper;

        public EnterpriseRepository(SicpaDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateEnterprise(Enterprise enterprise)
        {
            _context.Add(enterprise);
            return Save();
        }

        public bool DeleteEnterprise(Enterprise enterprise)
        {
            _context.Remove(enterprise);
            return Save();
        }

        public Enterprise GetEnterprise(int enterpriseId)
        {
            return _context.Enterprises.Where(r => r.Id == enterpriseId).FirstOrDefault();
        }

        public ICollection<Enterprise> GetEnterprises()
        {
            return _context.Enterprises.ToList();
        }

        public ICollection<Department> GetDepartmentsByEnterprise(int enterpriseId)
        {
            return _context.Departments.Where(r => r.Enterprises.Id == enterpriseId).ToList();
        }

        public bool EnterpriseExists(int enterpriseId)
        {
            return _context.Enterprises.Any(r => r.Id == enterpriseId);
        }
 
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEnterprise(Enterprise enterprise)
        {

        _context.Update(enterprise);
        return Save();
        }

    }
}
